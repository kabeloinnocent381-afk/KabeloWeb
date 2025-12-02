// Models/GitHubRepo.cs
namespace KabeloWeb.Models
{
    public class GitHubRepo
    {
        public string Name { get; set; } = "";
        public string HtmlUrl { get; set; } = "";
        public string Description { get; set; } = "";
        public string Language { get; set; } = "";
        public int StargazersCount { get; set; }
    }
}
