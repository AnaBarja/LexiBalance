using LexiBalance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace LexiBalance.Pages.Personal
{
    public class IndexModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;

        public IList<Trabajador> Trabajador { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Buscar { get; set; }

        public IndexModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            var trab = from Trabajador m in _context.Trabajador select m;
            if (!string.IsNullOrEmpty(Buscar))
            {
                trab = trab.Where(s => s.DNI.Contains(Buscar));
            }

            Trabajador = await trab.ToListAsync();
        }
    }
}
