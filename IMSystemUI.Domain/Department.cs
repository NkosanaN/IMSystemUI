using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IMSystemUI.Domain
{
    public class Department
    {
        [DisplayName("Department Id")]
        public Guid DepartmentId { get; set; }

        [DisplayName("Department Name")]
        [Required]
        public string DepartmentName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
