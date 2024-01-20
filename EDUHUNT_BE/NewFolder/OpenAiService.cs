using EDUHUNT_BE.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EDUHUNT_BE.NewFolder
{
    public class OpenAiService
    {
        private readonly string _apiUrl;
        private readonly string _apiKey;

        public OpenAiService(string apiUrl, string apiKey)
        {
            _apiUrl = apiUrl ?? throw new ArgumentNullException(nameof(apiUrl));
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        }

        public async Task<ScholarshipInfo> FetchScholarshipInfoAsync(string htmlAttribute)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                // Modify this part based on your specific needs
                string requestData = $"{{\"model\":\"gpt-3.5-turbo\",\"max_tokens\":4000,\"messages\":[]}}";
                StringContent content = new StringContent(requestData, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(_apiUrl, content).ConfigureAwait(false);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                    // Parse the response and create a new ScholarshipInfo instance
                    var scholarshipInfo = ParseResponseAndCreateScholarshipInfo(responseString, htmlAttribute);

                    return scholarshipInfo;
                }
                else
                {
                    throw new HttpRequestException($"Error: {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase}");
                }
            }
        }

        private ScholarshipInfo ParseResponseAndCreateScholarshipInfo(string response, string htmlAttribute)
        {
            // Implement your logic to parse the response and create a ScholarshipInfo instance
            // You might need to use HTML parsing libraries or regex based on your needs
            // Example: 
            // var scholarshipInfo = new ScholarshipInfo(response, htmlAttribute);

            // Replace the example with your actual implementation

            ScholarshipInfo scholarship = new ScholarshipInfo
            {
                Id = 1,
                Budget = 5000m,
                Title = "Science Scholarship",
                Location = "New York",
                SchoolName = "XYZ University",
                CategoryId = 2,
                AuthorId = 3,
                IsInSite = true
            };
            return scholarship;
        }
    }
}
