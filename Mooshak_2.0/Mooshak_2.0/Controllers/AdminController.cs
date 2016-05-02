using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mooshak_2._0.Repositories;
using Mooshak_2._0.Models.Entities;

namespace Mooshak_2._0.Controllers
{
    public class AdminController : Controller
    {
        private readonly CourseRepository _courseRepository = new CourseRepository();
		private readonly ApplicationUserRepository _userRepository = new ApplicationUserRepository();

		// GET: Admin
		public ActionResult Index()
        {
            return View();
        }

		#region Courses

		public ActionResult CreateCourse()
		{
            List<CourseViewModel> list = _courseRepository.GetCourses();
            return View(list);
		}

        [HttpPost]
        public ActionResult CreateCourse(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                _courseRepository.Add(model);
            }

            // Fetch the list again
            List<CourseViewModel> list = _courseRepository.GetCourses();
            return View(list);
        }

		#endregion

		#region Users
		public ActionResult CreateUser()
		{		
			return View();
		}

		[HttpPost]
		public ActionResult CreateUser(ApplicationUserViewModel model)
		{
			if(ModelState.IsValid)
			{
				_userRepository.Add(model);
			}

			return View();
		}

		#endregion
	}
}