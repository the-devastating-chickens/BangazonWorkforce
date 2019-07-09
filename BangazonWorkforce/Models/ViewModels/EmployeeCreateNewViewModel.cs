using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models.ViewModels
{
    public class EmployeeCreateNewViewModel
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
                 
                  var variable = AvailableDepartments.Select(a => new SelectListItem(a.Name, a.Id.ToString())).ToList();

                variable.Insert(0, new SelectListItem("Please select", null));

                return variable;
            }
        }
    }
}
