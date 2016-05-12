using System;
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
        public DateTime Startdate { get; set; }
        public string Languages { get; set; }
        public int GroupSize { get; set; }
		public string CourseName { get; set; }
		public string MilestoneTitle { get; set; }
		public double MilestonePercentage { get; set; }
		public List<string> MilestonesTitles { get; set; }
        public HttpPostedFileBase DescriptionFile { get; set; }
	}
}

