using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak_2._0.Models.Entities
{
    public class Assignment
    {
        public int ID { get; set; }
        public int CouresID { get; set; }
        public string Title { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

    }
}