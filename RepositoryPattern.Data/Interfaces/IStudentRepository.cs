using RepositoryPattern.Data.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern.Data.Interfaces
{
    public interface IStudentRepository
    {
        void SaveStudent(Student student);
        IEnumerable<Student> GetAllStudents();
        Student GetStudent(long id);
        void DeleteStudent(long id);
        void UpdateStudent(Student student);
    }
}
