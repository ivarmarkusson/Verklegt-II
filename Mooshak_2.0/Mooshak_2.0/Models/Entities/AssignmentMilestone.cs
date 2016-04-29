using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak_2._0.Models.Entities
{
    /// <summary>
    /// Assignment can be split in to many parts(Milestones), were each milestone can weight different percentage.
    /// </summary>
    public class AssignmentMilestone
    {
        public int ID { get; set; }
        public int AssignmentID { get; set; }
        public string Title { get; set; }
        public double Percentage { get; set; }
    }
}