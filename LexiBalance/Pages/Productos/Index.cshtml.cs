using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LexiBalance.Models;

namespace LexiBalance.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;

        public IndexModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        public IList<Producto> Productos { get;set; }

        public async Task OnGetAsync()
        {
            Productos = await _context.Productos.ToListAsync();
        }
    }
}
