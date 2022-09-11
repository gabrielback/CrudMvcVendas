using ControleDeVendas.Data;
using ControleDeVendas.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeVendas.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }
       // public <List<Department> FindAll()
        public async Task<List<Department>> FindAllAsync()
        {
            //return _context.Department.OrderBy(x => x.Name).ToList();
            return await _context.Department.OrderBy(x => x.Name).ToListAsync(); //using Microsoft.EntityFrameworkCore;
        }
    }
}
