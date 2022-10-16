using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BottomTextLMS.Models
{
    public class Assignment
    {
        [Key]
        public int ID { get; set; }

        public int ClassID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Max Points")]
        public int MaxPoints { get; set; }

        [Display(Name = "Submission Type")]
        public string AssignmentType { get; set; }

        public ICollection<Submission> Submissions { get; set; }

        public Class Class { get; set; }

    }
}
