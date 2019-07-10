/* Author: Billy Mathison
 * Purpose: Creating a model of ComputerEmployee to store information related to the computer assigned to an employee, including assignDate and UnassignDate
 * Methods: None
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models
{
    public class ComputerEmployee
    {
        public DateTime? AssignDate { get; set; }

        public DateTime? UnassignDate { get; set; }
    }
}
