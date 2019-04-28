﻿using LexiBalance.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiBalance.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;

        public IndexModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        public IList<Producto> Productos { get; set; }

        public async Task OnGetAsync()
        {
            Productos = await _context.Productos.ToListAsync();
        }
    }
}
