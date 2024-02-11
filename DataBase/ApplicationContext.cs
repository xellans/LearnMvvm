using DataBase.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> User { get; set; } = null!;
        public DbSet<People> People { get; set; } = null!;
        public DbSet<Product> Product {  get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = "Data Base";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            optionsBuilder.UseSqlite(@$"Data Source={path}\\Data Base.db");
        }
    }
}
