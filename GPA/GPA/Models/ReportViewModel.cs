using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/*
 * Project Name: GPA  
 * Date Started: 01/03/2014
 * Description: Handles Course business logic
 * Module Name: Report Module 
 * Developer Name: Mehrdad Panahandeh
 * Version: 0.1
 * Date Modified:
 */

namespace GPA.Models
{
    public class ReportViewModel
    {
        [Display(Name="UserID")]
        public int UserID { get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }
       
        public ReportViewModel()
        {
            UserList = new List<SelectListItem>();
        }

        public List<FindGPA_Result> GPAesult { get; set; }
        public List<GetUserDetails_Result> GPAUserDetails { get; set; }
        public List<StudentCourse_Result> GPAStudentCourse { get; set; }
    }
}