using LexiBalance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LexiBalance.Pages.Personal
{
    public class DetailsModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;

        public DetailsModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        public Trabajador Trabajador { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Trabajador = await _context.Trabajador.FirstOrDefaultAsync(m => m.ID == id);

            if (Trabajador == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
