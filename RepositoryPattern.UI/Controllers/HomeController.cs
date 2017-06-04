using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.UI.ViewModels;
using RepositoryPattern.Data.Interfaces;

namespace RepositoryPattern.UI.Controllers
{
    public class HomeController : Controller
    {
        private IStudentRepository studentRepository;

        public HomeController(IStudentRepository studentRepository)
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
        public IActionResult About()
        {
            return View();
        }
    }
}