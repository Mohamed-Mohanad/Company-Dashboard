using Company.DAL.Entities;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Company.PL.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Requried")]
        [MaxLength(50, ErrorMessage = "Maximum Lenght Of Name Is 50 Character")]
        [MinLength(6, ErrorMessage = "Minmum Lenght Of Name Is 6 Character")]
        public string? Name { get; set; }
        [Range(18, 60, ErrorMessage = "Age Must Be Between 18 and 60")]
        public int? Age { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        [Range(4000,8000, ErrorMessage ="Salay Must Be Between 4000 and 8000")]
        public decimal Salary { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Number Is Requried")]
        public string? PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        [Required(ErrorMessage = "Image Is Requried")]
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentViewModel? Department { get; set; }
    }
}
