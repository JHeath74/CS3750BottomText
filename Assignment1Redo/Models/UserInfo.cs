using System.ComponentModel.DataAnnotations;

namespace BottomTextLMS.Models
{
    public class UserInfo
    {
        [Key]
        public int UserID { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int? Zipcode { get; set; }

        public long? PhoneNumber { get; set; }

        public string Bio { get; set; }

        public string Link1 { get; set; }


        public string Link2 { get; set; }


        public string Link3 { get; set; }

        public User User { get; set; }
        public string ProfileImageName { get; set; }
    }
}
