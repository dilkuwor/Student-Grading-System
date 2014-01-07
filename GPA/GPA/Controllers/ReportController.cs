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
            model.UserList = from ruser in accountmanager.GetUserList()
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
        public ActionResult GradeReport(ReportViewModel model)
        {
            ReportViewModel model1 = new ReportViewModel();

            using (var ctx = new GPAEntities())
            {
                var idParam = new SqlParameter
                {
                    ParameterName = "StudentID",
                    Value = int.Parse(model.UserID.ToString())
                };
                var gradeList = ctx.Database.SqlQuery<FindGPA_Result>("exec FindGPA @StudentId ", idParam).ToList<FindGPA_Result>();
                model.GPAesult = gradeList;
                ViewBag.UserID = model.UserID;
                return View(model);

            }
        }

        public ActionResult Report(string id, string userId)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Content"), "StudentGrade.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }

            List<FindGPA_Result> gradeList = new List<FindGPA_Result>();
            using (var ctx = new GPAEntities())
            {
                var idParam = new SqlParameter
                {
                    ParameterName = "StudentID",
                    Value = int.Parse(userId)
                };
                gradeList = ctx.Database.SqlQuery<FindGPA_Result>("exec FindGPA @StudentId ", idParam).ToList<FindGPA_Result>();
            }

            ReportDataSource reportDataSource = new ReportDataSource("StudentGrade", gradeList);
            lr.DataSources.Add(reportDataSource);      
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

       

	}
}