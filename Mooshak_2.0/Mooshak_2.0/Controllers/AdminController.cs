using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Mooshak_2._0.Models;
using Mooshak_2._0.Repositories;
using Mooshak_2._0.Models.Entities;
using Mooshak_2._0.Models.ViewModels;

namespace Mooshak_2._0.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private readonly CourseRepository _courseRepository = new CourseRepository();
		private readonly ApplicationUserRepository _userRepository = new ApplicationUserRepository();
        MyApplicationUserManager mgr = new MyApplicationUserManager();

		// GET: Admin
		public ActionResult Index()
        {
            return View();
        }

        public ActionResult InitData()
        {
            new SampleData().Seed();
            return View("Index");
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
                // Map the viewModel to the Entity
                var user = Mapper.Map<ApplicationUser>(model);

                var result = mgr.Create(user, model.Password);
			    if (result.Succeeded)
			    {
			        return View();
			    }
			}

			return View();
		}

		#endregion
	}
}