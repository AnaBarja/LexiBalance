using LexiBalance.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LexiBalance.Controllers
{
    public class HomeController : Controller
    {
        private readonly LexiBalanceContext sqlite = new LexiBalanceContext();

        public IActionResult Index()
        {
            var category = sqlite.Productos.First();
            return View(category);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        //public IActionResult Error()
        //{

        //}
    }
}
