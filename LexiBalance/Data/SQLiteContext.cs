using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace LexiBalance.Data
{
    public class SQLiteContext : DbContext
    {
        public DbSet<Models.Producto> Productos { get; set; }

        public DbSet<Models.Trabajador> Trabajador { get; set; }

        public DbSet<Models.Cliente> Cliente { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "C:\\Users\\Ana\\Desktop\\ProyectoDAM\\LexiBalance.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
    }
}
