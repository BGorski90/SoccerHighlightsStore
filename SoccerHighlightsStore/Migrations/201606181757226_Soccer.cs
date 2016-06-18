namespace SoccerHighlightsStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Soccer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.CategoryID)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderTime = c.DateTime(nullable: false),
                        OrderValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        RegistrationTime = c.DateTime(nullable: false),
                        Country = c.String(maxLength: 20),
                        City = c.String(maxLength: 50),
                        TotalPoints = c.Int(nullable: false),
                        TotalOrders = c.Int(nullable: false),
                        TotalSpending = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        VideoID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Category = c.String(nullable: false, maxLength: 20),
                        Size = c.Int(nullable: false),
                        Format = c.String(nullable: false, maxLength: 10),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(nullable: false, maxLength: 500),
                        Length = c.Int(nullable: false),
                        Added = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VideoID)
                .Index(t => t.Title, unique: true);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.VideoOrders",
                c => new
                    {
                        Video_VideoID = c.Int(nullable: false),
                        Order_OrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Video_VideoID, t.Order_OrderID })
                .ForeignKey("dbo.Videos", t => t.Video_VideoID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID, cascadeDelete: true)
                .Index(t => t.Video_VideoID)
                .Index(t => t.Order_OrderID);
            
            CreateTable(
                "dbo.VideoUsers",
                c => new
                    {
                        Video_VideoID = c.Int(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Video_VideoID, t.User_Id })
                .ForeignKey("dbo.Videos", t => t.Video_VideoID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Video_VideoID)
                .Index(t => t.User_Id);
            
            CreateStoredProcedure(
                "dbo.Video_Insert",
                p => new
                    {
                        Title = p.String(maxLength: 50),
                        Category = p.String(maxLength: 20),
                        Size = p.Int(),
                        Format = p.String(maxLength: 10),
                        Price = p.Decimal(precision: 18, scale: 2),
                        Description = p.String(maxLength: 500),
                        Length = p.Int(),
                        Added = p.DateTime(),
                    },
                body:
                    @"INSERT [dbo].[Videos]([Title], [Category], [Size], [Format], [Price], [Description], [Length], [Added])
                      VALUES (@Title, @Category, @Size, @Format, @Price, @Description, @Length, @Added)
                      
                      DECLARE @VideoID int
                      SELECT @VideoID = [VideoID]
                      FROM [dbo].[Videos]
                      WHERE @@ROWCOUNT > 0 AND [VideoID] = scope_identity()
                      
                      SELECT t0.[VideoID]
                      FROM [dbo].[Videos] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[VideoID] = @VideoID"
            );
            
            CreateStoredProcedure(
                "dbo.Video_Update",
                p => new
                    {
                        VideoID = p.Int(),
                        Title = p.String(maxLength: 50),
                        Category = p.String(maxLength: 20),
                        Size = p.Int(),
                        Format = p.String(maxLength: 10),
                        Price = p.Decimal(precision: 18, scale: 2),
                        Description = p.String(maxLength: 500),
                        Length = p.Int(),
                        Added = p.DateTime(),
                    },
                body:
                    @"UPDATE [dbo].[Videos]
                      SET [Title] = @Title, [Category] = @Category, [Size] = @Size, [Format] = @Format, [Price] = @Price, [Description] = @Description, [Length] = @Length, [Added] = @Added
                      WHERE ([VideoID] = @VideoID)"
            );
            
            CreateStoredProcedure(
                "dbo.Video_Delete",
                p => new
                    {
                        VideoID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Videos]
                      WHERE ([VideoID] = @VideoID)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Video_Delete");
            DropStoredProcedure("dbo.Video_Update");
            DropStoredProcedure("dbo.Video_Insert");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.VideoUsers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.VideoUsers", "Video_VideoID", "dbo.Videos");
            DropForeignKey("dbo.VideoOrders", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.VideoOrders", "Video_VideoID", "dbo.Videos");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.VideoUsers", new[] { "User_Id" });
            DropIndex("dbo.VideoUsers", new[] { "Video_VideoID" });
            DropIndex("dbo.VideoOrders", new[] { "Order_OrderID" });
            DropIndex("dbo.VideoOrders", new[] { "Video_VideoID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Videos", new[] { "Title" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropIndex("dbo.Categories", new[] { "Name" });
            DropTable("dbo.VideoUsers");
            DropTable("dbo.VideoOrders");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Videos");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Orders");
            DropTable("dbo.Categories");
        }
    }
}
