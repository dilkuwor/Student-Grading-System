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
                var gradeList = ctx.Database.SqlQuery<FindGPA1_Result>("exec FindGPA @StudentId ", idParam).ToList<FindGPA1_Result>();
                model.GPAesult = gradeList;
                return View(model);

            }

            ////var 
            //using (GPAEntities dc = new GPAEntities())
            //{
            //    var v = dc.Users.ToList();
            //    return View(v);
            //}

        }

        public ActionResult Report(string id, string userId)
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

            //using (GPAEntities dc = new GPAEntities())
            //{
            //    cm = dc.Users.ToList();
            //}

            List<GPA.Models.User> gradeList = new List<GPA.Models.User>();
            using (var ctx = new GPAEntities())
            {
                var idParam = new SqlParameter
                {
                    ParameterName = "StudentID",
                    Value = int.Parse(userId)
                };
                gradeList = ctx.Database.SqlQuery<User>("exec FindGPA @StudentId ", idParam).ToList<User>();
            }


            SqlConnection sqlcon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["GPAConnectionString"].ConnectionString);

            SqlCommand com = new SqlCommand();
            com.Connection = sqlcon;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "FindGPA";

            SqlParameter parameter = new SqlParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.DbType = DbType.Int32;
            parameter.Value = int.Parse(userId);
            com.Parameters.Add(parameter);

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(ds);

            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", cm);
            reportDataSource.Value = da;

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