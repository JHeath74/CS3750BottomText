namespace BottomTextLMS.Models
{
    public class Enrollment
    {
        public int StudentID { get; set; }
        public int ClassID { get; set; }

        public User Student { get; set; }

        public Class Class { get; set; }
    }
}
