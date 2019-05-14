using LexiBalance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiBalance.Pages.Clientes
{
    public class CreateModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;

        public static List<int> numeroTelefonos;
        public static bool clienteExiste;

        public CreateModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            numeroTelefonos = new List<int>();
            clienteExiste = false;

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Telefono FROM Cliente";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                numeroTelefonos.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }
            }

            return Page();
        }

        [BindProperty]
        public Cliente Cliente { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Cliente.Add(Cliente);
            await _context.SaveChangesAsync();

            using (var connection = _context.Database.GetDbConnection())
            {
                int numTelefono = 0;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Telefono FROM Cliente order by ID desc limit 1";
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            numTelefono = reader.GetInt32(0);
                        }
                    }
                }

                if (numeroTelefonos.Contains(numTelefono))
                {
                    clienteExiste = true;

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM Cliente where ID = (select ID from Cliente order by ID desc limit 1)";
                        var delete = command.ExecuteReader();
                    }
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}