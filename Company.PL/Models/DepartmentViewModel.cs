using Company.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Company.PL.Models
{
    public class DepartmentViewModel
    {
        public int Code { get; set; }
        [Required]
        [MaxLength(50)]
        public String? Name { get; set; }
    }
}