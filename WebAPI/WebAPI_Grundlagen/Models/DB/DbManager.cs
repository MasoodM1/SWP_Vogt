using ArticleLibrary;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_Grundlagen.Models.DB
{
    public class DbManager : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // für den Pomelo-MySQL-Treiber
            string connectionString = "Server=localhost;database=WebAPI_5AHWII;user=root;password=root";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}

