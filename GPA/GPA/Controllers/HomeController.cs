using GPA.Models;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using GPA.Models.Manager;
using System;
using System.Linq;
using System.Web;
using GPA.DAL.Manager;



namespace GPA.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DashbordViewModel model = new DashbordViewModel();

            AccountManager amanager = new AccountManager();

            //Test
            UserDetail ruser = amanager.FindUserByUserID(1);
          
            //if the user is registered check his role and display different layout
            StudentViewModel studentModel = new StudentViewModel();
            
            StudentManager smanager = new StudentManager();
            studentModel.Courses = smanager.GetAlreadyTakenCoursesByUserID(ruser.RegistrationID);
            studentModel.ECourses = smanager.GetECourses(ruser.RegistrationID);
            model.StudentViewModel = studentModel;
            CreateSession(1);
            return View(model);
        }


        public ActionResult CourseSignup(int courseid)
        {
            return RedirectToAction("Index", "Home");
        }


        public ActionResult UserReport()
        {
            //var 
            using (GPAEntities dc = new GPAEntities())
            {
                var v = dc.Users.ToList();
                return View(v);
            }


        }

        public ActionResult Report(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Content"), "UserReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<GPA.Models.User> cm = new List<GPA.Models.User>();
            using (GPAEntities dc = new GPAEntities())
            {
                cm = dc.Users.ToList();
            }
            ReportDataSource rd = new ReportDataSource("DataSet1", cm);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";
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


        /// <summary>
        ///     Temp
        /// </summary>
        /// <returns></returns>
        /// 
        public void CreateSession(int userId)
        {
            User currentUser = null;
            using (var db = new GPAEntities())
            {
                currentUser = db.Users.Where(r => r.UserID == userId).Single();
            }
            AccountManager am = new AccountManager();
            Session["UserExist"] = "True";
            Session["User"] = currentUser.UserName;
            Session["CurrentUser"] = currentUser;

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public object List { get; set; }
    }
}