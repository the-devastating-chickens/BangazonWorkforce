using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public bool IsSuperVisor { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select Something")]
        public int? DepartmentId { get; set; }

        public List<TrainingProgram> EmployeeTrainingPrograms { get; set; } = new List<TrainingProgram>();

        public List<Computer> Computers { get; set; } = new List<Computer>();

        public Department Department { get; set; }
    }
}
