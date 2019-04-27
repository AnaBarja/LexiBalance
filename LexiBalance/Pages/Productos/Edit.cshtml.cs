using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LexiBalance.Models;

namespace LexiBalance.Pages.Productos
{
    public class EditModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;

        public static string[] colores;

        public EditModel(LexiBalance.Models.LexiBalanceContext context)
        {
            colores = Enum.GetNames(typeof(Producto.Colores));
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Productos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductosExists(Productos.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductosExists(int id)
        {
            return _context.Productos.Any(e => e.ID == id);
        }
    }
}
