using GPA.DAL.Manager;
using GPA.DAL.Util;
using GPA.Models;
using GPA.Models.Manager;
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
 * Developer Name: Dil Kuwor/Mehrdad Panahandeh
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
        /// <summary>
        /// Sends the request for course signup/cancel the signup request
        /// </summary>
        /// <param name="courseid"></param>
        /// <returns></returns>
        public ActionResult CourseSignup(int courseid,string name)
        {
            User currentUser = (User)Session["currentUser"];
            AccountManager amanager = new AccountManager();

            StudentManager smanager = new StudentManager();
            if (name.Equals("cancel"))
            {
                smanager.CancelCourseRequest(courseid, amanager.FindUserByUserID(currentUser.UserID).RegistrationID);
            }
            else
            {
                
                smanager.ApplyForCourse(courseid, amanager.FindUserByUserID(currentUser.UserID).RegistrationID);
            }
           
            return RedirectToAction("Index","Home");
        }

        

        /// <summary>
        /// course request approval
        /// validation fro prerequsite is done here
        /// </summary>
        /// <param name="courseid"></param>
        /// <returns></returns>
        public ActionResult CourseSignupRequestApprove(int courseid)
        {
            CourseManager cmanager = new CourseManager();
            cmanager.ApproveCourseSignupRequest(courseid);
            return RedirectToAction("Index", "Home");
        }
        // GET: /Course/Details/5
        public ActionResult Details(int? id)
        {

            var db = new GPAEntities();
            CourseViewModel cv = new CourseViewModel();
            StudentManager sm = new StudentManager();
            
            var course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            cv.CourseName = course.CourseName;
            cv.Id = course.Id;
            cv.Credit = course.Credit;
            cv.students = sm.getAllStudentsTakenCourse((int)id);
            return View(cv);

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

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddGrade(DashbordViewModel model,FormCollection formCollection)
        {
            string[] studentids = formCollection["item.RegistrationID"].Split(',');
            string[] gradeids = formCollection["GradeEnterFormViewModel.CourseID"].Split(',');
            //string[] _gradeids = new string[gradeids.Count()-1];
            //for (int count = 1; count < gradeids.Count(); count++)
            //{
            //    _gradeids[count-1] = gradeids[count];
            //}
            string[] extracredits = formCollection["count"].Split(',');
            CourseManager cmanager = new CourseManager();
            cmanager.AddStudentGrades( gradeids,studentids, extracredits, model.GradeEnterFormViewModel.CourseID);
            ModelState.AddModelError("", "The user name or password provided is incorrect.");

            Helper helper = new Helper();
            helper.SendGradeNotification(studentids, model.GradeEnterFormViewModel.CourseID, gradeids);
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetCourseByCourseID(DashbordViewModel model)
        {
            CourseManager cmanager = new CourseManager();
            GradeEnterFormViewModel grademodel = new GradeEnterFormViewModel();
            grademodel.CourseList = cmanager.GetCourseForDropdown();
            //grademodel.Grades = cmanager.GetGradeList();
            StudentManager smanager = new StudentManager();
            grademodel.Students = smanager.GetStudentsByCourseID(model.GradeEnterFormViewModel.CourseID);
            grademodel.Grades = cmanager.GetGradesForDropdown();
            model.GradeEnterFormViewModel = grademodel;
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("Index");
            }

            DashbordViewModel m = new DashbordViewModel();
            m.GradeEnterFormViewModel = grademodel;
            return PartialView("~/Views/Account/_GradeEntryPartial.cshtml", m);

        }

    }

   

    #region AdminView Request Region
    
    #endregion
}