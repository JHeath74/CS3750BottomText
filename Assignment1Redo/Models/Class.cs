using System;
using System.ComponentModel.DataAnnotations;

namespace BottomTextLMS.Models
{
    public class Class
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Class Number")]
        [Required]
        [Range(1, 9999)]
        public int? ClassNumber { get; set; }

        [Display(Name = "Class Name")]
        [Required]
        [StringLength(30)]
        public string ClassName { get; set; }

        [Required]
        [StringLength(300)]
        public string Description { get; set; }

        [Display(Name = "Credit Hours")]
        [Required]
        [Range(1, 8)]
        public int CreditHours { get; set; }

        [Display(Name = "Department")]
        [Required]
        public int DepartmentID { get; set; }
        public Department Department { get; set; }

        [Required]
        public int InstructorID { get; set; }
        public User User { get; set; }

        [Display(Name = "Room Number")]
        [Required]
        public int RoomID { get; set; }
        public Room Room { get; set; }

        [Display(Name = "Building")]
        [Required]
        public int BuildingID { get; set; }

        [Display(Name = "Class Days")]
        [Required]
        public string DaysOfWeek { get; set; }

        [Display(Name = "Class Start Time")]
        [Required]
        public DateTime StartTime { get; set; }

        [Display(Name = "Class End Time")]
        [Required]
        public DateTime EndTime { get; set; }
    }
}
