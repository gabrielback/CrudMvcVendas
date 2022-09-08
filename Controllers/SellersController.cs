using Microsoft.AspNetCore.Mvc;

namespace ControleDeVendas.Controllers
{
    public class SellersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
