using Company.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IEmployeeRepository _employeeRepository, IDepartmentRepository _departmentRepository)
        {
            EmployeeRepository = _employeeRepository;
            DepartmentRepository = _departmentRepository;
        }

        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set ; }
    }
}
