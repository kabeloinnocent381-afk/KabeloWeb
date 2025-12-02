// Services/GitHubService.cs
using System.Text.Json;
using KabeloWeb.Models;

namespace KabeloWeb.Services
{
    public class GitHubService
    {
        private readonly HttpClient _http;

        public GitHubService(HttpClient http)
        {
            _http = http;
            _http.DefaultRequestHeaders.UserAgent.ParseAdd("KabeloWeb-App");
            _http.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github.v3+json");
        }

        public async Task<List<GitHubRepo>> GetUserReposAsync(string username)
        {
            var url = $"https://api.github.com/users/{username}/repos?per_page=100&sort=updated";
            using var res = await _http.GetAsync(url);
            res.EnsureSuccessStatusCode();

            var stream = await res.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);
            var root = doc.RootElement;

            var list = new List<GitHubRepo>();
            foreach (var item in root.EnumerateArray())
            {
                list.Add(new GitHubRepo
                {
                    Name = item.GetProperty("name").GetString() ?? "",
                    HtmlUrl = item.GetProperty("html_url").GetString() ?? "",
                    Description = item.TryGetProperty("description", out var d) && d.ValueKind != JsonValueKind.Null ? d.GetString() ?? "" : "",
                    Language = item.TryGetProperty("language", out var l) && l.ValueKind != JsonValueKind.Null ? l.GetString() ?? "" : "",
                    StargazersCount = item.TryGetProperty("stargazers_count", out var s) ? s.GetInt32() : 0
                });
            }
            return list;
        }
    }
}
