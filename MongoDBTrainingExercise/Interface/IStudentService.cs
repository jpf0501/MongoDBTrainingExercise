using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDBTrainingExercise.Models;
using MongoDBTrainingExercise.Services;


namespace MongoDBTrainingExercise.Interface
{
    public interface IStudentService
    {
        Task<List<Student>> Get();
        Task Create(Student student);
        Task Update(Student student);
        Task Delete(string id);
    }
}
