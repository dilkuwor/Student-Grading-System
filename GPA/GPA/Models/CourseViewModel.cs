using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace GPA.Models
{
    public class CourseViewModel
    {

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "SubCode")]
        public string SubCode { get; set; }

        [Display(Name = "Level")]
        public string Level { get; set; }

        [Display(Name = "CourseName")]
        public string CourseName { get; set; }

        [Display(Name = "Credit")]
        public int Credit { get; set; }

        public List<Cours> Courses { get; set; }

    }
}