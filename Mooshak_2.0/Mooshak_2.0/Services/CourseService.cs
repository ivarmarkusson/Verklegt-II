using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using AutoMapper;
using Mooshak_2._0.Controllers;
using Mooshak_2._0.Models;
using Mooshak_2._0.Models.Entities;

namespace Mooshak_2._0.Services
{
    public class CourseService
    {
        private ApplicationDbContext _db;

        public CourseService()
        {
            _db = new ApplicationDbContext();
        }

        public CourseViewModel Add(CourseViewModel model)
        {
            var course = Mapper.Map<Course>(model);

            // Save entity to Database
            _db.Courses.Add(course);
            _db.SaveChanges();

            // Return the viewmodel
            return Mapper.Map<CourseViewModel>(course);
        }

        public List<CourseViewModel> GetCourses()
        {
            var courses = _db.Courses.ToList();

            return Mapper.Map<List<CourseViewModel>>(courses);
        }
    }
}