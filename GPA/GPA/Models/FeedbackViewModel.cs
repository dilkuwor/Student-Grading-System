using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GPA.Models
{
    public class FeedbackViewModel
    {
        public int FromId { get; set; }

        [Display(Name = "Feedback")]
        public string Message { get; set; }
        [Display(Name = "To")]
        public List<Registration> UserList { get; set; }       
        public int ToId { get; set; }
    }
}