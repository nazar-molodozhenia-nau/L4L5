using System.Data.Entity;

using DAL_Entities;

namespace DAL_Database {
    public class ApplicationDbContext : DbContext {
        
        public ApplicationDbContext() : base("TestContentLibrary") { Configuration.LazyLoadingEnabled = false; }
        
        public DbSet<Storage> Storage { get; set; }
        public DbSet<Folder> Folder { get; set; }
        public DbSet<File> File { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) { }

    }
}
