using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace LexiBalance.Models
{
    public class LexiBalanceContext : DbContext

    {
        public LexiBalanceContext(DbContextOptions<LexiBalanceContext> options)
     : base(options)
        {
        }
        public LexiBalanceContext()
        {

        }

        public DbSet<Models.Producto> Productos { get; set; }

        public DbSet<Models.Trabajador> Trabajador { get; set; }

        public DbSet<Models.Cliente> Cliente { get; set; }

        public DbSet<LexiBalance.Models.Venta> Venta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string directory = Environment.GetEnvironmentVariable("homepath");
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = directory + "\\LexiBalance.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }

    }
}
