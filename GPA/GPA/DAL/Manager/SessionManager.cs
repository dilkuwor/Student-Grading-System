using GPA.Models;
using GPA.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPA.DAL.Manager
{
    public class SessionManager : Controller
    {


        public void CreateUserSession(int userId)
        {
           
            //using (var db = new TestDBEntities())
            //{
            //    currentUser = db.Users.Where(r => r.Id == userId).Single();
            //}
            AccountManager am = new AccountManager();
            
           
        }


    }
}