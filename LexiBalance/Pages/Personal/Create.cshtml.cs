using LexiBalance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiBalance.Pages.Personal
{
    public class CreateModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;

        public static List<string> DNIs;
        public static bool trabajadorExiste;

        public CreateModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            DNIs = new List<string>();
            trabajadorExiste = false;

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT DNI FROM Trabajador";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                DNIs.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }

            return Page();
        }

        [BindProperty]
        public Trabajador Trabajador { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Trabajador.Add(Trabajador);
            await _context.SaveChangesAsync();

            using (var connection = _context.Database.GetDbConnection())
            {
                string numeroDNI = "";
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT DNI FROM Trabajador order by ID desc limit 1";
                    using (var reader = command.ExecuteReader())
                    {
                        numeroDNI = reader.GetString(0);
                    }
                }

                if (DNIs.Contains(numeroDNI))
                {
                    trabajadorExiste = true;

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM Trabajador where ID = (select ID from Trabajador order by ID desc limit 1)";
                        var delete = command.ExecuteReader();
                    }
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}