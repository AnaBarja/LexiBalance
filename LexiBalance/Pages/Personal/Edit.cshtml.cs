using LexiBalance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiBalance.Pages.Personal
{
    public class EditModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;
        public static List<string> DNIs;
        public static bool trabajadorExiste;
        public static string nombreInicial;
        public static string dniInicial;
        public static int telfInicial;
        public static string direccionInicial;

        public EditModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Trabajador Trabajador { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Trabajador = await _context.Trabajador.FirstOrDefaultAsync(m => m.ID == id);

            if (Trabajador == null)
            {
                return NotFound();
            }

            DNIs = new List<string>();
            trabajadorExiste = false;
            nombreInicial = Trabajador.Nombre;
            dniInicial = Trabajador.DNI;
            telfInicial = Trabajador.Telefono;
            direccionInicial = Trabajador.Direccion;

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Trabajador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrabajadorExists(Trabajador.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            trabajadorExiste = false;
            DNIs.Remove(dniInicial);

            using (var connection = _context.Database.GetDbConnection())
            {
                string numDNI = "";
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format("SELECT DNI FROM Trabajador WHERE ID = {0}", Trabajador.ID);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            numDNI = reader.GetString(0);
                        }
                    }
                }

                if (DNIs.Contains(numDNI))
                {
                    trabajadorExiste = true;

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = string.Format("UPDATE Trabajador SET Nombre = '{0}', DNI = '{1}', Telefono = {2}, " +
                            "Direccion = '{3}' where ID = {4}", nombreInicial, dniInicial, telfInicial, direccionInicial, Trabajador.ID);
                        var volverInicio = command.ExecuteReader();
                    }
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TrabajadorExists(int id)
        {
            return _context.Trabajador.Any(e => e.ID == id);
        }
    }
}
