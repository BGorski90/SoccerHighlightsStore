namespace SoccerHighlightsStore.Migrations
{
    using BusinessLayer.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ORM.SoccerVideoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ORM.SoccerVideoDbContext context)
        {
            context.Categories.AddOrUpdate(new Category { Name = "Premier League" });
            context.Categories.AddOrUpdate(new Category { Name = "Primera Division" });
            context.Categories.AddOrUpdate(new Category { Name = "Serie A" });

            #region Videos
            context.Videos.AddOrUpdate(new Video
            {
                Title = "Liverpool FC - Manchester United",
                Category = "Premier League",
                Description = "The latest North Derby at Anfield",
                Size = 949,
                Price = 2.99m,
                Length = 32,
                Added = DateTime.Now,
                Format = "WMV"
            });

            context.Videos.AddOrUpdate(new Video
            {
                Title = "Real Madrid - FC Barcelona",
                Category = "Primera Division",
                Description = "The latest Gran Derbi at Santiago Bernabeu",
                Size = 935,
                Price = 2.99m,
                Length = 32,
                Added = DateTime.Now,
                Format = "WMV"
            });

            context.Videos.AddOrUpdate(new Video
            {
                Title = "Inter Milan - AC Milan",
                Category = "Serie A",
                Description = "The latest Milan derby at San Siro",
                Size = 965,
                Price = 2.99m,
                Length = 33,
                Added = DateTime.Now,
                Format = "WMV"
            });
            #endregion

            base.Seed(context);
        }
    }
}
