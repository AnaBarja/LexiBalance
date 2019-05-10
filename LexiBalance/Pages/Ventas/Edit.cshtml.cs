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
        public static string trabajadorInicial;
        public static string clienteInicial;
        public static bool segundaVez;
        public static bool productoBorrado;

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

            segundaVez = false;
            productoBorrado = false;
            cantidadInicial = Venta.Cantidad;
            trabajadorInicial = Venta.Trabajador;
            clienteInicial = Venta.Cliente;

            productos = new List<string>();
            trabajadores = new List<string>();
            clientes = new List<string>();

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {

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

            bool siguiente = false;

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                int nuevaCantidadVendida = 0;
                int cantidadProducto = 0;

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format("SELECT Cantidad FROM Venta where ID = {0}", Venta.ID);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(0))
                            nuevaCantidadVendida = reader.GetInt32(0);
                    }
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format("select cantidad from Productos where ID = (select SUBSTR(Producto, " +
                        "INSTR(Producto,'#')+1,INSTR(Producto,'.')-2) from Venta where ID = {0})", Venta.ID);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            cantidadProducto = reader.GetInt32(0);
                            siguiente = true;
                        }
                    }
                }

                if (siguiente)
                {
                    int nuevacantidad = cantidadInicial + cantidadProducto - nuevaCantidadVendida;
                    if ((cantidadInicial + cantidadProducto) >= nuevaCantidadVendida && nuevaCantidadVendida > 0)
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = string.Format("UPDATE Productos SET Cantidad = {0} where ID = " +
                                "(select id from Productos where ID = (select SUBSTR(Producto, " +
                            "INSTR(Producto,'#')+1,INSTR(Producto,'.')-2) from Venta where ID = {1}))", nuevacantidad, Venta.ID);
                            var update = command.ExecuteReader();
                        }
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = string.Format("UPDATE Venta SET Fecha = DATETIME('now', 'localtime') WHERE ID = {0}", Venta.ID);
                            var fecha = command.ExecuteReader();
                        }
                    }
                    else
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = string.Format("UPDATE Venta SET Cantidad = {0}, Trabajador = '{1}', " +
                                "Cliente = '{2}' where ID = {3}", cantidadInicial, trabajadorInicial, clienteInicial, Venta.ID);
                            var volverInicio = command.ExecuteReader();
                        }
                        segundaVez = true;
                        return Page();
                    }
                }
                else
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = string.Format("UPDATE Venta SET Cantidad = {0}, Trabajador = '{1}', " +
                                "Cliente = '{2}' where ID = {3}", cantidadInicial, trabajadorInicial, clienteInicial, Venta.ID);
                        var volverInicio = command.ExecuteReader();
                    }
                    productoBorrado = true;
                    return Page();
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
