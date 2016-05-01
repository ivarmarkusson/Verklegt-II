using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mooshak_2._0.Services;

namespace Mooshak_2._0.Controllers
{
    public class AdminController : Controller
    {
        private readonly CourseService _repository = new CourseService();

       

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult CreateCourse()
		{
            List<CourseViewModel> list = _repository.GetCourses();
            return View(list);
		}

        [HttpPost]
        public ActionResult CreateCourse(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(model);
                // do your stuff like: save to database and redirect to required page.
            }

            // Fetch the list again
            List<CourseViewModel> list = _repository.GetCourses();
            return View(list);
        }

        public ActionResult AddUser()
		{
			return View();
		}
	}
}