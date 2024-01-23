using Microsoft.AspNetCore.Mvc;
using MongoDBTrainingExercise.Models;
using MongoDBTrainingExercise.Services;
using System.Diagnostics;

namespace MongoDBTrainingExercise.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserAccounts _userAccountService;

        public HomeController(ILogger<HomeController> logger, UserAccounts userAccountService)
        {
            _logger = logger;
            _userAccountService = userAccountService;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32(sessionUserId) ?? 0 ;
            var viewModel = userId != 0 ? _userAccountService.GetById(userId) : null;


            return View(viewModel);


            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
