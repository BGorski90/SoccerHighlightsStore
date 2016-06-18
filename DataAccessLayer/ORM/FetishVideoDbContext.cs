using SoccerHighlightsStore.BusinessLayer.Entities;
//using SoccerHighlightsStore.BusinessLayer.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;

namespace SoccerHighlightsStore.DataAccessLayer.ORM
{
    public class SoccerVideoDbContext : IdentityDbContext<User>
    {
        // Your context has been configured to use a 'SoccerVideoDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SoccerHighlightsStore.Models.SoccerVideoDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'SoccerVideoDbContext' 
        // connection string in the application configuration file.
        public SoccerVideoDbContext()
            : base("name=SoccerVideoDbContext")
        {
            Database.SetInitializer(new NullDatabaseInitializer<SoccerVideoDbContext>());
            this.Configuration.LazyLoadingEnabled = false;
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Video>().MapToStoredProcedures();
            base.OnModelCreating(modelBuilder);
        }

        public static SoccerVideoDbContext Create()
        {
            return new SoccerVideoDbContext();
        }
    }
}