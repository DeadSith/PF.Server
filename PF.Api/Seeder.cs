using PF.Api.Services;
using PF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PF.Api
{
    public class Seeder
    {
        public Seeder(ApplicationDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected ApplicationDbContext Context { get; set; }

        public async Task Seed()
        {
            await AddDefaultModifiers();
            await AddDefaultVariables();
        }

        //TODO: add sample job and experiences

        public async Task AddDefaultModifiers()
        {
            Context.DisabilityGroups.Add(new Data.Models.PensionModifier
            {
                FixedPayment = 2395.80,
                Standalone = true,
                ModifierName = "Учасник бойових дій",
                Type = "D"
            });

            Context.DisabilityGroups.Add(new Data.Models.PensionModifier
            {
                FixedPayment = 2395.80,
                Standalone = true,
                ModifierName = "Учасник бойових дій",
                Type = "D"
            });

            Context.DisabilityGroups.Add(new Data.Models.PensionModifier
            {
                FixedPayment = 4138.2,
                Standalone = true,
                ModifierName = "1 група інвалідності",
                Type = "D"
            });

            Context.DisabilityGroups.Add(new Data.Models.PensionModifier
            {
                FixedPayment = 3702.60,
                Standalone = true,
                ModifierName = "2 група інвалідності",
                Type = "D"
            });

            Context.DisabilityGroups.Add(new Data.Models.PensionModifier
            {
                FixedPayment = 3267.00,
                Standalone = true,
                ModifierName = "3 група інвалідності",
                Type = "D"
            });

            await Context.SaveChangesAsync();
        }

        public async Task AddDefaultVariables()
        {
            Context.Settings.Add(new Data.Models.Settings
            {
                Key = nameof(BaseSettings.BaseSalary),
                Value = 2027.ToString()
            });

            Context.Settings.Add(new Data.Models.Settings
            {
                Key = nameof(BaseSettings.MinAgeFemale),
                Value = 60.ToString()
            });

            Context.Settings.Add(new Data.Models.Settings
            {
                Key = nameof(BaseSettings.MinAgeMale),
                Value = 60.ToString()
            });

            Context.Settings.Add(new Data.Models.Settings
            {
                Key = nameof(BaseSettings.MinExpFemale),
                Value = 27.ToString()
            });

            Context.Settings.Add(new Data.Models.Settings
            {
                Key = nameof(BaseSettings.MinExpMale),
                Value = 27.ToString()
            });

            Context.Settings.Add(new Data.Models.Settings
            {
                Key = nameof(BaseSettings.AvgSalary),
                Value = 7000.ToString()
            });

            await Context.SaveChangesAsync();
        }

    }
}
