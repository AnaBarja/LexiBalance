using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LexiBalance.Pages
{
    public class IndexModel : PageModel
    {
        private readonly LexiBalance.Models.LexiBalanceContext _context;

        public static string mejorTrabajador;
        public static List<string> producto;
        public static List<int> cantidadProducto;


        public IndexModel(LexiBalance.Models.LexiBalanceContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            producto = new List<string>();
            cantidadProducto = new List<int>();
            mejorTrabajador = "";

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(Trabajador), Trabajador from Venta group by Trabajador " +
                        "order by COUNT(Trabajador) desc limit 1;";
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(1) && !reader.IsDBNull(0))
                            mejorTrabajador = reader.GetString(1);
                    }

                    command.CommandText = "SELECT Producto, Cantidad from Venta order by Fecha desc limit 3;";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0) && !reader.IsDBNull(1))
                            {
                                producto.Add(reader.GetString(0));
                                cantidadProducto.Add(reader.GetInt32(1));
                            }
                        }
                    }
                }
            }
        }
    }
}
