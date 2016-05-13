using Mooshak2.Models;
using Mooshak2.Models.Entities;
using Mooshak2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace Mooshak2.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
		private ApplicationUserManager _userManager;


		// *** SUBMISSIONS *** //


		// GET: /Student/SubmitSolution
		[Authorize(Roles = "Student")]
		#region public ActionResult SubmitSolution()
		public ActionResult SubmitSolution()
        {
            var milestones = _db.Milestones.ToList();
            
            List<MilestoneViewModel> models = new List<MilestoneViewModel>();
            
            foreach(var milestone in milestones)
            {
                MilestoneViewModel newModel = new MilestoneViewModel();

                newModel.ID = milestone.ID;
                newModel.Percentage = milestone.Percentage;
                newModel.Title = milestone.Title;

                models.Add(newModel);
            }

            return View(models);
        }
		#endregion


		// POST: /Student/SubmitSolution
		[Authorize(Roles = "Student")]
        [HttpPost]
		#region public ActionResult SubmitSolution(SubmissionViewModel model)
		public ActionResult SubmitSolution(SubmissionViewModel model)
        {
			var userId = User.Identity.GetUserId();

			Submission newSubmission = new Submission();

            newSubmission.MilestoneID = model.MilestoneID;
			newSubmission.UserID = userId;

            HttpPostedFileBase file = model.SubmissionFile;

            if(file.ContentLength > 0 && file != null)
            {
                var filename = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Submissions"), filename);

                newSubmission.SubmissionPath = path;
                newSubmission.SubmissionFileName = filename;
                file.SaveAs(path);
            }
			
			_db.Submissions.Add(newSubmission);
            _db.SaveChanges();
        
            return Redirect("~/Student/YourSubmissions");
        }
		#endregion

		// GET: /Student/YourSubmissions
		[Authorize(Roles = "Student")]
		#region public ActionResult YourSubmissions()
		public ActionResult YourSubmissions()
        {
            return View();
        }
		#endregion
	}

	// *** UTILITIES ***//


}