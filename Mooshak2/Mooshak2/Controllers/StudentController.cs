using Mooshak2.Models;
using Mooshak2.Models.Entities;
using Mooshak2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak2.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Student
        [Authorize(Roles = "Student")]
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

        // POST: Student
        [Authorize(Roles = "Student")]
        [HttpPost]
        public ActionResult SubmitSolution(SubmissionViewModel model)
        {
            Submission newSubmission = new Submission();

            newSubmission.MilestoneID = model.MilestoneID;

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


        [Authorize(Roles = "Student")]
        public ActionResult YourSubmissions()
        {
            return View();
        }
    }
}