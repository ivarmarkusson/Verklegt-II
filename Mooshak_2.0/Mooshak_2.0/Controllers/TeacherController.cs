using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak_2._0.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult CreateAssignment()
		{
			return View();
		}

		public ActionResult ReviewAssignment()
		{
			return View();
		}

		public ActionResult Assignments()
		{
			return View();
		}

		public ActionResult Settings()
		{
			return View();
		}
	}
}