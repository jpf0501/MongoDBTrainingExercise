using System;
using Microsoft.AspNetCore.Mvc;
using MongoDBTrainingExercise.Interface;
using MongoDBTrainingExercise.Models.ViewModels;
using MongoDBTrainingExercise.Services;

namespace MongoDBTrainingExercise.Controllers
{
    public class UserAccountController : BaseController
    {
        private readonly UserAccounts _accountService;

        public UserAccountController(UserAccounts accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            var viewModel = _accountService.Get();
            return View(viewModel);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserAccountViewModel viewModel)
        {
            var teacher = _accountService.Create(viewModel);

            if (teacher)
            {
                TempData["PromptCreate"] = "Successfully created!";
            }
            else
            {
                TempData["PromptCreate"] = "Failed to create!";
            }

            //return View();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _accountService.GetById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(UserAccountViewModel viewModel)
        {
            if(viewModel.passwordUpdate != viewModel.oldPassword && viewModel.isPasswordUpdate == true)
            {
                TempData["PromptInvalid"] = "Passwords do not match";
                return RedirectToAction("Edit");
            }
            else if ((viewModel.passwordUpdate == null || viewModel.password == null) && viewModel.isPasswordUpdate == true)
            {
                TempData["PromptInvalid"] = "Password field cannot be empty";
                return RedirectToAction("Edit");
            }
            else
            {
                if(viewModel.passwordNew != null)
                {
                    viewModel.password = viewModel.passwordNew;
                }

                var teacher = _accountService.Update(viewModel);

                if (teacher)
                {
                    TempData["PromptCreate"] = "Successfully updated!";
                }
                else
                {
                    TempData["PromptCreate"] = "Failed to update!";
                }

                return RedirectToAction("Index");
            }

            //return View();

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var viewModel = _accountService.GetById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(UserAccountViewModel viewModel)
        {
            var teacher = _accountService.Delete(viewModel);

            if (teacher)
            {
                TempData["PromptCreate"] = "Successfully deleted!";
            }
            else
            {
                TempData["PromptCreate"] = "Failed to delete!";
            }

            //return View();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Restore()
        {
            var viewModel = _accountService.GetAllInactive();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Restore(int id)
        {
            var viewModel = _accountService.GetById(id);
            var student = _accountService.Restore(viewModel);

            if (student)
            {
                TempData["PromptCreate"] = "Successfully restored!";
            }
            else
            {
                TempData["PromptCreate"] = "Failed to restore!";
            }

            //return View();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserAccountViewModel viewModel)
        {
            try
            {
                var userObj = _accountService.GetByUsername(viewModel.username, viewModel.password);

                if (userObj != null)
                {
                    HttpContext.Session.SetInt32(sessionUserId, userObj.userId);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Prompt"] = "Incorrect credentials!";
                    return RedirectToAction("Login", "UserAccount");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "UserAccount");
        }
    }
}
