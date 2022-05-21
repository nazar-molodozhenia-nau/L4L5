using System.Data.Entity;

using DAL_Entities;

namespace DAL_Database {
    public class ApplicationDbContext : DbContext {
        
        public ApplicationDbContext() : base("ContentLibrary") { Configuration.LazyLoadingEnabled = false; }
        
        public DbSet<Storage> Storage { get; set; }
        public DbSet<Folder> Folder { get; set; }
        public DbSet<File> File { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            //modelBuilder.Entity<Storage>()
            //.HasMany(h => h.Rooms)
            //.WithOne(r => r.Hotel)
            //.HasForeignKey(r => r.HotelId);
            //modelBuilder.Entity<RoomEntity>()
            //.HasOne(r => r.Customer)
            //.WithOne(c => c.Room)
            //.HasForeignKey<CustomerEntity>(c => c.RoomId);
            //modelBuilder.Entity<CustomerEntity>()
                //.HasOne(c => c.Room)
                //.WithOne(r => r.Customer)
                //.IsRequired();
        }

    }
}
