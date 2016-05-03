using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Mooshak_2._0.Models;
using Mooshak_2._0.Models.Entities;
using Mooshak_2._0.Models.ViewModels;
using Mooshak_2._0.Repositories;

namespace Mooshak_2._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationUserRepository _userRepository = new ApplicationUserRepository();
        MyApplicationUserManager _userManager = new MyApplicationUserManager();

        public ActionResult Index()
        {

			return View();
        }

        [HttpPost]
        public ActionResult Index(ApplicationUserViewModel model)
        {
            // TODO: Find out why I can't access a controller with Authorize attribute
			// TODO: Login authentication

            var user = _userManager.FindByName(model.UserName);
            if (user != null)
            {
                if (_userManager.CheckPassword(user, model.Password))
                {
                    var ci = _userManager.CreateIdentity(user, "Cookie");

                    var ctx = Request.GetOwinContext();
                    ctx.Authentication.SignIn(ci);

                    return Redirect("~/Admin");
                }
            }

            ModelState.AddModelError("", "Invalid username or password");

            return View();
        }
    }
}