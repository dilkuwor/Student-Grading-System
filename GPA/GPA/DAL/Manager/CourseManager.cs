using GPA.DAL.Extended;
using GPA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
/*
 * Project Name: GPA  
 * Date Started: 01/01/2014
 * Description: Handles the feedback module in DAL
 * Module Name: User Administration Module
 * Developer Name: Dil Kuwor/Mehrdad Panahandeh
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


        /// <summary>
        /// Returns list of signup requested courses 
        /// </summary>
        /// <returns></returns>
        public List<CourseUserRequest> GetRequestedCourses()
        {
            List<CourseUserRequest> requestedcourses = new List<CourseUserRequest>();
            using (var db = new GPAEntities())
            {
                requestedcourses = (from ce in db.CourseEnrolments.Where(r => r.IsApproved == false)
                             join c in db.Courses on ce.CourseRef_ID equals c.Id
                             join ud in db.UserDetails on ce.UserRef_ID equals ud.RegistrationID
                             select new CourseUserRequest
                             {
                                 CourseName = c.CourseName,
                                 Id = c.Id,
                                 StudentName = ud.FName +" "+ud.LName,
                                 RequestID = ce.RequestID

                             }).ToList();

            }

            return requestedcourses;
        }

        /// <summary>
        /// Updates the CourseEnrollment table row sets the IsApproved filed as true
        /// </summary>
        /// <param name="requestid">Enrollment request id</param>
        public void ApproveCourseSignupRequest(int requestid)
        {
            using (var db = new GPAEntities())
            {
                CourseEnrolment courserequest = db.CourseEnrolments.Where(r => r.RequestID == requestid).Single();
                //Add validatin here
                courserequest.IsApproved = true;
                //db.CourseEnrolments.Attach(courserequest);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Returns the list of course for html dropdown helper
        /// </summary>
        public List<SelectListItem> GetCourseForDropdown()
        {          

             return (from courses in getCourses()
                   select new SelectListItem
                   {
                       Text = courses.CourseName,
                       Value = courses.Id.ToString()
                   }).ToList();

        }

        /// <summary>
        /// Returns the list of available grading list
        /// </summary>
        public List<Grade> GetGradeList()
        {
            List<Grade> grades = new List<Grade>();
            using (var db = new GPAEntities())
            {
                grades = db.Grades.ToList();
            }

            return grades;
        }

        /// <summary>
        /// Returns the list of grades for html dropdown helper
        /// </summary>
        public List<SelectListItem> GetGradesForDropdown()
        {
            List<SelectListItem> grades = new List<SelectListItem>();

            using (var db = new GPAEntities())
            {
                grades = (from g in db.Grades.ToList()
                         select new SelectListItem
                    {
                        Text = g.GradeScore,
                        Value = g.Id.ToString()
                    }).ToList();
            }

            return grades;

        }
        /// <summary>
        /// Inserts student grades data 
        /// </summary>
        /// <param name="students"></param>
        /// <param name="grades"></param>
        /// <param name="courseid"></param>
        /// <returns></returns>
        public bool AddStudentGrades(string[] students, string[] grades,string[] extracredits, int courseid)
        {
            List<StudentGrade> studentgrades = new List<StudentGrade>();
            using (var db = new GPAEntities())
            {
                StudentGrade studentgrade;
                for (int count = 0; count < students.Count(); count++)
                {
                    studentgrade = new StudentGrade();
                    studentgrade.CourseId = courseid;
                    
                    studentgrade.GradeId = int.Parse(grades[count]);
                    studentgrade.UserId = int.Parse(students[count]);
                   
                    studentgrade.ExtraCredit = int.Parse(extracredits[count]);

                    studentgrades.Add(studentgrade);

                    
                }

                db.StudentGrades.AddRange(studentgrades);
                db.SaveChanges();
                
            }

            return false;
        }


    }
}