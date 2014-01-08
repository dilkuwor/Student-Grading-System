using GPA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
/*
 * Project Name: GPA  
 * Date Started: 01/01/2014
 * Description: Handles the feedback module in DAL
 * Module Name: User Administration Module
 * Developer Name: Mehrdad Panahandeh
 * Version: 0.1
 * Date Modified:
 */
namespace GPA.DAL.Manager
{
    public class CourseManager
    {
        public void saveCourse(Cours course)
        {
            using (var db = new GPAEntities())
            {
                db.Entry(course).State = course.Id == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;
                db.SaveChanges();

            }
        }

        public void deleteCourse(int courseId)
        {
            using (var db = new GPAEntities())
            {
                Cours course = new Cours();
                course = db.Courses.Find(courseId);
                db.Courses.Remove(course);
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
            IQueryable<Cours> list;
            var db = new GPAEntities();
            list = db.Courses.Where(c => c.CourseName.ToUpper().Contains(searchString.ToUpper()));
            return list.ToList();
        }
    }
}