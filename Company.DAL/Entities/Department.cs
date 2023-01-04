using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Entities
{
    public class Department
    {
        public int Id { get; set; }
        [DisplayName("Department Code")]
        public int Code { get; set; }
        [DisplayName("Department Name")]
        [Required]
        [MaxLength(100)]
        public String? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

    }
}
