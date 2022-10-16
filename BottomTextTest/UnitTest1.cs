<<<<<<< Updated upstream
using Assignment1Redo.Models;
using Assignment1Redo.Pages.Courses;
using Assignment1Redo.Pages.Assignments;
using Assignment1Redo.Pages.StudentAssignments;
using Assignment1Redo.Pages.Submissions;
using Microsoft.EntityFrameworkCore;
=======
>>>>>>> Stashed changes
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BottomTextTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task InstructorCanCreateCourseTest()
        {
            // connect to database
            DbContextOptions<BottomTextLMS.AppDbContext> options = new DbContextOptions<BottomTextLMS.AppDbContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMSGhln;User ID=LMSGhln;Password=Password*1; MultipleActiveResultSets=True", null);
            var _context = new BottomTextLMS.AppDbContext((DbContextOptions<BottomTextLMS.AppDbContext>)builder.Options);

            // define instructor ID
            int instructorID = 3;

            // start with instructor with given ID
            BottomTextLMS.Models.User prof = new BottomTextLMS.Models.User();
            prof = _context.Users.Where(p => p.ID == instructorID).FirstOrDefault();

            // query and save how many courses this instructor is teaching (N courses)
            int preAddCourseCount = (from Classes in _context.Classes where Classes.InstructorID == instructorID select Classes.InstructorID).Count();

            // create and populate test class
            Class TestClass = new Class();
            TestClass.ClassNumber = 999;
            TestClass.ClassName = "TEST_CLASS";
            TestClass.Description = "TestClass description.";
            TestClass.CreditHours = 4;
            TestClass.DepartmentID = 1;
            TestClass.RoomID = 1;
            TestClass.BuildingID = 1;
            TestClass.InstructorID = instructorID;
            TestClass.DaysOfWeek = "M,W,F";
            TestClass.StartTime = System.DateTime.Now;
            TestClass.EndTime = System.DateTime.Now;

            // exercise your source code to make instructor create a new course
            BottomTextLMS.Pages.Courses.CreateModel model = new BottomTextLMS.Pages.Courses.CreateModel(_context);
            await model.SaveCourse(TestClass);

            // query and save how many courses this instructor is teaching now (N+1 courses)
            int postAddCourseCount = (from Classes in _context.Classes where Classes.InstructorID == instructorID select Classes.InstructorID).Count();

            // assert whether or not the test was successful
            Assert.IsTrue(postAddCourseCount == preAddCourseCount + 1, "Instructor class count did not increment by 1.");

            // if the test was successful, delete test class from database
            if (postAddCourseCount == preAddCourseCount + 1)
            {
                _context.Database.ExecuteSqlRaw(string.Format("DELETE FROM Classes WHERE ClassNumber = '{0}' AND InstructorID = {1}", TestClass.ClassNumber, instructorID));
            }
        }

        [TestMethod]
        public async Task InstructorCanCreateAssignmentTest()
        {
            DbContextOptions<BottomTextLMS.AppDbContext> options = new DbContextOptions<BottomTextLMS.AppDbContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMSGhln;User ID=LMSGhln;Password=Password*1; MultipleActiveResultSets=True", null);
            var _context = new BottomTextLMS.AppDbContext((DbContextOptions<BottomTextLMS.AppDbContext>)builder.Options);

            // Instructor: Walter White
            int instructorID = 1;
            // Class: Introductory Chemistry
            int classID = 1;

            Assignment Assignment = new Assignment();
            Assignment.ClassID = classID;
            Assignment.Title = "Test Assignment";
            Assignment.Description = "Test Description";
            Assignment.DueDate = System.DateTime.Now;
            Assignment.MaxPoints = 100;
            Assignment.AssignmentType = "File";

            int preAddAssignmentCount = (from aRow in _context.Assignments where aRow.ClassID == classID select aRow).Count();

            BottomTextLMS.Pages.Assignments.CreateModel model = new BottomTextLMS.Pages.Assignments.CreateModel(_context);
            await model.SaveAssignment(Assignment);

            int postAddAssignmentCount = (from aRow in _context.Assignments where aRow.ClassID == classID select aRow).Count();

            Assert.IsTrue(postAddAssignmentCount == preAddAssignmentCount + 1, "Assignment was added");

            // delete assignment
            if (postAddAssignmentCount == preAddAssignmentCount + 1)
            {
                _context.Assignments.Remove(Assignment);
                await _context.SaveChangesAsync();
            }
        }

        [TestMethod]
