using LexiBalance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiBalance.Pages.Ventas
{
    public class CreateModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;

        public static List<string> productos;
        public static List<string> trabajadores;
        public static List<string> clientes;
        public static bool segundaVez;
        public static bool productoBorrado;

        public CreateModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            productos = new List<string>();
            trabajadores = new List<string>();
            clientes = new List<string>();
            segundaVez = false;
            productoBorrado = false;

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT ID, Nombre, Cantidad FROM Productos where cantidad > 0";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(1) && !reader.IsDBNull(0) && !reader.IsDBNull(2))
                            {
                                productos.Add("#" + reader.GetInt16(0) + ". " + reader.GetString(1) + " (" + reader.GetInt16(2) + " uds.)");
                            }
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

        [BindProperty]
        public Venta Venta { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            int NUMERO = 0;
            int CANTIDAD = 0;
            bool siguiente = false;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Venta.Producto = Venta.Producto.Substring(0, Venta.Producto.LastIndexOf("("));

            _context.Venta.Add(Venta);
            await _context.SaveChangesAsync();

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Cantidad FROM Venta order by ID desc limit 1";
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(0))
                            NUMERO = reader.GetInt32(0);
                    }
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select cantidad from Productos where ID = (select SUBSTR(Producto, " +
                        "INSTR(Producto,'#')+1,INSTR(Producto,'.')-2) from Venta order by ID desc limit 1)";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            CANTIDAD = reader.GetInt32(0);
                            siguiente = true;
                        }
                    }
                }

                if (siguiente)
                {
                    if (CANTIDAD < NUMERO || NUMERO < 1)
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "DELETE FROM Venta where ID = (select ID from Venta order by ID desc limit 1)";
                            var delete = command.ExecuteReader();
                        }
                        segundaVez = true;
                        return Page();
                    }
                    else
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = string.Format("UPDATE Productos SET Cantidad = Cantidad - {0} where ID = " +
                                "(select id from Productos where ID = (select SUBSTR(Producto, " +
                            "INSTR(Producto,'#')+1,INSTR(Producto,'.')-2) from Venta order by ID desc limit 1))", NUMERO);
                            var update = command.ExecuteReader();
                        }
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "UPDATE Venta SET Fecha = DATETIME('now', 'localtime') WHERE ID = " +
                        "(select ID from Venta order by ID desc limit 1)";
                            var fecha = command.ExecuteReader();

                        }
                    }
                }
                else
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM Venta where ID = (select ID from Venta order by ID desc limit 1)";
                        var delete = command.ExecuteReader();
                    }
                    productoBorrado = true;
                    return Page();
                }
            }
            return RedirectToPage("./Index");
        }
    }
}