using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models.ViewModels
{
    public class EmployeeEditViewModel
    {
        public Employee Employee { get; set; }
        public List<Department> AvailableDepartments { get; set; }
        public List<SelectListItem> AvailableDepartmentsSelectList
        {
            get
            {
                if (AvailableDepartments == null)
                {
                    return null;
                }

                return AvailableDepartments.Select(a => new SelectListItem(a.Name, a.Id.ToString())).ToList();

                
            }
        }
        public int CurrentComputerId { get; set;  }
        public List<Computer> AvailableComputers { get; set; }
        public List<SelectListItem> AvailableComputersSelectList
        {
            get
            {
                if(AvailableComputers == null)
                {
                    return null;
                }

                var variable = AvailableComputers.Select(a => new SelectListItem(a.Make, a.Id.ToString())).ToList();
                variable.Insert(0, new SelectListItem("Select", null));
                return variable;
            }
        }
    }
}
