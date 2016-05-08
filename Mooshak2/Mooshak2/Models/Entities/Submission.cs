using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models.Entities
{
	public class Submission
	{
		public int ID { get; set; }
		public string Input { set; get; }
		public string Output { set; get; }
	}
}