using Mooshak2.Models;
using Mooshak2.Models.Entities;
using Mooshak2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak2.Controllers
{
    public class TeacherController : Controller
    {
		private ApplicationDbContext _db = new ApplicationDbContext();
		private ApplicationUserManager _userManager;
		private ApplicationRoleManager _roleManager;


		// *** INDEX *** //


		// GET: /Teacher/Index/
		[Authorize(Roles = "Teacher")]
		#region public ActionResult Index()
		public ActionResult Index()
        {
            return View();
        }
		#endregion


		// *** ASSIGNMENTS *** //


		// GET: /Teacher/Assignments/
		[Authorize(Roles = "Teacher")]
		#region public ActionResult Assignments()
		public ActionResult Assignments()
		{
			List<AssignmentViewModel> theList = new List<AssignmentViewModel>();
			var result = _db.Assignments.ToList();

			foreach (var item in result)
			{
				AssignmentViewModel assignmentViewModel = new AssignmentViewModel();

				assignmentViewModel.ID = item.ID;
				assignmentViewModel.Title = item.Title;
				assignmentViewModel.CourseID = item.CourseID;
				assignmentViewModel.DueDate = item.DueDate;

				assignmentViewModel.CourseName = _db.Courses
													.Where(x => x.ID == item.CourseID)
													.Select(x => x.Name).SingleOrDefault();

				theList.Add(assignmentViewModel);
			}
			return View(theList);
		}
		#endregion

		// GET: /Teacher/CreateAssignments/
		[Authorize(Roles = "Teacher")]
		#region public ActionResult CreateAssignments()
		public ActionResult CreateAssignments()
		{
			List<CourseViewModel> listOfCourses = new List<CourseViewModel>();
			var result = _db.Courses.ToList();

			foreach (var item in result)
			{
				CourseViewModel course = new CourseViewModel();

				course.ID = item.ID;
				course.Name = item.Name;

				listOfCourses.Add(course);
			}
			return View(listOfCourses);
		}
		#endregion

		// POST: /Teacher/CreateAssignments/
		[HttpPost]
		#region public ActionResult CreateAssignments(AssignmentViewModel model)
		[Authorize(Roles = "Teacher")]
		public ActionResult CreateAssignments(AssignmentViewModel model)
		{
			if (ModelState.IsValid)
			{
				var assignment = new Assignment();

				assignment.CourseID = model.CourseID;
				assignment.Title = model.Title;
				assignment.DueDate = model.DueDate;

				_db.Assignments.Add(assignment);
				_db.SaveChanges();
			}
			return Redirect("~/Teacher/Assignments");	
		}
		#endregion


		// *** SERVICES *** ///

	
	}
}