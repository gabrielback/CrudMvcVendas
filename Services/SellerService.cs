using ControleDeVendas.Data;
using ControleDeVendas.Models;

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

        // inserir novo vendedor no banco de dados

        public void Insert(Seller obj)
        {
            //obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);

        }
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
