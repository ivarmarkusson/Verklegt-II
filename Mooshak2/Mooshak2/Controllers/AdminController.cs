using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Mooshak2.Models;
using PagedList;
using Mooshak2.Models.Entities;
using Mooshak2.Models.ViewModels;

namespace Mooshak2.Controllers
{
	public class AdminController : Controller
	{
		private ApplicationDbContext _db = new ApplicationDbContext();
		private ApplicationUserManager _userManager;
		private ApplicationRoleManager _roleManager;


		// *** INDEX *** //


		// GET: /Admin/Index
		[Authorize(Roles = "Administrator")]
		#region public ActionResult Index()
		public ActionResult Index()
		{

			// Hér er hægt að gera eitthvað

			return View();
		}
		#endregion


		//  *** USERS *** //


		// GET: /Admin/Users
		[Authorize(Roles = "Administrator")]
		#region public ActionResult Users(string searchStringUserNameOrEmail, string currentFilter, int? page)
		public ActionResult Users(string searchStringUserNameOrEmail, string currentFilter, int? page)
		{
			try
			{
				#region Searh Code
				int intPage = 1;
				int intPageSize = 5;
				int intTotalPageCount = 0;

				if (searchStringUserNameOrEmail != null)
				{
					intPage = 1;
				}
				else
				{
					if (currentFilter != null)
					{
						searchStringUserNameOrEmail = currentFilter;
						intPage = page ?? 1;
					}
					else
					{
						searchStringUserNameOrEmail = "";
						intPage = page ?? 1;
					}
				}
				#endregion

				ViewBag.CurrentFilter = searchStringUserNameOrEmail;

				List<UserViewModel> ViewModelUsers = new List<UserViewModel>();
				int intSkip = (intPage - 1) * intPageSize;

				int TotalPageCount = UserManager.Users
					.Where(x => x.UserName.Contains(searchStringUserNameOrEmail))
					.Count();

				var result = UserManager.Users
					.Where(x => x.UserName.Contains(searchStringUserNameOrEmail))
					.OrderBy(x => x.UserName)
					.Skip(intSkip)
					.Take(intPageSize)
					.ToList();

				foreach (var item in result)
				{
					UserViewModel objUserViewModel = new UserViewModel();

					objUserViewModel.UserName = item.UserName;
					objUserViewModel.Email = item.Email;
					objUserViewModel.LockoutEndDateUtc = item.LockoutEndDateUtc;

					ViewModelUsers.Add(objUserViewModel);
				}

				// Set the number of pages
				var _UserDTOAsIPagedList =
					new StaticPagedList<UserViewModel>
					(
						ViewModelUsers, intPage, intPageSize, intTotalPageCount
					);

				return View(_UserDTOAsIPagedList);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "Error: " + ex);
				List<UserViewModel> col_UserDTO = new List<UserViewModel>();

				return View(col_UserDTO.ToPagedList(1, 25));
			}
		}
		#endregion

		// GET: /Admin/Edit/Create 
		[Authorize(Roles = "Administrator")]
		#region public ActionResult CreateUser()
		public ActionResult CreateUser()
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

			UserViewModel user = new UserViewModel();
			user.Courses = listOfCourses;
			ViewBag.Roles = GetAllRolesAsSelectList();

