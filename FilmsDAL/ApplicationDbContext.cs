using Microsoft.EntityFrameworkCore;
using ApiDemoFilms.Model;
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        } 

        static readonly string connectionString = "User Id=root;Host=localhost;Database=db;Persist Security Info=True;Password=password";

        //var builder = WebApplication.CreateBuilder(args);
        //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Actor> Actors { get; set; }

    }

