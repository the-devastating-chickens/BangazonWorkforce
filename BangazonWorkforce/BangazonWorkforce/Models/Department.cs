using System.ComponentModel.DataAnnotations;

namespace BangazonWorkforce.Models
{

    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must provide a name for this department.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must provide a budget for this department.")]
        [Range(0, int.MaxValue, ErrorMessage = "A budget cannot be less than zero.")]
        public int Budget { get; set; }
    }
}
