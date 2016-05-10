﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models.ViewModels
{
	public class AssignmentViewModel
	{
		public int ID { get; set; }
		public int CourseID { get; set; }
		public string Title { get; set; }
		public DateTime DueDate { get; set; }
		public string CourseName { get; set; }
	}
}
