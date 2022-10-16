using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BottomTextLMS.Models
{
    public class Room
    {
        [Key]
        public int ID { get; set; }

        public int? RoomNumber { get; set; }

        public int BuildingID { get; set; }

        public ICollection<Class> Classes { get; set; }

        public Building Building { get; set; }
    }
}
