using GPA.DAL.Manager;
using GPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPA.Controllers
{
    public class FeedbackController : Controller
    {
       
        //
        // GET: /FeedBack/
        public ActionResult Index()
        {
          
            FeedbackViewModel model = new FeedbackViewModel();
            FeedbackManager feedManager = new FeedbackManager();
            model.FeedbackReadViewModel = new FeedbackReadViewModel();
            model.FeedbackReadViewModel.Feedbacks = feedManager.GetFeedbacks((User)Session["CurrentUser"]);

            FeedbackSendViewModel sendmodel = new FeedbackSendViewModel();

            User currentUser = (User)Session["currentUser"];

            sendmodel.UserList = from ruser in feedManager.GetRegisterUser(currentUser)
                                select new SelectListItem
                                {
                                    Text = ruser.FName +" "+ruser.LName,
                                    Value = ((int)ruser.RegistrationID).ToString()
                                };

            model.FeedbackSendViewModel = sendmodel;
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SendFeedback(FeedbackViewModel model)
        {

            Feedback feedback = new Feedback();
           
           
            feedback.Date = DateTime.Now.ToString("yyyy-MM-dd");
            feedback.Comment = model.FeedbackSendViewModel.Message;
            feedback.Subject = model.FeedbackSendViewModel.Subject;
            User currentUser = (User)Session["currentUser"];
            FeedbackManager fmanager = new FeedbackManager();
            Registration current = fmanager.FindUserByUserID(currentUser.UserID);
            feedback.FromID = current.RegistrationID;
            feedback.ToID = model.FeedbackSendViewModel.ToID;

            fmanager.SendFeedback(feedback);
            TempData["MessageSent"] = "True";
            return RedirectToAction("Index");
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFeedback(FeedbackViewModel model)
        {

            return RedirectToAction("Index");
        }

        
	}
}