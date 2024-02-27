using Microsoft.EntityFrameworkCore;

namespace DataBase
{
    public class Context : DbContext
    {
        internal DbSet<User> User { get; set; } = null!;
        internal DbSet<Person> Person { get; set; } = null!;
        internal DbSet<Product> Products { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = "Data Base";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            optionsBuilder.UseSqlite(@$"Data Source={path}\\Data Base.db");
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>(e =>
        //    {
        //        e.Property(x => x.Id);
        //    });
        //}
    }
}