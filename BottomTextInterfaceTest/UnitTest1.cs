using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace BottomTextInterfaceTest
{
    public class Tests
    {
        public IWebDriver Driver;

        [SetUp]
        public void Setup()
        {
            Driver = new ChromeDriver();
        }

        [Test]
        public void LoginTest()
        {
            Driver.Navigate().GoToUrl("http://localhost:5001/");
            System.Threading.Thread.Sleep(500);
            Driver.FindElement(By.Id("myUser_Email")).SendKeys("walterwhite@gmail.com");
            System.Threading.Thread.Sleep(500);
            Driver.FindElement(By.Id("myUser_Password")).SendKeys("Password");
            System.Threading.Thread.Sleep(500);
            Driver.FindElement(By.Id("login_button")).Click();
            System.Threading.Thread.Sleep(2000);
            Driver.Close();
            Assert.Pass();
        }

        
        [Test]
        public void StudentClassRegistrationAndDrop()
        {
            string email = "jessepinkman@gmail.com";
            string password = "Password";

            Driver.Navigate().GoToUrl("http://localhost:5001/");
            Driver.FindElement(By.Id("myUser_Email")).SendKeys(email);
            Driver.FindElement(By.Id("myUser_Password")).SendKeys(password);
            Driver.FindElement(By.Id("login_button")).Click();
            Thread.Sleep(3000);
            Driver.FindElement(By.Id("registration")).Click();
            Thread.Sleep(3000);
            Driver.FindElement(By.Id("registerBtn_registration_5")).Click();
            Thread.Sleep(3000);
            Driver.FindElement(By.Id("dropBtn_registration_5")).Click();
            Thread.Sleep(3000);
            Driver.Close();
            Assert.Pass();
        }

        [Test]
        public void InstructLogin_AddCourse_DeleteCourseTest()
        {
            // Set instructor parameters to run test with
            string email = "walterwhite@gmail.com";
            string password = "Password";

            // Go to login page
            Driver.Navigate().GoToUrl("http://localhost:5001/");

            // Log in as instructor
            Driver.FindElement(By.Id("myUser_Email")).SendKeys(email);
            System.Threading.Thread.Sleep(500);
            Driver.FindElement(By.Id("myUser_Password")).SendKeys(password);
            System.Threading.Thread.Sleep(500);
            Driver.FindElement(By.Id("login_button")).Click();

            // Navigate to add course page
            System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.LinkText("Add Class")).Click();
            System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.Id("add_course_link")).Click();

            // Complete form to add course
            System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.Id("Class_ClassNumber")).SendKeys("5555");
            System.Threading.Thread.Sleep(500);
            Driver.FindElement(By.Id("Class_ClassName")).SendKeys("TestClass");
            System.Threading.Thread.Sleep(500);
            Driver.FindElement(By.Id("Class_Description")).SendKeys("Test description");
            System.Threading.Thread.Sleep(500);
            Driver.FindElement(By.Id("Class_CreditHours")).SendKeys("5");
            System.Threading.Thread.Sleep(500);
            Driver.FindElement(By.Id("Class_StartTime")).SendKeys("530p");
            System.Threading.Thread.Sleep(500);
            Driver.FindElement(By.Id("Class_EndTime")).SendKeys("630p");
            System.Threading.Thread.Sleep(500);
            Driver.FindElement(By.Id("submit_button")).Click();

            // Verify that course was added
            System.Threading.Thread.Sleep(3000);

            try
            {
                Driver.FindElement(By.Id("TestClass"));
            }
            catch (NoSuchElementException e)
            {
                Driver.Close();
                Assert.Fail();      // If table row holding test class doesn't exist, fail test
            }

            // Delete course
            Driver.FindElement(By.Id("delete_TestClass")).Click();
            System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.Id("delete_button")).Click();

            // Verify course is deleted
            System.Threading.Thread.Sleep(2000);

            try
            {
                var classRow = Driver.FindElement(By.Id("TestClass"));
                if (classRow != null)
                {
                    Driver.Close();
                    Assert.Fail();  // If table row holding class still exists, fail test
                }
            }
            catch (NoSuchElementException e)
            {
                Driver.Close();
                Assert.Pass();      // If table row holding test class is now gone, pass test
            }
        }

        [Test]
<<<<<<< Updated upstream
        public void Student_Can_Submit_Assignment_Test() {
            // set student parameters to run test with
            string email = "kippyjoel@gmail.com";
=======
        public void Student_Can_Submit_Assignment_Test()
        {
            // Set instructor parameters to run test with
            string email = "student9@gmail.com";
>>>>>>> Stashed changes
            string password = "Password";
            int id = 32;

            // Go to login page
            Driver.Navigate().GoToUrl("http://localhost:5001/");

            // log in as student
            Driver.FindElement(By.Id("myUser_Email")).SendKeys(email);
            System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.Id("myUser_Password")).SendKeys(password);
            System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.Id("login_button")).Click();

            // navigate to assignment submission page
            System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.Id("card_1")).Click();
            System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.Id("submission_18")).Click();

            // if assignment already exists, assert fail
            System.Threading.Thread.Sleep(2000);
            string preSubCheck = Driver.FindElement(By.Id("textArea")).GetAttribute("value");
<<<<<<< Updated upstream
            if (preSubCheck != "") {
                Driver.Close();
=======
            if (preSubCheck != "")
            {
>>>>>>> Stashed changes
                Assert.Fail();
            }

            // enter and submit test submission
            Driver.FindElement(By.Id("textArea")).SendKeys("Testing a student submission!");
            System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.Id("submitButton")).Click();

            // re-enter student submission page
            System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.Id("submission_18")).Click();

            // save current student submission
            System.Threading.Thread.Sleep(2000);
            string postSubCheck = Driver.FindElement(By.Id("textArea")).GetAttribute("value");

            // if assignment was saved, clear it from database and assert pass
            if (postSubCheck == "Testing a student submission!")
            {
                // connect to database
                DbContextOptions<BottomTextLMS.AppDbContext> options = new DbContextOptions<BottomTextLMS.AppDbContext>();
                DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
                SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMSGhln;User ID=LMSGhln;Password=Password*1; MultipleActiveResultSets=True", null);
                var _context = new BottomTextLMS.AppDbContext((DbContextOptions<BottomTextLMS.AppDbContext>)builder.Options);

                // delete test submission
                _context.Database.ExecuteSqlRaw(string.Format("UPDATE Submissions SET HasSubmitted = '{0}', TextSubmission = NULL WHERE StudentID = 32 AND AssignmentID = 18", false));

                Driver.Close();
                Assert.Pass();
            }
        }
    }
}