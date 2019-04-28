using LexiBalance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace LexiBalance.Pages.Productos
{
    public class CreateModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;

        public static string[] colores;

        public CreateModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            colores = Enum.GetNames(typeof(Producto.Colores));
            return Page();
        }

        [BindProperty]
        public Producto Productos { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Productos.Add(Productos);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}