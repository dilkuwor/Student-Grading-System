using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPA.Models;
using GPA.DAL.Manager;

namespace GPA.DAL.Extended
{
    public class CourseSearch : ISearch<string, List<Cours>>
    {
        CourseManager cm = new CourseManager();
        public List<Cours> FindByName(string search)
        {
            var courses = cm.getCourses();

            if (!String.IsNullOrEmpty(search))
            {
                courses = cm.getCoursesByName(search);
            }
            return courses;
        }
    }
}