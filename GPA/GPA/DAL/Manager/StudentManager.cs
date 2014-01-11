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
 * Date Modified:  01/07/2014
 * Modified By: Kengsreng Tang
 * Description: added method getAllStudentsTakenCourse()
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
        public List<Cours> GetAlreadyTakenCoursesByUserID(int userid)
        {
            List<Cours> courses = new List<Cours>();
            using (var db = new GPAEntities())
            {


                courses = (from c in db.Courses
                           join cu in db.CourseUsers on c.Id equals cu.Courses_Id
                           where cu.Users_Id == userid
                           select c
                              ).ToList();
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


                courses = (from c in db.Courses
                           join cu in db.CourseUsers on c.Id equals cu.Courses_Id
                           where cu.Users_Id != userid
                           select c).ToList();
            }
            return courses;
        }

        public List<UserDetail> getAllStudentsTakenCourse(int courseId) 
        {
            List<UserDetail> users = new List<UserDetail>();
            using (var db = new GPAEntities())
            {
                users = (from u in db.UserDetails
                           join ce in db.CourseEnrolments on u.RegistrationID equals ce.UserRef_ID
                         where ce.CourseRef_ID == courseId && ce.IsApproved == true
                           select u
                ).ToList();
            }
            return users;
        }


        /// <summary>
        /// Saves the course registration request for each block
        /// </summary>
        /// <param name="courseid"></param>
        /// <param name="studentid"></param>
        public void ApplyForCourse(int courseid, int studentid)
        {

            CourseEnrolment request = new CourseEnrolment();
            request.CourseRef_ID = courseid;
            request.UserRef_ID = studentid;
            request.Date = DateTime.Now.ToString("yyyy-MM-dd");
            request.IsApproved = false;
            using (var db = new GPAEntities())
            {
                db.CourseEnrolments.Add(request);
                db.SaveChanges();
            }

        }
        /// <summary>
        /// Student can cancel the course signup request so that he can apply for another course
        /// </summary>
        /// <param name="courseid"></param>
        public void CancelCourseRequest(int courseid,int userid)
        {
            using (var db = new GPAEntities())
            {
                CourseEnrolment enrollmentrequest = db.CourseEnrolments.Where(r => r.CourseRef_ID == courseid && r.UserRef_ID==userid).Single();
                db.CourseEnrolments.Remove(enrollmentrequest);
                db.SaveChanges();
            }
        }


        /// <summary>
        /// Returns the custom course list which contains extra boolean filed IsCourseRequested
        /// extra boolean field is used to disable to row
        /// </summary>
        /// <param name="studentid"></param>
        /// <returns></returns>
        public List<ECourse> GetECourses(int studentid)
        {

            List<ECourse> courses = new List<ECourse>();

            //List<Cours> _courses = GetCourseList(studentid);

            using (var db = new GPAEntities())
            {
                List<Cours> _courses = GetCourseList(studentid);


                //courses = (from c in _courses
                //           where !(from c1 in db.Courses join ce in db.CourseEnrolments on c1.Id equals
                //                   ce.CourseRef_ID 
                //                   where (c1.Id == studentid && ce.IsApproved == true) select c1.Id).ToList().Contains(c.Id)
                //           select new ECourse 
                //           {
                //               Id = c.Id,
                //               CourseName = c.CourseName,
                //               IsRequested = IsCourseRequested(studentid,c.Id)

                //           }
                //             ).ToList();

                 courses = (from c in db.GetEcourses(studentid)

                               select new ECourse
                              {
                                  Id = c.Id,
                                  CourseName = c.CourseName,
                                  IsRequested = IsCourseRequested(studentid, c.Id)

                              }).ToList();
                
            }

            return courses;
        }

        public bool IsCourseRequested(int userid, int courseid)
        {
            CourseEnrolment cr;
            using (var db = new GPAEntities())
            {
                try
                {
                    cr = db.CourseEnrolments.Where(r => (r.UserRef_ID == userid && r.CourseRef_ID == courseid && r.IsApproved == false)).SingleOrDefault();
                }
                catch (Exception)
                {

                    return false;
                }
                
            }

            if (cr != null)
                return true;
            else
                return false;
        }


        /// <summary>
        /// Returns the list of students enrolled in a particular course(courseid)
        /// </summary>
        /// <param name="courseid"></param>
        /// <returns>List of User(Student)</returns>
        public List<UserDetail> GetStudentsByCourseID(int courseid)
        {
            List<UserDetail> students = new List<UserDetail>();
            using (var db = new GPAEntities())
            {
                students = (from s in db.UserDetails
                              join ce in db.CourseEnrolments on s.RegistrationID equals ce.UserRef_ID
                              where (ce.IsApproved == true && ce.CourseRef_ID == courseid)
                              select s).ToList();
            }

            return students;
        }


    }
}