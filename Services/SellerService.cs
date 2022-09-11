using ControleDeVendas.Data;
using ControleDeVendas.Models;
using ControleDeVendas.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
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
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id); 
            // Eager loading => (.Include(obj => obj.Department) => Carrega outros obj associados ao obj principal...

        }
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
        
        public void update (Seller obj)
        {
            if(!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
            _context.Update(obj);
            _context.SaveChanges();

            }
            catch(DbCuncurrencyException e)
            {
                throw new DbCuncurrencyException(e.Message);
            }
        }
    }
}
