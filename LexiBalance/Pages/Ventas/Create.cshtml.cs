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
                    command.CommandText = "SELECT Nombre FROM Productos";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productos.Add(reader.GetString(0));
                        }
                    }

                    command.CommandText = "SELECT Nombre FROM Trabajador";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            trabajadores.Add(reader.GetString(0));
                        }
                    }

                    command.CommandText = "SELECT Nombre FROM Cliente";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clientes.Add(reader.GetString(0));
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

            return RedirectToPage("./Index");
        }
    }
}