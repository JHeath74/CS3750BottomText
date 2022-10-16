using System.ComponentModel.DataAnnotations;

namespace BottomTextLMS.Models
{
    public class Profile
    {
        [Required]
        public int ID { get; set; }

        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int Zip { get; set; }

        public string Phone { get; set; }

        public string Bio { get; set; }

        public string Link1 { get; set; }

        public string Link2 { get; set; }

        public string Link3 { get; set; }
    }
}
