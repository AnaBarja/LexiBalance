using LexiBalance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


namespace LexiBalance.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;

        public IList<Cliente> Cliente { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Buscar { get; set; }

        public IndexModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            var client = from Cliente m in _context.Cliente select m;
            if (!string.IsNullOrEmpty(Buscar))
            {
                client = client.Where(s => s.Telefono.ToString().Contains(Buscar));
            }

            Cliente = await client.ToListAsync();
        }
    }
}
