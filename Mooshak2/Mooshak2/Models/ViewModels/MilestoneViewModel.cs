﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models.ViewModels
{
	public class MilestoneViewModel
	{
		public int ID { get; set; }
		public int AssignmentID { get; set; }
		public string Title { get; set; }
		public double Percentage { get; set; }
		public double Grade { get; set; }
	}
}