//using SoccerHighlightsStore.BusinessLayer.Identity;
//using SoccerHighlightsStore.BusinessLayer.Entities;
////using SoccerHighlightsStore.DataAccessLayer.ORM;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SoccerHighlightsStore.BusinessLayer.Identity
//{
//    public class SoccerVideoDbInitializer : NullDatabaseInitializer<SoccerVideoDbContext>
//    {
//        //protected override void Seed(SoccerVideoDbContext context)
//        //{
//        //    ApplicationUserManager userMgr =
//        //    new ApplicationUserManager(new UserStore<User>(context));
//        //    StoreRoleManager roleMgr =
//        //    new StoreRoleManager(new RoleStore<StoreRole>(context));
//        //    string roleName = "Administrators";
//        //    string adminName = "Admin";
//        //    string adminPassword = "secret";
//        //    string adminEmail = "admin@example.com";
//        //    if (!roleMgr.RoleExists(roleName))
//        //    {
//        //        roleMgr.Create(new StoreRole(roleName));
//        //    }
//        //    User admin = userMgr.FindByName(adminName);
//        //    if (admin == null)
//        //    {
//        //        userMgr.Create(new User
//        //        {
//        //            UserName = adminName,
//        //            Email = adminEmail,
//        //            RegistrationTime = DateTime.Now
//        //        }, adminPassword);
//        //        admin = userMgr.FindByName(adminName);
//        //    }
//        //    if (!userMgr.IsInRole(admin.Id, roleName))
//        //    {
//        //        userMgr.AddToRole(admin.Id, roleName);
//        //    }

//        //    string userName = "BartG";
//        //    string userPassword = "manutd";
//        //    string userEmail = "bgorski90@gmail.com";

//        //    User user = userMgr.FindByName(userName);
//        //    if (user == null)
//        //    {
//        //        userMgr.Create(new User
//        //        {
//        //            UserName = userName,
//        //            Email = userEmail,
//        //            RegistrationTime = DateTime.Now
//        //        }, userPassword);
//        //    }
//        //    if (!context.Categories.Any())
//        //    {
//        //        context.Categories.Add(new Category { Name = "Foot domination" });
//        //        context.Categories.Add(new Category { Name = "Ass worship" });
//        //        context.Categories.Add(new Category { Name = "Facesitting" });
//        //    }
//        //    if (!context.Videos.Any())
//        //    {
//        //        context.Videos.Add(new Video
//        //        {
//        //            Title = "How I Like My Feet Licked",
//        //            Category = "Foot domination",
//        //            Description = "Gabriela obediently smells and worships Bruna's delicious feet",
//        //            Size = 949,
//        //            Price = 22.99m,
//        //            Length = 32,
//        //            Added = DateTime.Now,
//        //            Format = "WMV"
//        //        });

//        //        context.Videos.Add(new Video
//        //        {
//        //            Title = "Kneeling Under Lara Feet",
//        //            Category = "Foot domination",
//        //            Description = "Gabriela obediently smells and worships Lara's delicious feet",
//        //            Size = 935,
//        //            Price = 21.99m,
//        //            Length = 32,
//        //            Added = DateTime.Now,
//        //            Format = "WMV"
//        //        });

//        //        context.Videos.Add(new Video
//        //        {
//        //            Title = "Lick Sharon Sweaty Black Feet",
//        //            Category = "Foot domination",
//        //            Description = "Gabriela obediently smells and worships Sharon's delicious feet",
//        //            Size = 965,
//        //            Price = 22.99m,
//        //            Length = 33,
//        //            Added = DateTime.Now,
//        //            Format = "WMV"
//        //        });
//        //    }
//        //    base.Seed(context);
//        //}
//    }
//}
