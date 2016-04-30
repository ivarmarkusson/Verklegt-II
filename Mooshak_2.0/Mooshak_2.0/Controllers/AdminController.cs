using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak_2._0.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult CreateCourse()
		{
			return View();
		}

		public ActionResult AddUser()
		{
			return View();
		}
	}
}