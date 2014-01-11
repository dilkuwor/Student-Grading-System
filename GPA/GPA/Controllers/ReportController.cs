using GPA.Models;
using GPA.Models.Manager;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/*
 * Project Name: GPA  
 * Date Started: 01/04/2014
 * Description: Handles user login and registration module
 * Module Name: User Administration Module(001)
 * Developer Name: Dil Kuwor, Laxaman Adhikari
 * Version: 0.1
 * Date Modified:01/06/2014 Laxman Adhikari
 * Date Modified:01/08/2014 Laxman Adhikari
 * 
 */

namespace GPA.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/
        public ActionResult Index()
        {
            ReportViewModel model = new ReportViewModel();

            AccountManager accountmanager = new AccountManager();
            model.UserList = from ruser in accountmanager.GetUserListByRole("Student")
                             select new SelectListItem
                             {
                                 Text = ruser.FName + " " + ruser.LName,
                                 Value = ((int)ruser.RegistrationID).ToString()
                             };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult GradeReport(ReportViewModel model, string command)
        {
            ReportViewModel model1 = new ReportViewModel();

            if (command == "Student Grade")
            {
                using (var ctx = new GPAEntities())
                {
                    var idParam = new SqlParameter
                    {
                        ParameterName = "StudentID",
                        Value = int.Parse(model.UserID.ToString())
                    };
                    model.GPAesult = ctx.Database.SqlQuery<FindGPA_Result>("exec FindGPA @StudentId ", idParam).ToList<FindGPA_Result>();
                    ViewBag.UserID = model.UserID;
                    ViewBag.Reporttype = "Student Grade";
                    ViewBag.ReportName = "StudentGrade.rdlc";
                    return View(model);
                }
            }
            else if (command == "Student Details")
            {
                using (var ctx = new GPAEntities())
                {
                    var idParam = new SqlParameter
                    {
                        ParameterName = "StudentID",
                        Value = int.Parse(model.UserID.ToString())
                    };
                    model.GPAUserDetails = ctx.Database.SqlQuery<GetUserDetails_Result>("exec GetUserDetails @StudentId ", idParam).ToList<GetUserDetails_Result>();
                    ViewBag.UserID = model.UserID;
                    ViewBag.Reporttype = "Student Details";
                    ViewBag.ReportName = "StudentDetails.rdlc";
                    return View(model);
                }
            }
             else if (command == "Student Course Enrollment")
            {
                using (var ctx = new GPAEntities())
                {
                    model.GPAStudentCourse = ctx.Database.SqlQuery<StudentCourse_Result>("exec StudentCourse").ToList<StudentCourse_Result>();
                    ViewBag.UserID = model.UserID;
                    ViewBag.Reporttype = "Student Course Enrollment";
                    ViewBag.ReportName = "StudentCourse.rdlc";
                    return View(model);
                }
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Report(string id, int userId, string reportName)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Content/Reports/"), reportName);
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }

            ReportDataSource reportDataSource;
            string deviceInfo = "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            if (reportName == "StudentGrade.rdlc")
            {
                List<FindGPA_Result> gradeList = new List<FindGPA_Result>();

                using (var ctx = new GPAEntities())
                {
                    var idParam = new SqlParameter
                    {
                        ParameterName = "StudentID",
                        Value = userId
                    };
                    gradeList = ctx.Database.SqlQuery<FindGPA_Result>("exec FindGPA @StudentId ", idParam).ToList<FindGPA_Result>();
                }

                reportDataSource = new ReportDataSource("StudentGrade", gradeList);
                lr.DataSources.Add(reportDataSource);

            }
            else if (reportName == "StudentDetails.rdlc")
            {
                List<GetUserDetails_Result> userDetails = new List<GetUserDetails_Result>();
                using (var ctx = new GPAEntities())
                {
                    var idParam = new SqlParameter
                    {
                        ParameterName = "StudentID",
                        Value = userId
                    };
                    userDetails = ctx.Database.SqlQuery<GetUserDetails_Result>("exec GetUserDetails @StudentId ", idParam).ToList<GetUserDetails_Result>();
                }
                reportDataSource = new ReportDataSource("StudentDetails", userDetails);
                lr.DataSources.Add(reportDataSource);
                deviceInfo = "<DeviceInfo>" +
           "  <OutputFormat>" + id + "</OutputFormat>" +
           "  <PageWidth>11in</PageWidth>" +
           "  <PageHeight>11in</PageHeight>" +
           "  <MarginTop>0.5in</MarginTop>" +
           "  <MarginLeft>1in</MarginLeft>" +
           "  <MarginRight>1in</MarginRight>" +
           "  <MarginBottom>0.5in</MarginBottom>" +
           "</DeviceInfo>";
            }
            else if (reportName == "StudentCourse.rdlc")
                {
                    List<StudentCourse_Result> studentcourse = new List<StudentCourse_Result>();

                    using (var ctx = new GPAEntities())
                    {
                        studentcourse = ctx.Database.SqlQuery<StudentCourse_Result>("exec StudentCourse").ToList<StudentCourse_Result>();
                    }

                    reportDataSource = new ReportDataSource("dsStudentCourse", studentcourse);
                    lr.DataSources.Add(reportDataSource);

                }
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }

    }
}