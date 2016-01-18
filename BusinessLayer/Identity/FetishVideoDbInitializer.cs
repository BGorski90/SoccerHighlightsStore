using Storefront.BusinessLayer.Entities;
using Storefront.BusinessLayer.ORM;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Identity
{
    public class SoccerVideoDbInitializer : DropCreateDatabaseIfModelChanges<SoccerVideoDbContext>
    {
        protected override void Seed(SoccerVideoDbContext context)
        {
            ApplicationUserManager userMgr =
            new ApplicationUserManager(new UserStore<User>(context));
            StoreRoleManager roleMgr =
            new StoreRoleManager(new RoleStore<StoreRole>(context));
            string roleName = "Administrators";
            string adminName = "Admin";
            string adminPassword = "secret";
            string adminEmail = "admin@example.com";
            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new StoreRole(roleName));
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
            if (!context.Videos.Any())
            {
                context.Videos.Add(new Video
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

                context.Videos.Add(new Video
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

                context.Videos.Add(new Video
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
            }
            base.Seed(context);
        }
    }
}
