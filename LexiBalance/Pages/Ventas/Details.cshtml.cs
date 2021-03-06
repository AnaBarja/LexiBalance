﻿using LexiBalance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LexiBalance.Pages.Ventas
{
    public class DetailsModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;

        public DetailsModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        public Venta Venta { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Venta = await _context.Venta.FirstOrDefaultAsync(m => m.ID == id);

            if (Venta == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
