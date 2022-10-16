using System.ComponentModel.DataAnnotations;

namespace BottomTextLMS.Models
{
    public class ProfileImage
    {
        [Key]
        public int ID { get; set; }
        public string ProfileName { get; set; }
        public string ProfileLocation { get; set; }

    }
}
