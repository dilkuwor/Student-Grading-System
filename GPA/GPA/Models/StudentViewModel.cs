using GPA.DAL.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPA.Models
{
    public class StudentViewModel
    {
        public List<Cours> Courses { get; set; }
        public List<ECourse> ECourses { get; set; }
    }

   
}