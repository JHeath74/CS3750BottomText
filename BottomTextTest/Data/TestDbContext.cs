namespace Data
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.StudentID, e.ClassID });
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserInfo> UsersInfo { get; set; }
        public DbSet<Building> Buildings { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Class> Classes { get; set; }

        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<ProfileImage> ProfileImage { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<Submission> Submissions { get; set; }

        public DbSet<BottomTextLMS.Models.CreditCard> CreditCard { get; set; }

        public DbSet<Profile> profiles { get; set; }

    }
}
