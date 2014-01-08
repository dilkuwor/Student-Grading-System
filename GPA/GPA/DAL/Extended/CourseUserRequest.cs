using GPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPA.DAL.Extended
{
    public class CourseUserRequest : Cours
    {
        public int RequestID { get; set; }
        public string StudentName { get; set; }
        public CourseUserRequest()
        {

        }
    }
}