using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SharedClassLibrary.DTOs;

namespace EDUHUNT_BE.NewFolder
{
    public class ScrapingService
    {
        private readonly IWebDriver _driver;

        public ScrapingService(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public async Task<List<ScholarshipDTO>> ScrapeScholarships()
        {
            try
            {
                _driver.Navigate().GoToUrl("https://www.scholarshipportal.com/");

                await Task.Delay(1000);

                var scholarshipDivs = _driver.FindElements(By.CssSelector("div.flex.md\\:w-1\\/3"));

                var scholarships = new List<ScholarshipDTO>();

                foreach (var scholarshipDiv in scholarshipDivs)
                {
                    var scholarshipLink = scholarshipDiv.FindElement(By.CssSelector("a"));
                    var scholarshipUrl = scholarshipLink.GetAttribute("href");

                    Console.WriteLine("====================LINK======================");
                    Console.WriteLine(scholarshipUrl);

                    ((IJavaScriptExecutor)_driver).ExecuteScript($"window.open('{scholarshipUrl}', '_blank');");
                    _driver.SwitchTo().Window(_driver.WindowHandles[^1]);

                    var mainElement = _driver.FindElement(By.CssSelector("main.p-6.bg-white.rounded-t.shadow.sm\\:p-8.md\\:p-12"));

                    Console.WriteLine("==========================================");
                    Console.WriteLine(mainElement.GetAttribute("outerHTML"));

                    //var aiservice = new OpenAiService("api url", "api key");
                    //var newscholarship = await aiservice.FetchScholarshipInfoAsync(mainElement.GetAttribute("outerHTML"));

                    // Add scholarship DTO to the list based on the parsing logic
                    // scholarships.Add(newscholarship);

                    _driver.Close();
                    _driver.SwitchTo().Window(_driver.WindowHandles[0]);
                }

                return scholarships;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<ScholarshipDTO>();
            }
        }
    }
}
