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

        public CreateModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            productos = new List<string>();
            trabajadores = new List<string>();
            clientes = new List<string>();

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT ID, Nombre FROM Productos where cantidad > 0";
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

        [BindProperty]
        public Venta Venta { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Venta.Add(Venta);
            await _context.SaveChangesAsync();

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    int NUMERO = 0;
                    command.CommandText = "SELECT Cantidad FROM Venta order by ID desc limit 1";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NUMERO = reader.GetInt32(0);
                        }
                    }

                    command.CommandText = "UPDATE Productos SET Cantidad = Cantidad -" + NUMERO + " where ID = " +
                        "(select id from Productos where Nombre =(select substr(Producto, instr(Producto, ' ') + 1) " +
                        "from Venta order by ID desc limit 1))";
                    var update = command.ExecuteReader();
                }
            }
            return RedirectToPage("./Index");
        }
    }
}