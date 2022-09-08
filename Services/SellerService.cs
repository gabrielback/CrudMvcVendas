using ControleDeVendas.Data;
using ControleDeVendas.Models.ViewModels;

namespace ControleDeVendas.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context; // criando dependência para Dbcontext e evitando que ela seja alterada com readonly 

        // criar um construtor de dependência para que a injeção de dependência ocorra.
        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }
    }
}
