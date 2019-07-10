/* Author: Billy Mathison
 * Purpose: Creating a model of TrainingProgram to store information related to the training program, including name, start date, end date, maximum number of attendees, and list of employees. 
 * Methods: None
 */


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models
{
    public class TrainingProgram
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Required]
        [Display(Name = "Maximum Number Attendees")]
        public int MaxAttendees { get; set; }
        public List<Employee> EmployeesInTrainingProgram { get; set; } = new List<Employee>();
    }
}
