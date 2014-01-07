using GPA.DAL.Extended;
using GPA.DAL.Manager;
using GPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            sendmodel.UserList = GetUserList(currentUser);
            
            //hides and show the detail message panel
            if (TempData["Detail"] == "True")
            {
                model.FeedBackMood = "Detail";
                model.FeedbackReadViewModel.FeedbackDetail = (UserFeedback)TempData["DetailFeedback"];
                
            }
            else if (TempData["Reply"] == "True")
            {
                UserFeedback feed = (UserFeedback)TempData["DetailFeedback"];
                FeedbackSendViewModel sendDafault = new FeedbackSendViewModel();
                sendmodel.Message = feed.Comment;
                sendmodel.Subject = feed.Subject;
                sendmodel.ToID = feed.FromID;               
                model.FeedBackMood = "Reply";
            }

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
            UserDetail current = fmanager.FindUserByUserID(currentUser.UserID);
            feedback.FromID = current.RegistrationID;
            feedback.ToID = model.FeedbackSendViewModel.ToID;
            fmanager.SendFeedback(feedback);
            TempData["MessageSent"] = "True";

            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("Index");
            }
            var httpStatus = HttpStatusCode.OK;
            return new HttpStatusCodeResult(httpStatus);

           // return RedirectToAction("Index");
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SendFeedbackReply(FeedbackViewModel model)
        {
            FormCollection col = new FormCollection();
            var value = col[0];
            return RedirectToAction("Index");

        }

        public ActionResult DeleteFeedback(int id)
        {
            TempData["MessageDeleted"] = "True";
            TempData["MessageSent"] = "False";
            FeedbackManager fmanager = new FeedbackManager();
            fmanager.DeleteFeedback(id);
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("Index");
            }
            var httpStatus = HttpStatusCode.OK;
            return new HttpStatusCodeResult(httpStatus);
            //return RedirectToAction("Index");
        }

        public ActionResult FeedbackDetails(int id)
        {
            TempData["MessageDeleted"] = "False";
            TempData["Detail"] = "True";
            FeedbackManager fmanager = new FeedbackManager();
            User currentUser = (User)Session["currentUser"];
            UserFeedback ufeedback = fmanager.GetFeedbacks(currentUser).Where(r => r.FeedbackID == id).Single();
            TempData["DetailFeedback"] = ufeedback;
            return RedirectToAction("Index");
        }




        public ActionResult FeedbackReply(int id)
        {
            FeedbackManager fmanager = new FeedbackManager();
            TempData["Reply"] = "True";
            User currentUser = (User)Session["currentUser"];
            UserFeedback ufeedback = fmanager.GetFeedbacks(currentUser).Where(r => r.FeedbackID == id).Single();
            TempData["DetailFeedback"] = fmanager.PrepareReplyMessage(ufeedback);
            return RedirectToAction("Index");
        }


        public IEnumerable<SelectListItem> GetUserList(User currentUser)
        {
            FeedbackManager feedManager = new FeedbackManager();
            return from ruser in feedManager.GetRegisterUser(currentUser)
                                       select new SelectListItem
                                       {
                                           Text = ruser.FName + " " + ruser.LName,
                                           Value = ((int)ruser.RegistrationID).ToString()
                                       };

        }



    }
}