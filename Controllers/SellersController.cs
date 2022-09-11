using ControleDeVendas.Models;
using ControleDeVendas.Models.ViewModels;
using ControleDeVendas.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeVendas.Controllers
{
    public class SellersController : Controller
    {
        // Declarar dependencia do SellerService
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        // Criar ação create
        // recebe um objeto vendedor que veio na requisição... basta coloca-lo como parametro

        // Anotation => Define como post e não de Get
        // ataque crff... Para evitar que alguem aproveita sua autentição para enviar dados maliciosos
        [HttpPost] 
        [ValidateAntiForgeryToken] 
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));// 1:nameof(Index)||2:("Index)... 1-Mais indicado por conta de modificações futuras
        }
        
    }
}
