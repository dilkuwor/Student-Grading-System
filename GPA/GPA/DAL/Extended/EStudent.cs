using GPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPA.DAL.Extended
{
    public class EStudent
    {
        public int GradeID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public double ExtraCredit { get; set; }
        public EStudent()
        {

        }
    }
}