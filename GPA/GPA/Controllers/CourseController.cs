using GPA.DAL.Manager;
using GPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/*
 * Project Name: GPA  
 * Date Started: 01/06/2014
 * Description: Handles the Course module
 * Module Name: User Administration Module
 * Developer Name: Mehrdad Panahandeh
 * Version: 0.1
 * Date Modified:
 * 
 */

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

        [HttpGet]
        public ActionResult Edit(int id, string pageName)
        {
            using (var db = new GPAEntities())
            {
                var query = from d in db.Courses
                            where d.Id == id
                            select d;
                return View(pageName, query.First());
            }

        }

    
        public ActionResult Create()
        {
            return View();

        }

        public ActionResult DeleteCourse(int id)
        {
            using (var db = new GPAEntities())
            {
                var query = from d in db.Courses
                            where d.Id == id
                            select d;
                return View("Delete", query.First());
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Delete(Cours course)
        {
            try
            {
                cm.deleteCourse(course.Id);
                TempData["MessageDeleted"] = "True";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CourseError", "This Course cannot be deleted       " + ex.Message);
                return View();
            }
        }

        public ActionResult CourseSignup(int courseid)
        {
            User currentUser = (User)Session["currentUser"];
            StudentManager smanager = new StudentManager();
            smanager.ApplyForCourse(courseid, currentUser.UserID);
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

    }
}