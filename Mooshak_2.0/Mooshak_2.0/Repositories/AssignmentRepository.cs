using Mooshak_2._0.Models;
using Mooshak_2._0.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak_2._0.Repositories
{
    public class AssignmentRepository
    {
        private ApplicationDbContext _db;

        public AssignmentRepository()
        {
            _db = new ApplicationDbContext();
        }
        public List<AssignmentViewModel> GetAssignmentsInCourse(int courseID)
        {
            return null;
        }

        public AssignmentViewModel GetAssignmentByID(int assignmentID)
        {
            var assignment = _db.Assignments.SingleOrDefault(x => x.ID == assignmentID);
            if(assignment == null)
            {
                //TODO: kasta villu!
            }

            var milestones = _db.Milestones
                .Where(x => x.AssignmentID == assignmentID)
                .Select(x => new AssignementMilestoneViewModel
                {
                    Title = x.Title
                })
                .ToList();



            var viewModel = new AssignmentViewModel
            {
                Title = assignment.Title,
                Milestones = milestones
            };

            return viewModel;
        }
    }
}