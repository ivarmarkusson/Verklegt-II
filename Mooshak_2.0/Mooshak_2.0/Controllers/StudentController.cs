using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak_2._0.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Overview()
		{
			return View();
		}

		public ActionResult SubmitSolution()
		{
			return View();
		}

		public ActionResult Settings()
		{
			return View();
		}
	}
}