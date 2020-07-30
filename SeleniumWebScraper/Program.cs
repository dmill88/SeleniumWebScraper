using System;
using System.IO;
using Selenium;
using SeleniumWebScraper;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Internal;

namespace SeleniumWebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Simple Screen Scrapper");

            ChromeOptions options = new ChromeOptions() { AcceptInsecureCertificates = true };
            options.AddArgument("--headless");
            using var driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options);
            TryWebDriver tester = new TryWebDriver(driver);

            tester.TestLoggingIn();

            Console.WriteLine($"{Environment.NewLine}Press any key to read text from Reactjs.org");
            Console.ReadKey();
            tester.TestGettingTextFromReactJS();

            Console.WriteLine($"{Environment.NewLine}Press any key to close");
            Console.ReadKey();
            driver.Quit();
        }
    }

    class TryWebDriver 
    {
        private readonly ChromeDriver _driver = null;

        public TryWebDriver(ChromeDriver driver)
        {
            _driver = driver;
        }

        public void TestLoggingIn()
        {
            _driver.Navigate().GoToUrl("http://testing-ground.scraping.pro/login");

            var userNameField = _driver.FindElementById("usr");
            var userPasswordField = _driver.FindElementById("pwd");
            var loginButton = _driver.FindElementByXPath("//input[@value='Login']");

            userNameField.SendKeys("admin");
            userPasswordField.SendKeys("12345");
            loginButton.Click();

            string result = _driver.FindElementByXPath("//div[@id='case_login']/h3").Text;
            Console.WriteLine(result);
        }

        public void TestGettingTextFromReactJS()
        {
            _driver.Navigate().GoToUrl("https://reactjs.org/");
            
            IWebElement elem = _driver.FindElementByXPath("/html/body/div/div[1]/div/div/div/div/div/section[2]/div/div[1]/div[1]/div/p[1]");
            string result = elem == null ? "Element not found" : elem.Text;
            Console.WriteLine(result);
        }
    }


}
