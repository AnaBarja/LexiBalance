using LexiBalance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiBalance.Pages.Ventas
{
    public class EditModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;

        public static List<string> productos;
        public static List<string> trabajadores;
        public static List<string> clientes;
        public static int cantidadInicial;

        public EditModel(LexiBalance.Models.LexiBalanceContext context)
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

            cantidadInicial = Venta.Cantidad;
            productos = new List<string>();
            trabajadores = new List<string>();
            clientes = new List<string>();

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT ID, Nombre FROM Productos";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(1) && !reader.IsDBNull(0))
                                productos.Add("#" + reader.GetInt16(0) + ". " + reader.GetString(1));
                        }
                    }

                    command.CommandText = "SELECT ID, Nombre FROM Trabajador";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(1) && !reader.IsDBNull(0))
                                trabajadores.Add("#" + reader.GetInt16(0) + ". " + reader.GetString(1));
                        }
                    }

                    command.CommandText = "SELECT ID, Nombre FROM Cliente";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(1) && !reader.IsDBNull(0))
                                clientes.Add("#" + reader.GetInt16(0) + ". " + reader.GetString(1));
                        }
                    }
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Venta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentaExists(Venta.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                int nuevaCantidadVendida = 0;
                int cantidadProducto = 0;

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Cantidad FROM Venta where ID=" + Venta.ID;
                    using (var reader = command.ExecuteReader())
                    {
                        nuevaCantidadVendida = reader.GetInt32(0);
                    }
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select cantidad from Productos where Nombre = (select substr(Producto, instr(Producto, ' ') " +
                        "+ 1) from Venta where ID=" + Venta.ID + ")";

                    using (var reader = command.ExecuteReader())
                    {
                        cantidadProducto = reader.GetInt32(0);
                    }
                }

                int nuevacantidad = cantidadInicial + cantidadProducto - nuevaCantidadVendida;
                if (cantidadProducto < nuevacantidad || nuevaCantidadVendida < 1 || nuevacantidad < 0)
                {
                    return Page();
                }
                else
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE Productos SET Cantidad =" + nuevacantidad + " where ID = " +
                            "(select id from Productos where Nombre =(select substr(Producto, instr(Producto, ' ') + 1) " +
                            "from Venta where ID=" + Venta.ID + "))";
                        var update = command.ExecuteReader();
                    }
                }
            }

            return RedirectToPage("./Index");
        }

        private bool VentaExists(int id)
        {
            return _context.Venta.Any(e => e.ID == id);
        }
    }
}
