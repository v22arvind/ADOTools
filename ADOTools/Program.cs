using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace ADOTools
{
    class Program
    {
        static async Task ListGitHubIssues()
        {
            string owner = "v22arvind";
            string repo = "ADOTools";
            string url = $"https://api.github.com/repos/{owner}/{repo}/issues";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("ADOToolsApp/1.0");

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var doc = JsonDocument.Parse(json);
            foreach (var issue in doc.RootElement.EnumerateArray())
            {
                string title = issue.GetProperty("title").GetString();
                int number = issue.GetProperty("number").GetInt32();
                string state = issue.GetProperty("state").GetString();
                Console.WriteLine($"#{number} [{state}] {title}");
            }
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to ADOTools!");
            Console.WriteLine("Listing GitHub issues:");
            await ListGitHubIssues();
        }
    }
}
