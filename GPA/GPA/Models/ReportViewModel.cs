using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


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
    }
}