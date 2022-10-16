using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BottomTextLMS.Models
{
    public class Submission
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int StudentID { get; set; }

        public int AssignmentID { get; set; }

        public bool HasSubmitted { get; set; }

        public Assignment Assignment { get; set; }

        public string FileSubmission { get; set; }

        public string TextSubmission { get; set; }

        public int? PointsEarned { get; set; }

        public DateTime SubmitTime { get; set; }
    }
}
