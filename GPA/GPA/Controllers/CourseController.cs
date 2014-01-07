using GPA.DAL.Manager;
using GPA.Models;
using GPA.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPA.Controllers
{
    public class CourseController : Controller
    {
        CourseManager cm = new CourseManager();
        //
        // GET: /AddCourse/
        public ActionResult Index(string searchString)
        {
            CourseViewModel cvm = new CourseViewModel();

            cvm.Courses = cm.getCourses();
            if (!String.IsNullOrEmpty(searchString))
            {
                cvm.Courses = cm.getCoursesByName(searchString);
                
            }
            return View(cvm);

        }

        public ActionResult Edit(int id)
        {
            using (var db = new GPAEntities())
            {
                var query = from d in db.Courses
                            where d.Id == id
                            select d;
                return View(query.First());
            }


        }

        public ActionResult CourseSignup(int courseid)
        {
            User currentUser = (User)Session["currentUser"];
            AccountManager amanager = new AccountManager();
            
            StudentManager smanager = new StudentManager();
            smanager.ApplyForCourse(courseid, amanager.FindUserByUserID(currentUser.UserID).RegistrationID);
            return RedirectToAction("Index","Home");
        }
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Save(CourseViewModel cvm)
        {
            Cours course = new Cours();
            course.Id = cvm.Id;
            course.CourseName = cvm.CourseName;
            course.Level = cvm.Level;
            course.SubCode = cvm.SubCode;
            course.Credit = cvm.Credit;
           
            cm.saveCourse(course);
            return RedirectToAction("Index");
        }
        //public ActionResult AddCourse()
        //{
        //    return View();
        //}

        //public ActionResult AddCourse(CourseViewModel acvm)
        //{

        //    return View();
        //}
    }
}