using Company.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Contexts
{
    public class CompanyDbContext : IdentityDbContext<ApplicationUser>
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
        {
        }

        //protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        //{
        //    builder.Properties<DateOnly>()
        //        .HaveConversion<DateOnlyConverter>()
        //        .HaveColumnType("date");
        //}

        public DbSet<Department>? Departments { get; set; }
        public DbSet<Employee>? Employees { get; set; }
    }
}
