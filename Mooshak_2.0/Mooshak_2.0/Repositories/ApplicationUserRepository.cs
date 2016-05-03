using AutoMapper;
using Mooshak_2._0.Models;
using Mooshak_2._0.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using Mooshak_2._0.Models.ViewModels;

namespace Mooshak_2._0.Repositories
{
	public class ApplicationUserRepository
	{
		private ApplicationDbContext _db;

		public ApplicationUserRepository()
		{
			_db = new ApplicationDbContext();
		}

		public ApplicationUserViewModel Add(ApplicationUserViewModel model)
		{
			// Map the viewModel to the Entity
			var user = Mapper.Map<ApplicationUser>(model);

			// Save entity to Database
			_db.Users.Add(user);
			_db.SaveChanges();

			// Map the Entity back to the viewModel and return the viewModel
			return Mapper.Map<ApplicationUserViewModel>(user);
		}

		public List<ApplicationUserViewModel> GetApplicationUsers()
		{
			var users = _db.Users.ToList();

			// Map the Entity to the viewModel and return the viewModel
			return Mapper.Map<List<ApplicationUserViewModel>>(users);
		}

	    public ApplicationUser GetUserByUsername(string username)
	    {
	        return _db.Users.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());
	    }
	}
}