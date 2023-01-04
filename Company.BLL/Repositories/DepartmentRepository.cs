using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Entities;

namespace Company.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
    {
        private readonly CompanyDbContext? _context;

        public DepartmentRepository(CompanyDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
