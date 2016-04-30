using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak_2._0.Models.Entities
{
	public class ApplicationUsers
	{
		public int ID { set; get; }
		public string Username { set; get; }
		public string Fullname { set; get; }
		public bool IsAdmin { set; get; }
		public string Password { set; get; }

	}
}