using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Data.Interfaces;
using RepositoryPattern.Data.BaseClasses;
using Microsoft.AspNetCore.Http;
using RepositoryPattern.UI.ViewModels;

namespace RepositoryPattern.UI.Controllers
{
    public class StudentController : Controller
    {
        private IStudentRepository studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<StudentViewModel> model = studentRepository.GetAllStudents().Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = $"{s.FirstName} {s.LastName}",
                EnrollmentNo = s.EnrollmentNo,
                Email = s.Email
            });
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult AddEditStudent(long? id)
        {
            StudentViewModel model = new StudentViewModel();
            if (id.HasValue)
            {
                Student student = studentRepository.GetStudent(id.Value); if (student != null)
                {
                    model.Id = student.Id;
                    model.FirstName = student.FirstName;
                    model.LastName = student.LastName;
                    model.EnrollmentNo = student.EnrollmentNo;
                    model.Email = student.Email;
                }
            }
            return PartialView("~/Views/Student/_AddEditStudent.cshtml", model);
        }
        [HttpPost]
        public ActionResult AddEditStudent(long? id, StudentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isNew = !id.HasValue;
                    Student student = isNew ? new Student
                    {
                        AddedDate = DateTime.UtcNow
                    } : studentRepository.GetStudent(id.Value);
                    student.FirstName = model.FirstName;
                    student.LastName = model.LastName;
                    student.EnrollmentNo = model.EnrollmentNo;
                    student.Email = model.Email;
                    student.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    student.ModifiedDate = DateTime.UtcNow;
                    if (isNew)
                    {
                        studentRepository.SaveStudent(student);
                    }
                    else
                    {
                        studentRepository.UpdateStudent(student);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteStudent(long id)
        {
            Student student = studentRepository.GetStudent(id);
            StudentViewModel model = new StudentViewModel
            {
                Name = $"{student.FirstName} {student.LastName}"
            };
            return PartialView("~/Views/Student/_DeleteStudent.cshtml", model);
        }

        [HttpPost]
        public IActionResult DeleteStudent(long id, FormCollection form)
        {
            studentRepository.DeleteStudent(id);
            return RedirectToAction("Index");
        }
    }
}