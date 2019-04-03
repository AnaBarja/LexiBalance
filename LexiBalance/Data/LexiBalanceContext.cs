using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LexiBalance.Models;

namespace LexiBalance.Models
{
    public class LexiBalanceContext : DbContext
    {
        public LexiBalanceContext (DbContextOptions<LexiBalanceContext> options)
            : base(options)
        {
        }

        public DbSet<LexiBalance.Models.Producto> Productos { get; set; }

        public DbSet<LexiBalance.Models.Trabajador> Trabajador { get; set; }

        public DbSet<LexiBalance.Models.Cliente> Cliente { get; set; }
    }
}
