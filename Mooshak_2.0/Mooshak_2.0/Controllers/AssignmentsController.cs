using Mooshak_2._0.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak_2._0.Controllers
{
    public class AssignmentsController : Controller
    {


		//Test comment
        private AssignmentRepository _service = new AssignmentRepository();
        // GET: Assignments
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var viewModel = _service.GetAssignmentByID(id);

            return View(viewModel);
        }
    }
}