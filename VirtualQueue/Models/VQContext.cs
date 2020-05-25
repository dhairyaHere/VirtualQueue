namespace VirtualQueue.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class VQContext : DbContext
    {
        // Your context has been configured to use a 'VQContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'VirtualQueue.Models.VQContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'VQContext' 
        // connection string in the application configuration file.
        public VQContext()
            : base("name=VQContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VQContext, VirtualQueue.Migrations.Configuration>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<ProjectConfig> ProjectConfigs  { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}