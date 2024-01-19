using System;
using Microsoft.AspNetCore.Mvc;
using MongoDBTrainingExercise.Models.ViewModels;
using MongoDBTrainingExercise.Services;

namespace MongoDBTrainingExercise.Controllers
{
    public class StudentController : Controller
    {
        private readonly Students _studentService;

        public StudentController(Students studentService)
        {
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            var viewModel = _studentService.Get();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentViewModel viewModel)
        {
            var student = _studentService.Create(viewModel);

            if(student)
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
            var viewModel = _studentService.GetById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(StudentViewModel viewModel)
        {
            var student = _studentService.Update(viewModel);

            if (student)
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
            var viewModel = _studentService.GetById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(StudentViewModel viewModel)
        {
            var student = _studentService.Delete(viewModel);

            if (student)
            {
                TempData["PromptCreate"] = "Successfully deleted!";
            }
            else
            {
                TempData["PromptCreate"] = "Failed to deleted!";
            }

            //return View();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Restore()
        {
            var viewModel = _studentService.GetAllInactive();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Restore(int id)
        {
            var viewModel = _studentService.GetById(id);
            var student = _studentService.Restore(viewModel);

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
