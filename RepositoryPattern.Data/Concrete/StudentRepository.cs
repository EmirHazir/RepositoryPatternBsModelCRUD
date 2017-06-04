using RepositoryPattern.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using RepositoryPattern.Data.BaseClasses;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RepositoryPattern.Data.Concrete
{
    public class StudentRepository : IStudentRepository
    {
        private ApplicationContext context;
        private DbSet<Student> studentEntity;
        public StudentRepository(ApplicationContext _context)
        {
            this.context = _context;
            studentEntity = _context.Set<Student>();
        }

        public void DeleteStudent(long id)
        {
            Student student = GetStudent(id);
            studentEntity.Remove(student);
            context.SaveChanges();
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return studentEntity.AsEnumerable();
        }

        public Student GetStudent(long id)
        {
            return studentEntity.SingleOrDefault(s => s.Id == id);
        }

        public void SaveStudent(Student student)
        {
            context.Entry(student).State = EntityState.Added;
            context.SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            context.SaveChanges();
        }
    }
}
