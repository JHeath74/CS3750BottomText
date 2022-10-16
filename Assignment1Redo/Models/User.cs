using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BottomTextLMS.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [MinimumAge(18, "You are not 18 years old!")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+@[a-zA-Z]+.[a-zA-Z]{2,4}$")]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped] // Doesn't add to database
        [DataType(DataType.Password)]
        [CompareAttribute("Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Role { get; set; }

        public UserInfo UserInfo { get; set; }

        [ForeignKey("InstructorID")]
        public ICollection<Class> Classes { get; set; }
    }
}