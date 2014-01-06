using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPA.Controllers
{
    public class ImportDataController : Controller
    {
        //
        // GET: /ImportData/
        public ActionResult ImportFromExternalFile()
        {
            return View();
        }
        public ActionResult Importexcel()
        {
            if (Request.Files["FileUpload1"].ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName);
                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/UploadedFolder"), Request.Files["FileUpload1"].FileName);
                if (System.IO.File.Exists(path1))
                    System.IO.File.Delete(path1);

                Request.Files["FileUpload1"].SaveAs(path1);
                string sqlConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GPAEntities"].ConnectionString.Substring(System.Configuration.ConfigurationManager.ConnectionStrings["GPAEntities"].ConnectionString.IndexOf(';') + 1);

                sqlConnectionString = sqlConnectionString.Substring(sqlConnectionString.IndexOf('"') + 1);
                sqlConnectionString = sqlConnectionString.Substring(0, sqlConnectionString.IndexOf("MultipleActiveResultSets"));


                //Create connection string to Excel work book
                string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=Excel 12.0;Persist Security Info=False";

                //Create Connection to Excel work book
                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                //Create OleDbCommand to fetch data from Excel
                OleDbCommand cmd = new OleDbCommand("Select [UserID],[UserName],[Password],[VerificationCode],[Role] from " +
                    "[Users]", excelConnection);

                excelConnection.Open();
                OleDbDataReader dReader;
                dReader = cmd.ExecuteReader();
                SqlBulkCopy sqlBulk = new SqlBulkCopy(sqlConnectionString);
                //Give your Destination table name
                sqlBulk.DestinationTableName = "Users";
                sqlBulk.WriteToServer(dReader);
                excelConnection.Close();
            }
            return RedirectToAction("UserReport", "Home");
        }
    }
}