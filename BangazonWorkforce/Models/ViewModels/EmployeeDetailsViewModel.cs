/* Author: Billy Mathison
 * Purpose: Creating a view model for an employee's details to an employee, assigned computer, a list of training programs, and ComputerEmployee which contains assigned and unassigned dates for their computer.
 * Methods: None
 */

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
