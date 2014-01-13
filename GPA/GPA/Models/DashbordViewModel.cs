using GPA.DAL.Extended;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
/*
 * Project Name: GPA  
 * Date Started: 01/08/2014
 * Description: Integration of the different modules based on the roles.
 * Module Name: Authoerization
 * Module Number: 008-100-101
 * Developer Name: Dil/Laxman
 * Version: 0.1
 * Date Modified:
 * 
 */

namespace GPA.Models
{
    public class DashbordViewModel
    {
        public StudentViewModel StudentViewModel { get; set; }
        public AdminViewModel AdminViewModel { get; set; }
        public GradeEnterFormViewModel GradeEnterFormViewModel { get; set; }
        public FeedbackViewModel FeedbackViewModel { get; set; }
        public FacultyViewModel FacultyViewModel { get; set; }
    }


    public class AdminViewModel
    {
        public List<CourseUserRequest> RequestedCourses { get; set; }

        public ApplicationSettingViewModel ApplicationSettingViewModel { get; set; }
    }

    public class FacultyViewModel
    {

    }

    public class GradeEnterFormViewModel
    {
       
        public List<UserDetail> Students { get; set; }
        [Required(ErrorMessage="This is required")]
        public List<SelectListItem> Grades { get; set; }

        [Display(Name="Course")]
        public int CourseID { get; set; }
        public int GradeID { get; set; }

        public GradeEnterFormViewModel()
        {
            CourseList = new List<SelectListItem>();
        }
       
        public IEnumerable<SelectListItem> CourseList { get; set; }   
    }


}