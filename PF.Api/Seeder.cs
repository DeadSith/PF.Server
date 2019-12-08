using Microsoft.Extensions.DependencyInjection;
using PF.Api.Services;
using PF.Data;
using PF.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PF.Api
{
    public class Seeder
    {
        public static async Task EnsureDataSeeded(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var seeder = new Seeder(context);
                await seeder.Seed();
            }
        }

        public Seeder(ApplicationDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected ApplicationDbContext Context { get; set; }

        public async Task Seed()
        {
            await AddDefaultModifiers();
            await AddDefaultVariables();
            await AddSamplePositions();
            await AddSamplePeople();
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

        public async Task AddSamplePositions()
        {
            Context.Positions.Add(new Position
            {
                Name = "Position 1"
            });
            Context.Positions.Add(new Data.Models.Position
            {
                Name = "Position 2"
            });
            Context.Positions.Add(new Data.Models.Position
            {
                Name = "Position 3"
            });

            await Context.SaveChangesAsync();
        }

        public async Task AddSamplePeople()
        {
            var modifier = Context.DisabilityGroups.FirstOrDefault(g => g.ModifierName == "1 група інвалідності");
            var position1 = Context.Positions.FirstOrDefault(p => p.Name == "Position 1");
            var position2 = Context.Positions.FirstOrDefault(p => p.Name == "Position 2");
            var position3 = Context.Positions.FirstOrDefault(p => p.Name == "Position 3");

            var person1 = new Person
            {
                Name = "Name 1",
                DateOfBirth = new DateTime(1950, 2, 12),
                Sex = "F"
            };
            person1.Experiences = new List<Experience>
            {
                new Experience
                {
                    StartDate = new DateTime(1970, 1, 1),
                    EndDate = new DateTime(1990, 1, 1),
                    Salary = 100,
                    Position = position1,
                    Person = person1
                },
                new Experience
                {
                    StartDate = new DateTime(1990, 1, 2),
                    EndDate = new DateTime(2000, 1, 1),
                    Salary = 1000,
                    Position = position2,
                    Person = person1
                },
                new Experience
                {
                    StartDate = new DateTime(2000, 1, 2),
                    EndDate = new DateTime(2015, 1, 1),
                    Salary = 9000,
                    Position = position3,
                    Person = person1
                }
            };
            Context.People.Add(person1);

            var person2 = new Person
            {
                Name = "Name 2",
                DateOfBirth = new DateTime(1955, 4, 11),
                Sex = "M"
            };
            person2.Experiences = new List<Experience>
            {
                new Experience
                {
                    StartDate = new DateTime(1970, 1, 1),
                    EndDate = new DateTime(1980, 1, 1),
                    Salary = 100,
                    Position = position1,
                    Person = person2
                },
                new Experience
                {
                    StartDate = new DateTime(1980, 1, 2),
                    EndDate = new DateTime(2000, 1, 1),
                    Salary = 150,
                    Position = position2,
                    Person = person2
                },
                new Experience
                {
                    StartDate = new DateTime(2000, 1, 2),
                    EndDate = new DateTime(2015, 1, 1),
                    Salary = 9000,
                    Position = position3,
                    Person = person2
                }
            };
            Context.People.Add(person2);

            var person3 = new Person
            {
                Name = "Name 3",
                DateOfBirth = new DateTime(2000, 6, 6),
                Sex = "F",
                Modifier = modifier
            };
            Context.People.Add(person3);

            var person4 = new Person
            {
                Name = "Name 4",
                DateOfBirth = new DateTime(1996, 6, 6),
                Sex = "M"
            };
            Context.People.Add(person4);

            await Context.SaveChangesAsync();
        }

    }
}
