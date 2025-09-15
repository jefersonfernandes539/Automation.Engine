using Automation.Engine.Domain.Entities;
using Automation.Engine.Domain.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Automation.Engine.Infrastructure.Rpa
{
    public class RpaService : IRpaService
    {
        public void FillForm(Quote quote)
        {
            using IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.w3schools.com/html/html_forms.asp");

            var input = driver.FindElement(By.XPath("//input[@name='firstname']"));
            input.Clear();
            input.SendKeys(quote.Text);

            Thread.Sleep(5000);
            driver.Quit();
        }
    }
}
