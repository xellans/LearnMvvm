using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{
    internal class UsersPeopleDb : DbContext
    {
        private readonly string connectionString;

        public UsersPeopleDb(string dataBaseNamePath)
        {
            connectionString = $"Data Source={dataBaseNamePath}";
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Person> People { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id);
                entity.Property(e => e.Name);
                entity.Property(e => e.IsAuthorized);

                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("people");

                entity.Property(e => e.Id);
                entity.Property(e => e.Name);
                entity.Property(e => e.Age);

                entity.HasKey(e => e.Id);

                entity.HasData(InitData.People);
            });
        }
    }

    public static class InitData
    {
        public static readonly IEnumerable<Person> People = @"
1 Алиса 23
5 Екатерина 56
3 Василий 45
7 Андрей 12
2 Пётр 34
6 Инна 54
44 Вика 99
33 Жанна 77
11 Ксюша 87
22 Анатолий 18"
            .Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .Select(args => new Person() { Id = int.Parse(args[0]), Name = args[1], Age = int.Parse(args[2]) })
            .ToArray()
            .Select(Person => Person);
    }

}
