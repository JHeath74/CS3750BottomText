using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace BottomTextStudentRegisterAndDropClass
{

    public class Test
    { 
        public IWebDriver Driver;

        [SetUp]
        public void Setup()
        {
            Driver = new ChromeDriver();
        }

        [Test]
        public void StudentClassRegistrationAndDrop()
        {
            Driver.Navigate().GoToUrl("https://localhost:5001/");
            Driver.FindElement(By.Id("myUser_Email")).SendKeys("JHeath74@msn.com");
            Driver.FindElement(By.Id("myUser_Password")).SendKeys("B*1100ns");
            Driver.FindElement(By.Id("login_button")).Click();
            Thread.Sleep(3000);
            Driver.FindElement(By.Id("registration")).Click();
            Thread.Sleep(3000);
            Driver.FindElement(By.Id("registerBtn")).Click();
            Thread.Sleep(3000);
            Driver.FindElement(By.Id("dropBtn")).Click();
            Thread.Sleep(3000);
      }
    }
}
