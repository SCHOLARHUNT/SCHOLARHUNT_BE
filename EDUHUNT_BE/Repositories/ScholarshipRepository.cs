using EDUHUNT_BE.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SharedClassLibrary.Contracts;
using SharedClassLibrary.DTOs;
using EDUHUNT_BE.Model;
using EDUHUNT_BE.NewFolder;
public class ScholarshipRepository : IScholarship
{
    private readonly AppDbContext _dbContext;

    public ScholarshipRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ScholarshipDTO>> GetScholarships()
    {
        using (var driver = new ChromeDriver())
        {
            try
            {
                driver.Navigate().GoToUrl("https://www.scholarshipportal.com/");

                await Task.Delay(1000);

                // Find all div elements with class "flex md:w-1/3"
                var scholarshipDivs = driver.FindElements(By.CssSelector("div.flex.md\\:w-1\\/3"));

                foreach (var scholarshipDiv in scholarshipDivs)
                {
                    // Get the URL from the <a> tag inside the div
                    var scholarshipLink = scholarshipDiv.FindElement(By.CssSelector("a"));
                    var scholarshipUrl = scholarshipLink.GetAttribute("href");

                    Console.WriteLine("====================LINK======================");
                    Console.WriteLine(scholarshipUrl);

                    // Open a new tab by executing JavaScript
                    ((IJavaScriptExecutor)driver).ExecuteScript($"window.open('{scholarshipUrl}', '_blank');");

                    // Switch to the newly opened tab
                    driver.SwitchTo().Window(driver.WindowHandles[^1]);

                    // Find the <main> element with class "p-6 bg-white rounded-t shadow sm:p-8 md:p-12"
                    var mainElement = driver.FindElement(By.CssSelector("main.p-6.bg-white.rounded-t.shadow.sm\\:p-8.md\\:p-12"));


                    // Print the HTML content inside the <main> element
                    Console.WriteLine("==========================================");
                    Console.WriteLine(mainElement.GetAttribute("outerHTML"));

                    //var aiservice = new OpenAiService("api url", "api key");
                    //var newscholarship = await aiservice.FetchScholarshipInfoAsync(mainElement.GetAttribute("outerHTML"));


                    // Close the current tab (optional)
                    driver.Close();

                    // Switch back to the main window
                    driver.SwitchTo().Window(driver.WindowHandles[0]);
                }

                // For illustration purposes, return an empty list
                return new List<ScholarshipDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<ScholarshipDTO>();
            }
            finally
            {
                // This will only close the main window, not the additional tabs
                driver.Quit();
            }
        }
    }
    //public async Task<List<ScholarshipDTO>> GetScholarships()
    //{
    //    using (var driver = new ChromeDriver())
    //    {
    //        var scholarships = await _scrapingService.ScrapeScholarships();

    //        // Add logic to save scholarships to the database if needed
    //        // ...

    //        return scholarships;
    //    }
    //}






    public async Task AddScholarship(ScholarshipDTO scholarship)
    {
        // Implement logic to add scholarship to your data source (e.g., database)
        // ...
    }

    private List<ScholarshipDTO> ParseHtml(string html)
    {
        // Implement logic to parse the HTML and extract scholarship information
        // ...

        // For illustration purposes, return an empty list
        return new List<ScholarshipDTO>();
    }
}
