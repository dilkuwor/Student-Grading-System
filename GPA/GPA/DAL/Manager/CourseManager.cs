using GPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPA.DAL.Manager
{
    public class CourseManager
    {
        public void saveCourse(Cours course)
        {
            using (var db = new GPAEntities())
            {
                db.Courses.Attach(course);
                db.SaveChanges();

            }
        }

        public List<Cours> getCourses()
        {
            List<Cours> courses = new List<Cours>();
            using (var db = new GPAEntities())
            {
                courses = (List<Cours>)db.Courses.ToList();
            }
            return courses;
        }

        public List<Cours> getCoursesByName(string searchString)
        {
            List<Cours> courses = new List<Cours>();
            using (var db = new GPAEntities())
            {
                courses = (List<Cours>)db.Courses.Where(c => c.CourseName.ToUpper().Contains(searchString.ToUpper()));
            }
            return courses;
        }
    }
}