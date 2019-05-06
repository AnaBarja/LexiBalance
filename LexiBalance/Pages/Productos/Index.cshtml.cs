using LexiBalance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace LexiBalance.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;

        public IList<Producto> Productos { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Buscar { get; set; }

        public IndexModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            var prod = from Producto m in _context.Productos select m;
            if (!string.IsNullOrEmpty(Buscar))
            {
                prod = prod.Where(s => s.Nombre.Contains(Buscar));
            }

            Productos = await prod.ToListAsync();
        }
    }
}
