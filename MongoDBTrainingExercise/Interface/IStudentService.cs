﻿using MongoDBTrainingExercise.Models;
using MongoDBTrainingExercise.Models.ViewModels;


namespace MongoDBTrainingExercise.Interface
{
    public interface IStudentService
    {
        IEnumerable<StudentViewModel> Get();

        StudentViewModel GetById(int id);
        bool Create(StudentViewModel viewModel);
        bool Update(StudentViewModel viewModel);
        bool Delete(StudentViewModel viewModel);
    }
}
