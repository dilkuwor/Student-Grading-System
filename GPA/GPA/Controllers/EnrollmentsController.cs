using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPA.Models;
using GPA.DAL.Manager;

namespace GPA.Controllers
{
    public class EnrollmentsController : Controller
    {
        //
        // GET: /Enrollments/
        public ActionResult Index()
        {
            CourseViewModel cv = new CourseViewModel();
            CourseManager cm = new CourseManager();
            cv.CoursesList = from course in cm.getCourses()
                             select new SelectListItem
                             {
                                 Text = course.CourseName,
                                 Value = ((int)course.Id).ToString()
                             };
            return View(cv);
        }
	}
}