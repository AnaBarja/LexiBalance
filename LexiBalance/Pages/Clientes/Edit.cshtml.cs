using LexiBalance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiBalance.Pages.Clientes
{
    public class EditModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;
        public static List<int> numeroTelefonos;
        public static bool clienteExiste;
        public static string nombreInicial;
        public static int CPInicial;
        public static int telfInicial;

        public EditModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cliente Cliente { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cliente = await _context.Cliente.FirstOrDefaultAsync(m => m.ID == id);

            if (Cliente == null)
            {
                return NotFound();
            }

            numeroTelefonos = new List<int>();
            clienteExiste = false;
            nombreInicial = Cliente.Nombre;
            CPInicial = Cliente.CP;
            telfInicial = Cliente.Telefono;

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(Cliente.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            clienteExiste = false;
            numeroTelefonos.Remove(telfInicial);

            using (var connection = _context.Database.GetDbConnection())
            {
                int numTelefono = 0;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Telefono FROM Cliente WHERE ID=" + Cliente.ID;
                    using (var reader = command.ExecuteReader())
                    {
                        numTelefono = reader.GetInt32(0);
                    }
                }

                if (numeroTelefonos.Contains(numTelefono))
                {
                    clienteExiste = true;

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE Cliente SET Nombre = '" + nombreInicial + "', CP = " +
                           CPInicial + ", Telefono = " + telfInicial + " where ID=" + Cliente.ID;
                        var volverInicio = command.ExecuteReader();
                    }
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.ID == id);
        }
    }
}
