using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak_2._0.Models.ViewModels
{
    public class AssignmentViewModel
    {
        public string Title { get; set; }
        public List<AssignementMilestoneViewModel> Milestones { get; set; }
 
    }
}