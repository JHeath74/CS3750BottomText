using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BottomTextLMS.Models
{
    public class Department
    {
        [Key]
        public int ID { get; set; }

        public string DepartmentName { get; set; }
        public string DepartmentAbbrv { get; set; }

        public ICollection<Class> Classes { get; set; }
    }
}
