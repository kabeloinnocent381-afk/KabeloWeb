// Controllers/HomeController.cs
using System.Diagnostics;
using KabeloWeb.Models;
using KabeloWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace KabeloWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GitHubService _gitHubService;

        public HomeController(ILogger<HomeController> logger, GitHubService gitHubService)
        {
            _logger = logger;
            _gitHubService = gitHubService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About() => View();

        public IActionResult CV() => View();

        public IActionResult Contact() => View();

        public async Task<IActionResult> Projects()
        {
            var username = "kabeloinnocent381-afk";
            var repos = await _gitHubService.GetUserReposAsync(username);
            return View(repos);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
