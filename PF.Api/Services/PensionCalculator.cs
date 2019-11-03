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

        public PensionCalculator(IBaseSettings settings)
        {
            _settings = settings;
        }

        public double Calculate(Person person)
        {
            var pension = 0.0;
            if (CanUseStandardPension(person))
            {
                pension = CalculateBasePension();
            }

            pension = ApplyModifiers(person, pension);
            return pension;
        }

        public bool CanUseStandardPension(Person person)
        {
            // TODO: check if person can have default pension based on experience
            throw new NotImplementedException();
        }

        public double CalculateBasePension()
        {
            throw new NotImplementedException();
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
    }
}
