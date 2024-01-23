using Microsoft.AspNetCore.Mvc;
using MongoDBTrainingExercise.Models.ViewModels;
using MongoDBTrainingExercise.Services;

namespace MongoDBTrainingExercise.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserAccounts _accountService;

        public LoginController(ILogger<LoginController> logger, UserAccounts accountService)
        {
            _logger = logger;
            _accountService = accountService;

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


    }
}
