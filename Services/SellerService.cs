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

        //public List<Seller> FindAll()
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        // inserir novo vendedor no banco de dados

        //public void Insert(Seller obj)
        public async Task InsertAsync(Seller obj)
        {
            //obj.Department = _context.Department.First();
            _context.Add(obj);
           // _context.SaveChanges();
            await _context.SaveChangesAsync();
        }

        //public Seller FindById(int id)
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id); 
            // Eager loading => (.Include(obj => obj.Department) => Carrega outros obj associados ao obj principal...

        }
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                //throw new IntegrityException(e.Message);
                throw new IntegrityException("Can't delete seller because he/she has sales");
            }
        }
        
        //public void Update (Seller obj)
        public async Task UpdateAsync (Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id); // tiramos de if() para usar await e melhorar a leitura
            //if(!_context.Seller.Any(x => x.Id == obj.Id))
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
            _context.Update(obj);
            //_context.SaveChanges();
            await _context.SaveChangesAsync();

            }
            catch(DbCuncurrencyException e)
            {
                throw new DbCuncurrencyException(e.Message);
            }
        }
    }
}
