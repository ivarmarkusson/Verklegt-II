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

				assignmentViewModel.MilestoneTitle = _db.Milestones.Where(x => x.AssignmentID == item.ID)
														.Select(x => x.Title)
														.SingleOrDefault();

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
		[Authorize(Roles = "Teacher")]
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
                assignment.StartDate = model.Startdate;
                assignment.Languages = model.Languages;
                assignment.GroupSize = model.GroupSize;

				_db.Assignments.Add(assignment);
				_db.SaveChanges();

				var milestone = new Milestone();

				milestone.Title = model.MilestoneTitle;
				milestone.AssignmentID = assignment.ID;
				milestone.Percentage = model.MilestonePercentage;

				_db.Milestones.Add(milestone);
				_db.SaveChanges();


			}
			return Redirect("~/Teacher/Assignments");	
		}
		#endregion

		// GET: /Teacher/DeleteAssignment/
		[Authorize(Roles = "Teacher")]
		#region public ActionResult DeleteAssignment(string title)
		public ActionResult DeleteAssignment(string title)
		{
			AssignmentViewModel assignmentViewModel = getAssignmentByTitle(title);

			if (assignmentViewModel != null)
			{
				// Hér þarf að bæta við logic
				// til að eyða milestones þegar
				// assignment er eytt

				DeleteAssignment(assignmentViewModel);
			}
			return Redirect("~/Teacher/Assignments");
		}
		#endregion


		// *** MILESTONES *** //



		// *** SUBMISSIONS *** //


		[Authorize(Roles = "Teacher")]
		public ActionResult Submissions()
		{
			return View();
		}



		// *** SERVICES *** ///


		// Kommenta Kóða
		#region private AssignmentViewModel getCourseByTitle(string title)
		private AssignmentViewModel getAssignmentByTitle(string title)
		{
			AssignmentViewModel assignment = new AssignmentViewModel();
			var result = _db.Assignments.Where(x => x.Title == title).SingleOrDefault();

			if (result != null)
			{
				assignment.ID = result.ID;
				assignment.Title = result.Title;
				assignment.DueDate = result.DueDate;
				assignment.CourseID = result.CourseID;
			}
			return assignment;
		}
		#endregion

		// Kommenta Kóða
		#region private void DeleteAssignment(AssignmentViewModel assignmentViewModel)
		private void DeleteAssignment(AssignmentViewModel assignmentViewModel)
		{
			Assignment assignment = _db.Assignments
				.Where(x => x.Title == assignmentViewModel.Title)
				.SingleOrDefault();

			if (assignment != null)
			{
				_db.Assignments.Remove(assignment);
				_db.SaveChanges();
			}
		}
		#endregion

	}
}