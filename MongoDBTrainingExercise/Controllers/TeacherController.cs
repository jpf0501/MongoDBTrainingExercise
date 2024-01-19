using System;
using Microsoft.AspNetCore.Mvc;
using MongoDBTrainingExercise.Interface;
using MongoDBTrainingExercise.Models.ViewModels;
using MongoDBTrainingExercise.Services;

namespace MongoDBTrainingExercise.Controllers
{
    public class TeacherController : Controller
    {
        private readonly Teachers _teacherService;

        public TeacherController(Teachers teacherService)
        {
            _teacherService = teacherService;
        }

        public IActionResult Index()
        {
            var viewModel = _teacherService.Get();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TeacherViewModel viewModel)
        {
            var teacher = _teacherService.Create(viewModel);

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
            var viewModel = _teacherService.GetById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(TeacherViewModel viewModel)
        {
            var teacher = _teacherService.Update(viewModel);

            if (teacher)
            {
                TempData["PromptCreate"] = "Successfully updated!";
            }
            else
            {
                TempData["PromptCreate"] = "Failed to update!";
            }

            //return View();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var viewModel = _teacherService.GetById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(TeacherViewModel viewModel)
        {
            var teacher = _teacherService.Delete(viewModel);

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
            var viewModel = _teacherService.GetAllInactive();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Restore(int id)
        {
            var viewModel = _teacherService.GetById(id);
            var student = _teacherService.Restore(viewModel);

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
    }
}
