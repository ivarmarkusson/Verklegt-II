using Mooshak2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak2.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        [Authorize(Roles = "Student")]
        public ActionResult SubmitSolution()
        {
            return View();
        }

        [Authorize(Roles = "Student")]
        [HttpPost]
        public ActionResult SubmitSolution(SubmissionViewModel model)
        {
            return View();
        }

        [Authorize(Roles = "Student")]
        public ActionResult YourSubmissions()
        {
            return View();
        }
    }
}