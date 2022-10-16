using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BottomTextLMS.Models
{
    public class Building
    {
        [Key]
        public int ID { get; set; }

        public string BuildingName { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}
