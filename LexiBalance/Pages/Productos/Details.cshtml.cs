﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LexiBalance.Models;

namespace LexiBalance.Pages.Productos
{
    public class DetailsModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;

        public DetailsModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        public Producto Productos { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Productos = await _context.Productos.FirstOrDefaultAsync(m => m.ID == id);

            if (Productos == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
