using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;




namespace TestProject1
{
    public class Tests
    {
        IWebDriver webDriver = new ChromeDriver();
        #region Locators
        string aceeptAllCookiesButton = "//*[contains(text(),'Accept All Cookies') and @role='button']";
        string navBar = "//*[contains(@class,'navbar-toggle collapsed') and @type='button']";
        string modernworkbutton = "//li[@id = 'menu-item-30711']//a[contains(@href, 'modern-work/')]";
        string employeeExperianceButton = "//div[@class = 'vc_tta-tabs-container']//span[text() ='Employee Experience']/..";
        string headerEmployeeExp = "//h3[contains(text(),'Employee Experience')]";
        string EmployeeExpPara = "//strong[contains(text(),'Engaging, ')]";
        #endregion


        [SetUp]
        public void Setup()
        {
            //open browser and navigate to site
            webDriver.Navigate().GoToUrl("https://www.spanishpoint.ie/");
        }

        [Test]
        public void TestForEmployeeExperienceSection()
        {
            string actualHeaderStr, actualParaStr;
            string expectedHeader = "Employee Experience";
            string expectedParaStr = "Engaging, Mobile Intranet and Digital Workspace collaboration solution.";
            string strFormat = "strong";

            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(input: webDriver);
            try
            {
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(1));
                var clickableElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(aceeptAllCookiesButton)));
                clickableElement.Click();
                var clickableNavBar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(navBar)));
                clickableNavBar.Click();
                var clickableModernworkbutton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(modernworkbutton)));
                clickableModernworkbutton.Click();
                fluentWait.Timeout = TimeSpan.FromSeconds(1);
                webDriver.FindElement(By.XPath(employeeExperianceButton)).Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            fluentWait.Timeout = TimeSpan.FromSeconds(2);
            actualHeaderStr = webDriver.FindElement(By.XPath(headerEmployeeExp)).GetAttribute("innerHTML");
            actualParaStr = webDriver.FindElement(By.XPath(EmployeeExpPara)).GetAttribute("innerHTML");
            Assert.Multiple (() =>
            {
                // Below assert is to check the heading
                Assert.AreEqual(expectedHeader, actualHeaderStr);
                StringAssert.Contains(strFormat, EmployeeExpPara, "Is not bold");
                // Below assert is to check the paragraph sentence which is in bold
                Assert.AreEqual(expectedParaStr, actualParaStr);
            }
            ); 
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Close();
            webDriver.Dispose();
            webDriver.Quit();
        }
    }
}
