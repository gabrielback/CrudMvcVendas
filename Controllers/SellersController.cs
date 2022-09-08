using ControleDeVendas.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeVendas.Controllers
{
    public class SellersController : Controller
    {
        // Declarar dependencia do SellerService
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }
    }
}
