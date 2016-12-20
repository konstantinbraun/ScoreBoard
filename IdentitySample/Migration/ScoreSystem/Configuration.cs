namespace IdentitySample.Migration.ScoreSystem
{
    using IdentitySample.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IdentitySample.Models.ScoreSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migration\ScoreSystem";
            ContextKey = "IdentitySample.Models.ScoreSystemDbContext";
        }

        protected override void Seed(IdentitySample.Models.ScoreSystemDbContext context)
        {
            SportUnit su;
            List<SportUnit> suList = new List<SportUnit>();
            for (int i=0; i<10; i++)
            {
                su = new SportUnit()
                {
                    Name = String.Format("{0} Kyu", 10-i),
                    Description = String.Format("{0} Kyu", 10-i),
                    SportType = SportUnitType.Ranking,
                    Translations = new List<Translation>() 
                        {
                            new Translation() { Language= Language.de, Name = String.Format("{0} Kyu", 10-i)},
                            new Translation() { Language= Language.ru, Name = String.Format("{0} Кю", 10-i)}
                        }
                };

                suList.Add(su);
            }

            for (int i = 1; i <= 10; i++)
            {
                su = new SportUnit()
                {
                    Name = String.Format("{0} Dan", i),
                    Description = String.Format("{0} Dan",  i),
                    SportType = SportUnitType.Ranking,
                    Translations = new List<Translation>() 
                        {
                            new Translation() { Language= Language.de, Name = String.Format("{0} Dan", i)},
                            new Translation() { Language= Language.ru, Name = String.Format("{0} Дан", i)}
                        }
                };

                suList.Add(su);
            }

            for (int i = 1; i < 5; i++)
            {
                su = new SportUnit()
                {
                    Name = String.Format("Tatami #{0}", i),
                    Description = String.Format("Тatami{0}", i),
                    SportType = SportUnitType.Position,
                    Translations = new List<Translation>() 
                        {
                            new Translation() { Language= Language.de, Name = String.Format("Tatami Nr.{0}", i)},
                            new Translation() { Language= Language.ru, Name = String.Format("Татами №{0}", i)}
                        }
                };

                suList.Add(su);
            }

            suList.AddRange(new List<SportUnit>() {
                    new SportUnit() { Name = "Aka", Description ="#ff0000", SportType = SportUnitType.Distinction,
                        Translations = new List<Translation>() 
                            {
                                new Translation() { Language= Language.de, Name = "Aka"},
                                new Translation() { Language= Language.ru, Name = "Ака"}
                            }
                    }, 
                    new SportUnit() { Name = "Shiro", Description ="#fff", SportType = SportUnitType.Distinction,
                        Translations = new List<Translation>() 
                            {
                                new Translation() { Language= Language.de, Name = "Schiro"},
                                new Translation() { Language= Language.ru, Name = "Широ"}
                            } 
                    }
            });

            context.Groups.AddOrUpdate(
                p => p.Name,
                new Group
                {
                    Id = 1,
                    Name = "General",
                    IsPublicGroup = true,
                    GeneralUnits = new List<GeneralUnit>()
                            {
                                new GeneralUnit() { Name = "Germany", Description ="GER", GeneralType = GeneralUnitType.Country, Translations = new List<Translation>()
                                    {
                                        new Translation() { Language= Language.de, Name ="Deutschland" },
                                        new Translation() { Language= Language.ru, Name ="Германия" }
                                    }
                                },
                                new GeneralUnit() { Name = "Russia", Description ="RUS", GeneralType = GeneralUnitType.Country, Translations = new List<Translation>() 
                                    {
                                        new Translation() { Language= Language.de, Name ="Russland" },
                                        new Translation() { Language= Language.ru, Name ="Россия" }
                                    }
                                },
                                new GeneralUnit() { Name = "The 3rd place final", Description ="level_0", GeneralType = GeneralUnitType.Level, Translations = new List<Translation>() 
                                    {
                                        new Translation() { Language= Language.de, Name ="Das 3. Platz-Finale" },
                                        new Translation() { Language= Language.ru, Name ="Финал за 3 место" }
                                    }  
                                }, 
                                new GeneralUnit() { Name = "Final", Description ="level_1", GeneralType = GeneralUnitType.Level, Translations = new List<Translation>() 
                                    {
                                        new Translation() { Language= Language.de, Name ="Finale"},
                                        new Translation() { Language= Language.ru, Name ="Финал"}
                                    } 
                                }, 
                                new GeneralUnit() { Name = "Semi final", Description ="level_2", GeneralType = GeneralUnitType.Level, Translations = new List<Translation>() 
                                    {
                                        new Translation() { Language= Language.de, Name ="Halbfinale"},
                                        new Translation() { Language= Language.ru, Name ="Полуфинал"}
                                    }
                                } ,
                                new GeneralUnit() { Name = "Quarter final", Description ="level_3", GeneralType = GeneralUnitType.Level, Translations = new List<Translation>() 
                                    {
                                        new Translation() { Language= Language.de, Name ="Viertelfinale"},
                                        new Translation() { Language= Language.ru, Name ="Четвертьфинал"}
                                    }
                                },
                                new GeneralUnit() { Name = "1/8 final", Description ="level_4", GeneralType = GeneralUnitType.Level, Translations = new List<Translation>() 
                                    {
                                        new Translation() { Language= Language.de, Name ="Achtelfinale"},
                                        new Translation() { Language= Language.ru, Name ="1/8 финала"}
                                    }
                                }, 
                                new GeneralUnit() { Name = "1/16 final", Description ="level_5", GeneralType = GeneralUnitType.Level, Translations = new List<Translation>() 
                                    {
                                        new Translation() { Language= Language.de, Name ="1/16 Finale"},
                                        new Translation() { Language= Language.ru, Name ="1/16 финала"}
                                    }
                                } 

                            },
                },
                new Group
                {
                    Id = 2,
                    Name = "Karate",
                    IsPublicGroup = false,
                    SportUnits = suList
                });

        }
    }
}
