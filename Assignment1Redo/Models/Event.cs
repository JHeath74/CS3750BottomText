using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BottomTextLMS.Models
{
    public class Event
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int StudentID { get; set; }

        public string AssignmentName { get; set; }

        public Assignment Assignment { get; set; }

        public string ClassName { get; set; }

        public string EventType { get; set; }

        public bool HasViewed { get; set; }

        public DateTime TimeCreated { get; set; }
    }
}
