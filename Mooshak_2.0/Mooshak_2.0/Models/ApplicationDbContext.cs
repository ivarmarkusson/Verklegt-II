using Microsoft.AspNet.Identity.EntityFramework;
using Mooshak_2._0.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mooshak_2._0.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		/// <summary>
		/// Connection between entity classes and database connection
		/// </summary>
		public DbSet<Assignment> Assignments { get; set; }
		public DbSet<AssignmentMilestone> Milestones { set; get; }
		public DbSet<Course> Courses { get; set; }




		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
			Database.CreateIfNotExists();
		}


		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}

	}
}