using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PF.Data.Models;

namespace PF.Api.Services
{
    public class PensionCalculator : IPensionCalculator
    {
        private readonly IBaseSettings _settings;

        private readonly double _baseSalaryX10;

        private const string MALE = "M";

        private const string FEMALE = "F";

        public PensionCalculator(IBaseSettings settings)
        {
            _settings = settings;
            _baseSalaryX10 = _settings.BaseSalary * 10;
        }

        public double Calculate(Person person)
        {
            var pension = 0.0;
            if (CanUseStandardPension(person))
            {
                pension = CalculateBasePension(person);
            }

            pension = ApplyModifiers(person, pension);
            return pension;
        }

        public bool CanUseStandardPension(Person person)
        {
            bool result = true;

            var age = GetDifferenceInYears(DateTime.Today, person.DateOfBirth);

            if (MALE.Equals(person.Sex, StringComparison.OrdinalIgnoreCase))
            {
                if (age < _settings.MinAgeMale)
                    result = false;
            }
            else
            {
                if (age < _settings.MinExpFemale)
                    result = false;
            }

            var exp = person.Experiences?.Select(e => GetDifferenceInYears(e.EndDate, e.StartDate)).Sum();

            if (exp == null)
                return false;

            if (MALE.Equals(person.Sex, StringComparison.OrdinalIgnoreCase))
            {
                if (exp < _settings.MinExpMale)
                    result = false;
            }
            else
            {
                if (exp < _settings.MinExpFemale)
                    result = false;
            }

            return result;
        }

        public double CalculateBasePension(Person person)
        {
            int exp = person.Experiences.Select(e => GetDifferenceInYears(e.EndDate, e.StartDate)).Sum();
            double coef = exp * 0.01;

            double pension = 0.01 * exp * _settings.AvgSalary;

            if (pension < _settings.BaseSalary)
                return _settings.BaseSalary;
            else if (pension > _baseSalaryX10)
                return _baseSalaryX10;
            else
                return pension;
        }

        public double ApplyModifiers(Person person, double pension)
        {
            if (person.Modifier == null)
            {
                return pension;
            }

            var modifier = person.Modifier;

            var pensionApplied = pension > 0.0001;

            if (!pensionApplied)
            {
                if (!modifier.Standalone)
                {
                    return pension;
                }

                if (modifier.Coefficient.HasValue)
                {
                    pension += _settings.BaseSalary * modifier.Coefficient.Value;
                }
            }
            else
            {
                if (modifier.Coefficient.HasValue)
                {
                    pension *= modifier.Coefficient.Value;
                }
            }

            if (modifier.FixedPayment.HasValue)
            {
                pension += modifier.FixedPayment.Value;
            }

            return pension;
        }

        private int GetDifferenceInYears(DateTime date1, DateTime date2)
        {
            if (date2 > date1)
            {
                var tmp = date1;
                date1 = date2;
                date2 = tmp;
            }

            int years = date1.Year - date2.Year;
            if (date2 > date1.AddYears(-years))
                --years;

            return years;
        }
    }
}
