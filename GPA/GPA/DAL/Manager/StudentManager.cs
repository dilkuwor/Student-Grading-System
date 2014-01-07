using GPA.DAL.Extended;
using GPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
 * Project Name: GPA  
 * Date Started: 01/01/2014
 * Description: Handles Student business logic
 * Module Name: User Administration Module
 * Developer Name: Dil Kuwor
 * Version: 0.1
 * Date Modified:
 * 
 */
namespace GPA.DAL.Manager
{
    public class StudentManager
    {

        /// <summary>
        /// Selects the course already taken by the student by student id
        /// </summary>
        /// <param name="userid">Student id</param>
        /// <returns></returns>
        public List<Cours> GetRegisteredCourse(int userid)
        {
            List<Cours> courses = new List<Cours>();
            using (var db = new  GPAEntities())
            {
                
                courses = (from c in db.Courses
                              join cu in db.CourseUsers on c.Id equals cu.Courses_Id
                              where cu.Users_Id == userid
                              select c).ToList();
            }
            return courses;
        }

        /// <summary>
        /// Returns the list of courses not registered by the student
        /// </summary>
        /// <param name="userid">Student id</param>
        /// <returns></returns>
        public List<Cours> GetCourseList(int userid)
        {
            List<Cours> courses = new List<Cours>();
            using (var db = new GPAEntities())
            {
                Registration ruser  = db.Registrations.Where(r => r.UserID == userid).Single();

                courses = (from c in db.Courses
                           join cu in db.CourseUsers on c.Id equals cu.Courses_Id
                           where cu.Users_Id != ruser.RegistrationID
                           select c).ToList();
            }
            return courses;
        }


        /// <summary>
        /// Saves the course registration request for each block
        /// </summary>
        /// <param name="courseid"></param>
        /// <param name="studentid"></param>
        public void ApplyForCourse(int courseid,int studentid)
        {
            Registration ruser;
            using (var db = new GPAEntities())
            {
                ruser = db.Registrations.Where(r => r.UserID == studentid).Single();
            }
            CourseEnrolment request = new CourseEnrolment();
            request.CourseRef_ID = courseid;
            request.UserRef_ID = ruser.RegistrationID;
            request.Date = DateTime.Now.ToString("yyyy-MM-dd");
            request.IsApproved = false;
            using (var db =new  GPAEntities())
            {
                db.CourseEnrolments.Add(request);
                db.SaveChanges();
            }

        }


        /// <summary>
        /// Returns the custom course list which contains extra boolean filed IsCourseRequested
        /// extra boolean filed is used to disable to row
        /// </summary>
        /// <param name="studentid"></param>
        /// <returns></returns>
        public List<ECourse> GetCustomCourses(int studentid)
        {
            Registration ruser;
            List<ECourse> courses = new List<ECourse>();

            List<Cours> _courses = GetCourseList(studentid);
            using (var db = new GPAEntities())
            {
                ruser = db.Registrations.Where(r => r.UserID == studentid).Single();

                 courses = (from c in _courses
                             select new ECourse
                             {
                                 CourseName = c.CourseName,
                                 Id = c.Id,
                                 Credit = c.Credit,
                                 Level = c.Level,
                                 SubCode = c.SubCode,
                                 IsRequested = IsCourseRequested(ruser.RegistrationID,c.Id),

                             }).ToList();
                             
            }

            return courses;
        }

        public bool IsCourseRequested( int userid,int courseid)
        {
            CourseEnrolment cr ;
            using (var db = new GPAEntities())
            {
                
                cr = db.CourseEnrolments.Where(r => (r.UserRef_ID == userid && r.CourseRef_ID == courseid)).SingleOrDefault();
            }

            if (cr != null)
                return true;
            else
                return false;
        }


    }
}