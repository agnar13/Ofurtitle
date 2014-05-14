namespace verklega.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using verklega.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<verklega.Models.AppDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(verklega.Models.AppDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            /*context.Languages.AddOrUpdate(
                p => p.TextLanguage,
                new Language
                {
                    TextLanguage = "Enska"
                },
                new Language
                {
                    TextLanguage = "Íslenska"
                }
                );
           
            context.Users.AddOrUpdate(
                u => u.UserName,
                new User { UserName = "Njörður", Rank = "Hetja" },
                new User { UserName = "Hörður", Rank = "Skúrkur" },
                new User { UserName = "Gunna", Rank ="Hetja"}
                );

            context.Subtitles.AddOrUpdate(
                t => t.Title,
                new Subtitle { U_ID = "3d6b2407-19fe-4863-b53f-fe1cd55df0aa", L_ID = 1, Title = "Die Hard", Category = "Hasar" },
                new Subtitle { U_ID = "952c445e-57df-4c28-8b1f-d67a21ea8b5e", L_ID = 2, Title = "Borat", Category = "Gaman" }
                );

            context.SubtitleLines.AddOrUpdate(
               s => s.Start,
               new SubtitleLine
               {
                   S_ID = 1,
                   Start = "00:30:00",
                   Duration = "00:00:30",
                   SLText = "Yippikayyay m..."
               },
               new SubtitleLine
               {
                   S_ID = 2,
                   Start = "00:30:00",
                   Duration = "00:00:30",
                   SLText = "Look, there is a woman in a car! Can we follow her and maybe make a sexy time with her?"
               }
                );

            context.LineTranslations.AddOrUpdate(
                l => l.SL_ID,
                new LineTranslation { SL_ID = 1, L_ID = 1, Text = "Halló Nonni"},
                new LineTranslation { SL_ID = 2, L_ID = 1, Text = "Sjáðu gugga!"}
                );*/
        }
    }
}
