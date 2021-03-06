﻿using GPA.DAL.Extended;
using GPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/*
 * Project Name: GPA  
 * Date Started: 01/01/2014
 * Description: Handles the feedback module
 * Module Name: User Administration Module
 * Developer Name: Dil Kuwor
 * Version: 0.1
 * Date Modified:
 * 
 */
namespace GPA.DAL.Manager
{
    public class FeedbackManager
    {
        public List<UserDetail> GetRegisterUser(UserDetail user)
        {
            List<UserDetail> users = null;
            using (var db = new GPAEntities())
            {

                users = db.UserDetails.Where(r => r.RegistrationID != user.RegistrationID).ToList();
            }
            return users;
        }
        /// <summary>
        /// Regurns the feedbacks of a single user
        /// Feedback Entity is customized to UserFeedback 
        /// User name is added concatnating the FName and the LName in the Registraiont table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<UserFeedback> GetFeedbacks(UserDetail ruser)
        {
          
            List<UserFeedback> users = new List<UserFeedback>();
            using (var db = new GPAEntities())
            {

                //Registration id is the use id for this system
                users = (from f in db.Feedbacks
                         join r in db.UserDetails on f.FromID equals r.RegistrationID
                         where f.ToID == ruser.RegistrationID
                         select new UserFeedback
                         {
                             Comment = f.Comment,
                             From = r.FName + " " + r.LName,
                             ToID = f.ToID,
                             Subject = f.Subject,
                             FromID = f.FromID,
                             Date = f.Date,
                             FeedbackID = f.FeedbackID,
                             ShortMessage = f.Comment.Substring(0,60)+"  ........."

                         }).ToList();
                


            }
            return users;
        }

        public string GetUserNameByID(int userId)
        {
            UserDetail ruser = FindUserByUserID(userId);
            if (ruser == null)
                return "";

            return ruser.FName + " " + ruser.LName;
        }

        /**
         * 01.07.14 Add D.Shrestha
         */
        public UserDetail FindUserByRegID(int regId)
        {
            UserDetail user;
            using (var db = new GPAEntities())
            {
                user = db.UserDetails.Where(r => r.RegistrationID == regId).Single();
            }
            return user;
        }

        public UserDetail FindUserByUserID(int userId)
        {
            UserDetail user;
            using (var db = new GPAEntities())
            {
                user = db.UserDetails.Where(r => r.UserID == userId).Single();
            }
            return user;

        }

        public void SendFeedback(Feedback feedback)
        {
            using (var db = new GPAEntities())
            {
                db.Feedbacks.Add(feedback);
                db.SaveChanges();

            }
        }

        /// <summary>
        /// Deletes feedback by feedback id
        /// </summary>
        /// <param name="feedbackid"></param>
        public void DeleteFeedback(int feedbackid)
        {
            using (var db = new GPAEntities())
            {
                Feedback feedback = db.Feedbacks.Where(r => r.FeedbackID == feedbackid).SingleOrDefault();
                db.Feedbacks.Remove(feedback);
                db.SaveChanges();

            }
        }


        public UserFeedback PrepareReplyMessage(UserFeedback feedback)
        {
            UserFeedback newfeed = feedback;
            newfeed.Subject = "Re: "+feedback.Subject;
            newfeed.Comment = System.Environment.NewLine+feedback.Comment;

            return newfeed;



        }
        
    }
}