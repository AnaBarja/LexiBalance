using LexiBalance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LexiBalance.Pages.Ventas
{
    public class DeleteModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;

        public static int cantidadProductoDevolver;
        public static string nombreProduc;

        public DeleteModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        [BindProperty]
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

            cantidadProductoDevolver = Venta.Cantidad;
            nombreProduc = Venta.Producto;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Venta = await _context.Venta.FindAsync(id);

            if (Venta != null)
            {
                _context.Venta.Remove(Venta);
                await _context.SaveChangesAsync();
            }

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Productos SET CANTIDAD = (Cantidad + " + cantidadProductoDevolver + ") where ID = " +
                        "(select SUBSTR('" + nombreProduc + "',INSTR('" + nombreProduc + "','#')+1,INSTR('" + nombreProduc + "','.')-2))";
                    var añadirDeNuevo = command.ExecuteReader();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
