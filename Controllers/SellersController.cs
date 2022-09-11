using ControleDeVendas.Models;
using ControleDeVendas.Models.ViewModels;
using ControleDeVendas.Services;
using ControleDeVendas.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        //public IActionResult Index()
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        // Criar ação create
        // recebe um objeto vendedor que veio na requisição... basta coloca-lo como parametro

        // Anotation => Define como post e não de Get
        // ataque crff... Para evitar que alguem aproveita sua autentição para enviar dados maliciosos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
  /*          if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
*/
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));// 1:nameof(Index)||2:("Index)... 1-Mais indicado por conta de modificações futuras
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            // pegar quem é o obj que sera deletado

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Seller not found" });
            }

            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            // pegar quem é o obj que sera deletado

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {


            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            /*if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
*/
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e) // supertipo 
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

            /*
             * Desnecessário, uma vez que pode ser tratado com o supertipo ApplicationException
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbCuncurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            */
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
