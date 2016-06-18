namespace SoccerHighlightsStore.Migrations
{
    using BusinessLayer.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using BusinessLayer.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using DataAccessLayer.ORM;

    internal sealed class Configuration : DbMigrationsConfiguration<SoccerVideoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SoccerVideoDbContext context)
        {
            ApplicationUserManager userMgr =
            new ApplicationUserManager(new UserStore<User>(context));
            StoreRoleManager roleMgr =
            new StoreRoleManager(new RoleStore<IdentityRole>(context));
            string roleName = "Administrators";
            string adminName = "Admin";
            string adminPassword = "secret";
            string adminEmail = "admin@example.com";
            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new IdentityRole(roleName));
            }
            User admin = userMgr.FindByName(adminName);
            if (admin == null)
            {
                userMgr.Create(new User
                {
                    UserName = adminName,
                    Email = adminEmail,
                    RegistrationTime = DateTime.Now
                }, adminPassword);
                admin = userMgr.FindByName(adminName);
            }
            if (!userMgr.IsInRole(admin.Id, roleName))
            {
                userMgr.AddToRole(admin.Id, roleName);
            }

            string userName = "BartG";
            string userPassword = "manutd";
            string userEmail = "bgorski90@gmail.com";

            User user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new User
                {
                    UserName = userName,
                    Email = userEmail,
                    RegistrationTime = DateTime.Now
                }, userPassword);
            }
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
