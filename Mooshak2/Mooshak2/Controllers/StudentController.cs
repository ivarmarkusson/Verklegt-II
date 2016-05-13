using Mooshak2.Models;
using Mooshak2.Models.Entities;
using Mooshak2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var filename = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/Submissions"), filename);

            if (file.ContentLength > 0 && file != null)
            {
                newSubmission.SubmissionPath = path;
                newSubmission.SubmissionFileName = filename;
                file.SaveAs(path);
            }

            _db.Submissions.Add(newSubmission);
            _db.SaveChanges();

            var workingFolder = Server.MapPath("~/Content/Submissions");
            var exeFileName = filename.Replace(".cpp", ".exe");
            var exeFilePath = workingFolder + exeFileName;
            var compilerFolder = "C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\VC\\bin\\";

            Process compiler = new Process();
            compiler.StartInfo.FileName = "cmd.exe";
            compiler.StartInfo.WorkingDirectory = workingFolder;
            compiler.StartInfo.RedirectStandardInput = true;
            compiler.StartInfo.RedirectStandardOutput = true;
            compiler.StartInfo.UseShellExecute = false;

            compiler.Start();
            compiler.StandardInput.WriteLine("\"" + compilerFolder + "vcvars32.bat" + "\"");
            compiler.StandardInput.WriteLine("cl.exe /nologo /EHsc " + filename);
            compiler.StandardInput.WriteLine("exit");
            string output = compiler.StandardOutput.ReadToEnd();
            compiler.WaitForExit();
            compiler.Close();

            if (System.IO.File.Exists(exeFilePath))
            {
                var processInfoExe = new ProcessStartInfo(exeFilePath, "");
                processInfoExe.UseShellExecute = false;
                processInfoExe.RedirectStandardOutput = true;
                processInfoExe.RedirectStandardError = true;
                processInfoExe.CreateNoWindow = true;
                using (var processExe = new Process())
                {
                    processExe.StartInfo = processInfoExe;
                    processExe.Start();
                    // þurfum að setja innputin hér!
                    var lines = new List<string>();
                    while (!processExe.StandardOutput.EndOfStream)
                    {
                        lines.Add(processExe.StandardOutput.ReadLine());
                    }
                }
            }
            return Redirect("~/Student/YourSubmissions");
        }


        [Authorize(Roles = "Student")]
        public ActionResult YourSubmissions()
        {
            return View();
        }
    }
}