<<<<<<< Updated upstream
        public async Task StudentCanRegisterForCourseTest()
        {
            // connect to database
            DbContextOptions<Assignment1Redo.AppDbContext> options = new DbContextOptions<Assignment1Redo.AppDbContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMSGhln;User ID=LMSGhln;Password=Password*1; MultipleActiveResultSets=True", null);
            var _context = new Assignment1Redo.AppDbContext((DbContextOptions<Assignment1Redo.AppDbContext>)builder.Options);

            // Student to register
            int studentID = 8;  // Jesse Pinkman

            // Class to register for
            int classID = 6; // 4165 - Constitutional Rights

            // Count number of enrollments before registering
            int preEnrollmentCount = _context.Enrollments.Where(e => e.StudentID == studentID).ToList().Count;

            // Register for class
            Assignment1Redo.Pages.Registration.RegistrationModel model = new Assignment1Redo.Pages.Registration.RegistrationModel(_context);
            await model.RegisterStudent(studentID, classID);

            // Count number of enrollments after registering
            int postEnrollmentCount = _context.Enrollments.Where(e => e.StudentID == studentID).ToList().Count;

            // If number of enrollments increased by one, pass the test and drop the enrollment.
            Assert.AreEqual(preEnrollmentCount + 1, postEnrollmentCount);

            if (preEnrollmentCount + 1 == postEnrollmentCount)
            {
                await model.DropStudent(studentID, classID);
            }
        }

        [TestMethod]
        public async Task StudentCanSubmitAssignmentUnitTest()
        {
            // connect to database
            DbContextOptions<Assignment1Redo.AppDbContext> options = new DbContextOptions<Assignment1Redo.AppDbContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMSGhln;User ID=LMSGhln;Password=Password*1; MultipleActiveResultSets=True", null);
            var _context = new Assignment1Redo.AppDbContext((DbContextOptions<Assignment1Redo.AppDbContext>)builder.Options);

            // define student properties
            int studentID = 22;
            int submissionID = 61;

            // start with student with given ID
            Assignment1Redo.Models.User student = new Assignment1Redo.Models.User();
=======
        public async Task StudentCanSubmitAssignmentUnitTest()
        {
            // connect to database
            DbContextOptions<BottomTextLMS.AppDbContext> options = new DbContextOptions<BottomTextLMS.AppDbContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMSGhln;User ID=LMSGhln;Password=Password*1; MultipleActiveResultSets=True", null);
            var _context = new BottomTextLMS.AppDbContext((DbContextOptions<BottomTextLMS.AppDbContext>)builder.Options);


            // define student ID
            int studentID = 22;

            // start with student with given ID
            BottomTextLMS.Models.User student = new BottomTextLMS.Models.User();
>>>>>>> Stashed changes
            student = _context.Users.Where(p => p.ID == studentID).FirstOrDefault();

            // query and save how many assignments this student has submitted (N courses)
            int preAddAssignmentCount = (from Submission in _context.Submissions where Submission.StudentID == studentID && Submission.HasSubmitted == true select Submission.StudentID).Count();

<<<<<<< Updated upstream
            // create test text Submission
            Submission myTextSubmission = new Submission();

            // pull pre-exisiting submission from db, modify TextEntry property
            myTextSubmission = _context.Submissions.Where(s => s.StudentID == studentID && s.ID == submissionID).FirstOrDefault();
            myTextSubmission.TextSubmission = "Unit Test Text Submission";

            // exercise your source code to make student submit text assignment
            Assignment1Redo.Pages.StudentAssignments.TextSubmissionModel model = new Assignment1Redo.Pages.StudentAssignments.TextSubmissionModel(_context);
            model.SaveTextSubmission(myTextSubmission.TextSubmission, myTextSubmission.ID);

            // query and save how many assignments this student has submitted (should be N+1 courses)
            int postAddAssignmentCount = (from Submission in _context.Submissions where Submission.StudentID == studentID && Submission.HasSubmitted == true select Submission.StudentID).Count();

            // assert whether or not the test was successful
            Assert.IsTrue(postAddAssignmentCount == preAddAssignmentCount + 1, "Text Submission count did not increment by 1.");

            // if the test was successful, delete test submission from database
            if (postAddAssignmentCount == preAddAssignmentCount + 1)
            {
                _context.Database.ExecuteSqlRaw(string.Format("UPDATE Submissions SET TextSubmission = NULL, HasSubmitted = '{0}' WHERE ID = '{1}' AND StudentID = {2}", false, myTextSubmission.ID, studentID));
=======
            //Submit Assignment
            Submission myAssignmentSubmission = new Submission();

            myAssignmentSubmission.TextSubmission = "This is my Assignment";
            myAssignmentSubmission.HasSubmitted = true;
            myAssignmentSubmission.SubmitTime = DateTime.Now;

            //update method in submissions

            // exercise your source code to make student submit assignment
            BottomTextLMS.Pages.StudentAssignments.CreateModel model = new BottomTextLMS.Pages.StudentAssignments.CreateModel(_context);
            // await model.SaveAssignment(myAssignmentSubmission);

            int postAddAssignmentCount = (from Submission in _context.Submissions where Submission.StudentID == studentID && Submission.HasSubmitted == true select Submission.StudentID).Count();

            // assert whether or not the test was successful
            Assert.IsTrue(postAddAssignmentCount == preAddAssignmentCount + 1, "Assignment count did not increment by 1.");

            // if the test was successful, delete test class from database
            if (postAddAssignmentCount == preAddAssignmentCount + 1)
            {
                _context.Database.ExecuteSqlRaw(string.Format("UPDATE Submissions SET TextSubmission = '' and HasSubmitted = '' WHERE ID = '{0}' AND StudentID = {1}", myAssignmentSubmission.ID, studentID));
>>>>>>> Stashed changes
            }
        }
    }
}
