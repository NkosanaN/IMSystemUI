using System.ComponentModel;

namespace IMSystemUI.Domain
{
    public class Department
    {
        [DisplayName("Department Id")]
        public Guid DepartmentId { get; set; }

        [DisplayName("Department Name")]
        public string DepartmentName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
