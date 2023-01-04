using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? ImageName { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

    }
}