			return View(user);
		}
		#endregion

		// PUT: /Admin/CreateUser
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		#region public ActionResult CreateUser(UserViewModel model)
		public ActionResult CreateUser(UserViewModel model)
		{
			try
			{
				if (model == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}

				var Email = model.Email.Trim();
				var UserName = model.Email.Trim();
				var Password = model.Password.Trim();

				if (Email == "")
				{
					throw new Exception("No Email");
				}

				if (Password == "")
				{
					throw new Exception("No Password");
				}

				// UserName is LowerCase of the Email
				UserName = Email.ToLower();

				// Create user

				var objNewAdminUser = new ApplicationUser { UserName = UserName, Email = Email };
				var AdminUserCreateResult = UserManager.Create(objNewAdminUser, Password);

				if (AdminUserCreateResult.Succeeded == true)
				{
					string strNewRole = Convert.ToString(Request.Form["Roles"]);

					if (strNewRole != "0")
					{
						// Put user in role
						UserManager.AddToRole(objNewAdminUser.Id, strNewRole);
					}

					return Redirect("~/Admin/Users");
				}
				else
				{
					ViewBag.Roles = GetAllRolesAsSelectList();
					ModelState.AddModelError(string.Empty,
						"Error: Failed to create the user. Check password requirements.");
					return View(model);
				}
			}
			catch (Exception ex)
			{
				ViewBag.Roles = GetAllRolesAsSelectList();
				ModelState.AddModelError(string.Empty, "Error: " + ex);
				return View("Create");
			}
		}
		#endregion

		// GET: /Admin/Edit/TestUser 
		[Authorize(Roles = "Administrator")]
		#region public ActionResult EditUser(string UserName)
		public ActionResult EditUser(string UserName)
		{
			if (UserName == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			UserViewModel userViewModel = GetUser(UserName);
			if (userViewModel == null)
			{
				return HttpNotFound();
			}
			return View(userViewModel);
		}
		#endregion

		// PUT: /Admin/EditUser
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		#region public ActionResult EditUser(UserViewModel model)
		public ActionResult EditUser(UserViewModel model)
		{
			try
			{
				if (model == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}

				UserViewModel udatedUserViewModel = UpdateDTOUser(model);

				if (udatedUserViewModel == null)
				{
					return HttpNotFound();
				}

				return Redirect("~/Admin");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "Error: " + ex);
				return View("EditUser", GetUser(model.UserName));
			}
		}
		#endregion

		// DELETE: /Admin/DeleteUser
		[Authorize(Roles = "Administrator")]
		#region public ActionResult DeleteUser(string UserName)
		public ActionResult DeleteUser(string UserName)
		{
			try
			{
				if (UserName == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}

				if (UserName.ToLower() == this.User.Identity.Name.ToLower())
				{
					ModelState.AddModelError(
						string.Empty, "Error: Cannot delete the current user");

					return View("EditUser");
				}

				UserViewModel deletedUser = GetUser(UserName);

				if (deletedUser == null)
				{
					return HttpNotFound();
				}
				else
				{
					DeleteUser(deletedUser);
				}

				return Redirect("~/Admin");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "Error: " + ex);
				return View("EditUser", GetUser(UserName));
			}
		}
		#endregion

		// GET: /Admin/EditRoles/TestUser 
		[Authorize(Roles = "Administrator")]
		#region public ActionResult EditRoles(string UserName)
		public ActionResult EditRoles(string UserName)
		{
			if (UserName == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			UserName = UserName.ToLower();

			// Check that we have an actual user
			UserViewModel userViewModel = GetUser(UserName);

			if (userViewModel == null)
			{
				return HttpNotFound();
			}

			UserAndRolesViewModel userAndRolesViewModel = GetUserAndRoles(UserName);

			return View(userAndRolesViewModel);
		}
		#endregion

		// PUT: /Admin/EditRoles/TestUser 
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		#region public ActionResult EditRoles(UserAndRolesViewModel userAndRolesViewModel)
		public ActionResult EditRoles(UserAndRolesViewModel userAndRolesViewModel)
		{
			try
			{
				if (userAndRolesViewModel == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}

				string UserName = userAndRolesViewModel.UserName;
				string strNewRole = Convert.ToString(Request.Form["AddRole"]);

				if (strNewRole != "No Roles Found")
				{
					// Go get the User
					ApplicationUser user = UserManager.FindByName(UserName);

					// Put user in role
					UserManager.AddToRole(user.Id, strNewRole);
				}

				ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

				UserAndRolesViewModel objUserAndRolesViewModel =
					GetUserAndRoles(UserName);

				return View(objUserAndRolesViewModel);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "Error: " + ex);
				return View("EditRoles");
			}
		}
		#endregion

		// DELETE: /Admin/DeleteRole?UserName="TestUser&RoleName=Administrator
		[Authorize(Roles = "Administrator")]
		#region public ActionResult DeleteRole(string UserName, string RoleName)
		public ActionResult DeleteRole(string UserName, string RoleName)
		{
			try
			{
				if ((UserName == null) || (RoleName == null))
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}

				UserName = UserName.ToLower();

				// Check that we have an actual user
				UserViewModel userViewModel = GetUser(UserName);

				if (userViewModel == null)
				{
					return HttpNotFound();
				}

				if (UserName.ToLower() ==
					this.User.Identity.Name.ToLower() && RoleName == "Administrator")
				{
					ModelState.AddModelError(string.Empty,
						"Error: Cannot delete Administrator Role for the current user");
				}

				// Go get the User
				ApplicationUser user = UserManager.FindByName(UserName);
				// Remove User from role
				UserManager.RemoveFromRoles(user.Id, RoleName);
				UserManager.Update(user);

				ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

				return RedirectToAction("EditRoles", new { UserName = UserName });
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "Error: " + ex);

				ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

				UserAndRolesViewModel objUserAndRolesViewModel =
					GetUserAndRoles(UserName);

				return View("EditRoles", objUserAndRolesViewModel);
			}
		}
		#endregion


		// *** ROLES *** //


		// GET: /Admin/Roles
		[Authorize(Roles = "Administrator")]
		#region public ActionResult Roles()
		public ActionResult Roles()
		{
			var roleManager =
				new RoleManager<IdentityRole>
				(
					new RoleStore<IdentityRole>(new ApplicationDbContext())
				);

			List<RoleViewModel> colRoleDTO = 
				(from objRole 
				 in roleManager.Roles
				 select new RoleViewModel { Id = objRole.Id, RoleName = objRole.Name } 
				).ToList();

			return View(colRoleDTO);
		}
		#endregion

		// GET: /Admin/CreateRole
		[Authorize(Roles = "Administrator")]
		#region public ActionResult CreateRole()
		public ActionResult CreateRole()
		{
			RoleViewModel roleViewModel = new RoleViewModel();

			return View(roleViewModel);
		}
		#endregion

		// PUT: /Admin/CreateRole
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		#region public ActionResult CreateRole(RoleViewModel model)
		public ActionResult CreateRole(RoleViewModel model)
		{
			try
			{
				if (model == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}

				var RoleName = model.RoleName.Trim();

				if (RoleName == "")
				{
					throw new Exception("No RoleName");
				}

				// Create Role
				var roleManager =
					new RoleManager<IdentityRole>(
						new RoleStore<IdentityRole>(new ApplicationDbContext())
						);

				if (!roleManager.RoleExists(RoleName))
				{
					roleManager.Create(new IdentityRole(RoleName));
				}

				return Redirect("~/Admin/Roles");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "Error: " + ex);
				return View("CreateRole");
			}
		}
		#endregion

		// DELETE: /Admin/DeleteUserRole?RoleName=TestRole
		[Authorize(Roles = "Administrator")]
		#region public ActionResult DeleteUserRole(string RoleName)
		public ActionResult DeleteUserRole(string RoleName)
		{
			try
			{
				if (RoleName == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}

				if (RoleName.ToLower() == "administrator")
				{
					throw new Exception(String.Format("Cannot delete {0} Role.", RoleName));
				}

				var roleManager =
					new RoleManager<IdentityRole>(
						new RoleStore<IdentityRole>(new ApplicationDbContext()));

				var UsersInRole = roleManager.FindByName(RoleName).Users.Count();
				if (UsersInRole > 0)
				{
					throw new Exception(
						String.Format(
							"Canot delete {0} Role because it still has users.",
							RoleName)
							);
				}

				var objRoleToDelete = (from objRole in roleManager.Roles
									   where objRole.Name == RoleName
									   select objRole).FirstOrDefault();
				if (objRoleToDelete != null)
				{
					roleManager.Delete(objRoleToDelete);
				}
				else
				{
					throw new Exception(
						String.Format(
							"Canot delete {0} Role does not exist.",
							RoleName)
							);
				}

				List<RoleViewModel> roleViewModel = (from objRole in roleManager.Roles
											select new RoleViewModel
											{
												Id = objRole.Id,
												RoleName = objRole.Name
											}).ToList();

				return View("Roles", roleViewModel);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "Error: " + ex);

				var roleManager =
					new RoleManager<IdentityRole>(
						new RoleStore<IdentityRole>(new ApplicationDbContext()));

				List<RoleViewModel> colRoleDTO = (from objRole in roleManager.Roles
											select new RoleViewModel
											{
												Id = objRole.Id,
												RoleName = objRole.Name
											}).ToList();

				return View("Roles", colRoleDTO);
			}
		}
		#endregion


		// *** COURSES *** //


		// GET: /Admin/Courses
		[Authorize(Roles = "Administrator")]
		#region public ActionResult Courses()
		public ActionResult Courses()
		{
			List<CourseViewModel> theList = new List<CourseViewModel>();
			var result = _db.Courses.ToList();

			foreach (var item in result)
			{
				CourseViewModel courseViewModel = new CourseViewModel();

				courseViewModel.ID = item.ID;
				courseViewModel.Name = item.Name;

				theList.Add(courseViewModel);
			}

			return View(theList);
		}
		#endregion

		// GET: /Admin/CreateCourse/
		[Authorize(Roles = "Administrator")]
		#region public ActionResult CreateCourse()
		public ActionResult CreateCourse()
		{
			List<CourseViewModel> theList = new List<CourseViewModel>();
			var result = _db.Courses.ToList();

			foreach (var item in result)
			{
				CourseViewModel courseViewModel = new CourseViewModel();

				courseViewModel.ID = item.ID;
				courseViewModel.Name = item.Name;

				theList.Add(courseViewModel);
			}

			return View(theList);
		}
		#endregion

		// POST: /Admin/CreateCourse/
		[HttpPost]
		[Authorize(Roles = "Administrator")]
		#region public ActionResult CreateCourse(CourseViewModel model)
		public ActionResult CreateCourse(CourseViewModel model)
		{
			if (ModelState.IsValid)
			{
				var course = new Course();

				course.ID = model.ID;
				course.Name = model.Name;

				_db.Courses.Add(course);
				_db.SaveChanges();
			}

			return Redirect("~/Admin/Courses");
		}
		#endregion

		// GET: /Admin/DeleteCourse/
		[Authorize(Roles = "Administrator")]
		#region public ActionResult DeleteCourse(string Name)
		public ActionResult DeleteCourse(string Name)
		{
			CourseViewModel course = GetCourseByName(Name);

			if (course != null)
			{
				DeleteCourse(course);
			}
			return Redirect("~/Admin/Courses");
		}
		#endregion


		// *** UTILITIES *** //


		// Kommenta Kóða
		#region public ApplicationUserManager UserManager
		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ??
					HttpContext.GetOwinContext()
					.GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}
		#endregion

		// Kommenta Kóða
		#region public ApplicationRoleManager RoleManager
		public ApplicationRoleManager RoleManager
		{
			get
			{
				return _roleManager ??
					HttpContext.GetOwinContext()
					.GetUserManager<ApplicationRoleManager>();
			}
			private set
			{
				_roleManager = value;
			}
		}
		#endregion

		// Kommenta Kóða
		#region private List<SelectListItem> GetAllRolesAsSelectList()
		private List<SelectListItem> GetAllRolesAsSelectList()
		{
			List<SelectListItem> SelectRoleListItems =
				new List<SelectListItem>();

			var roleManager =
				new RoleManager<IdentityRole>(
					new RoleStore<IdentityRole>(new ApplicationDbContext()));

			var colRoleSelectList = roleManager.Roles.OrderBy(x => x.Name).ToList();

			SelectRoleListItems.Add(
				new SelectListItem
				{
					Text = "Select",
					Value = "0"
				});

			foreach (var item in colRoleSelectList)
			{
				SelectRoleListItems.Add(
					new SelectListItem
					{
						Text = item.Name.ToString(),
						Value = item.Name.ToString()
					});
			}

			return SelectRoleListItems;
		}
		#endregion

		// Kommenta Kóða
		#region private CourseViewModel GetCourseByName(string name)
		private CourseViewModel GetCourseByName(string name)
		{
			CourseViewModel course = new CourseViewModel();

			var result = _db.Courses.Where(x => x.Name == name).SingleOrDefault();

			if (result != null)
			{
				course.ID = result.ID;
				course.Name = result.Name;
			}

			return course;
		}
		#endregion

		// Kommenta Kóða og breyta nöfnum á breytum
		#region private void DeleteCourse(CourseViewModel paramCourseViewModel)
		private void DeleteCourse(CourseViewModel paramCourseViewModel)
		{
			Course course = _db.Courses
				.Where(x => x.Name == paramCourseViewModel.Name)
				.SingleOrDefault();

			if (course != null)
			{
				_db.Courses.Remove(course);
				_db.SaveChanges();
			}
		}
		#endregion

		// Kommenta Kóða og breyta nöfnum á breytum
		#region private ExpandedUserDTO GetUser(string paramUserName)
		private UserViewModel GetUser(string paramUserName)
		{
			UserViewModel user = new UserViewModel();

			var result = UserManager.FindByName(paramUserName);

			// If we could not find the user, throw an exception
			if (result == null) throw new Exception("Could not find the User");

			user.UserName = result.UserName;
			user.Email = result.Email;
			user.LockoutEndDateUtc = result.LockoutEndDateUtc;
			user.AccessFailedCount = result.AccessFailedCount;
			user.PhoneNumber = result.PhoneNumber;

			return user;
		}
		#endregion

		// Kommenta Kóða og breyta nöfnum á breytum
		#region private ExpandedUserDTO UpdateDTOUser(ExpandedUserDTO objExpandedUserDTO)
		private UserViewModel UpdateDTOUser(UserViewModel paramExpandedUserDTO)
		{
			ApplicationUser result =
				UserManager.FindByName(paramExpandedUserDTO.UserName);

			// If we could not find the user, throw an exception
			if (result == null)
			{
				throw new Exception("Could not find the User");
			}



			result.Email = paramExpandedUserDTO.Email.Trim();
			result.UserName = paramExpandedUserDTO.Email.Trim();

			// Lets check if the account needs to be unlocked
			if (UserManager.IsLockedOut(result.Id))
			{
				// Unlock user
				UserManager.ResetAccessFailedCountAsync(result.Id);
			}

			UserManager.Update(result);

			// Was a password sent across?
			if (!string.IsNullOrEmpty(paramExpandedUserDTO.Password))
			{
				// Remove current password
				var removePassword = UserManager.RemovePassword(result.Id);
				if (removePassword.Succeeded)
				{
					// Add new password
					var AddPassword =
						UserManager.AddPassword(
							result.Id,
							paramExpandedUserDTO.Password
							);

					if (AddPassword.Errors.Count() > 0)
					{
						throw new Exception(AddPassword.Errors.FirstOrDefault());
					}
				}
			}

			return paramExpandedUserDTO;
		}
		#endregion

		// Kommenta Kóða og breyta nöfnum á breytum
		#region private void DeleteUser(ExpandedUserDTO paramExpandedUserDTO)
		private void DeleteUser(UserViewModel paramExpandedUserDTO)
		{
			ApplicationUser user =
				UserManager.FindByName(paramExpandedUserDTO.UserName);

			// If we could not find the user, throw an exception
			if (user == null)
			{
				throw new Exception("Could not find the User");
			}

			UserManager.RemoveFromRoles(user.Id, UserManager.GetRoles(user.Id).ToArray());
			UserManager.Update(user);
			UserManager.Delete(user);
		}
		#endregion

		// Kommenta Kóða og breyta nöfnum á breytum
		#region private UserAndRolesDTO GetUserAndRoles(string UserName)
		private UserAndRolesViewModel GetUserAndRoles(string UserName)
		{
			// Go get the User
			ApplicationUser user = UserManager.FindByName(UserName);

			List<UserRoleViewModel> colUserRoleDTO =
				(from objRole in UserManager.GetRoles(user.Id)
				 select new UserRoleViewModel
				 {
					 RoleName = objRole,
					 UserName = UserName
				 }).ToList();

			if (colUserRoleDTO.Count() == 0)
			{
				colUserRoleDTO.Add(new UserRoleViewModel { RoleName = "No Roles Found" });
			}

			ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

			// Create UserRolesAndPermissionsDTO
			UserAndRolesViewModel objUserAndRolesDTO =
				new UserAndRolesViewModel();
			objUserAndRolesDTO.UserName = UserName;
			objUserAndRolesDTO.colUserRoleDTO = colUserRoleDTO;
			return objUserAndRolesDTO;
		}
		#endregion

		// Kommenta Kóða og breyta nöfnum á breytum
		#region private List<string> RolesUserIsNotIn(string UserName)
		private List<string> RolesUserIsNotIn(string UserName)
		{
			// Get roles the user is not in
			var colAllRoles = RoleManager.Roles.Select(x => x.Name).ToList();

			// Go get the roles for an individual
			ApplicationUser user = UserManager.FindByName(UserName);

			// If we could not find the user, throw an exception
			if (user == null)
			{
				throw new Exception("Could not find the User");
			}

			var colRolesForUser = UserManager.GetRoles(user.Id).ToList();
			var colRolesUserInNotIn = (from objRole in colAllRoles
									   where !colRolesForUser.Contains(objRole)
									   select objRole).ToList();

			if (colRolesUserInNotIn.Count() == 0)
			{
				colRolesUserInNotIn.Add("No Roles Found");
			}

			return colRolesUserInNotIn;
		}
		#endregion
	}
}