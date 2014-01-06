using GPA.DAL.Extended;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPA.Models
{
    public class FeedbackViewModel
    {
        public FeedbackReadViewModel FeedbackReadViewModel { get; set; }
        public FeedbackSendViewModel FeedbackSendViewModel { get; set; }
        //FeedBackMood: Read,Send,Reply
        public string FeedBackMood { get; set; }
        
    }

    public class FeedbackSendViewModel
    {
        
        public string Message { get; set; }
        public string Subject { get; set; }
        public int MyProperty { get; set; }

        public FeedbackSendViewModel()
        {
            UserList = new List<SelectListItem>();
        }
        [Display(Name = "To")]
        public int ToID { get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }       
    }

    public class FeedbackReadViewModel
    {
        [Display(Name = "From")]
        public int FromId { get; set; }

        [Display(Name = "Feedback")]
        public string Message { get; set; }
        [Display(Name = "To")]
        public List<Registration> UserList { get; set; }
        public int ToId { get; set; }

        public string Subject { get; set; }

        public int FeedbackID { get; set; }
        public UserFeedback FeedbackDetail { get; set; }
        public List<UserFeedback> Feedbacks { get; set; }
    }


}


