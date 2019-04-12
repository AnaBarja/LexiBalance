using LexiBalance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiBalance.Pages.Ventas
{
    public class CreateModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;

        public static List<string> productos;

        public CreateModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            productos = new List<string>();
            var connection = new SqliteConnection();

            connection.ConnectionString = "DataSource=DefaultConnection";
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                var selectCommand = connection.CreateCommand();
                selectCommand.Transaction = transaction;
                selectCommand.CommandText = "SELECT Nombre FROM Productos";
                using (var reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add(reader.GetString(0));
                    }
                }
                transaction.Commit();
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