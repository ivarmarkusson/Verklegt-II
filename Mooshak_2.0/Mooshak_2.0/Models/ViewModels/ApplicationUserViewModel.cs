using Mooshak_2._0.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mooshak_2._0.Models.Entities
{
	public class ApplicationUserViewModel
	{
		public string Email { set; get; }
		[Required]
		public string FullName { set; get; }
		[Required]
		public string Password { set; get; }
		[Required]
		public string UserName { set; get; }
	}
}