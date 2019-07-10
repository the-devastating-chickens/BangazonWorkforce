using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must provide a budget for this department.")]
        [Range(0, int.MaxValue, ErrorMessage = "A budget cannot be less than zero or Higher then 2 Billion")]
        public int Budget { get; set; }
        [Display(Name = "Number of Employees")]
        public int NumberOfEmployees { get; set;  }
        [Display(Name = "Employees:")]
        public List<Employee> DepartmentEmployees { get; set; }
    }
}
