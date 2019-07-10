using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models.ViewModels
{
    public class EmployeeDetailsViewModel
    {
        public Employee Employee { get; set; }
        public Computer AssignedComputer { get; set; }
        public List<TrainingProgram> TrainingPrograms { get; set; } = new List<TrainingProgram>();
        public ComputerEmployee ComputerEmployee { get; set; }
    }
}
