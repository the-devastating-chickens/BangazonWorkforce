using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace BangazonWorkforce.Models
{
    public class EmployeeAddEditViewModel
    {
        public Employee Employee { get; set; }
        public List<Department> AllDepartments { get; set; }
        public List<SelectListItem> AllDepartmentOptions
        {
            get
            {
                if (AllDepartments == null)
                {
                    return null;
                }

                return AllDepartments
                        .Select((d, id) => new SelectListItem(d.Name, id.ToString()))
                        .ToList();
            }
        }
    }
}
