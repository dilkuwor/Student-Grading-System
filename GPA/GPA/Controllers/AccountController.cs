using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using GPA.Models;
using GPA.Models.Manager;
using System.Collections.Generic;
using GPA.DAL.Manager;
using GPA.DAL.Util;

/*
 * Project Name: GPA  
 * Date Started: 01/01/2014
 * Description: Handles user login and registration module
 * Module Name: User Administration Module
 * Developer Name: Dil Kuwor, Laxaman Adhikari
 * Version: 0.1
 * Date Modified:
 * 
 */

namespace GPA.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            AccountManager amanager = new AccountManager();
            User user;
            if (amanager.ValidateUser(model, out user))
            {
                //Session["userID"] = user.UserID;
                TempData["currentUserID"] = user.UserID;
                AuthenticateUser au = new AuthenticateUser();

                if (amanager.IsUserRegistered(user))
                {
                    return RedirectToAction("LoginVerification", "Account");
                }
                else

                    return RedirectToAction("Register", "Account");
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginVerification(UserVerificationViewModel model)
        {
            int userId = (int)TempData["currentUserID"];

            AccountManager am = new AccountManager();
            if (am.GetUserVerificationCode(userId) == model.VerificationCode)
            {
                String returnUrl = null;             
                CreateSession(userId);
                return RedirectToLocal(returnUrl);
            }
            
            ModelState.AddModelError("", "The verification code is incorrect");
            return View(model);

           
        }

        public void CreateSession(int userId)
        {
            User currentUser = null;
            using (var db = new GPAEntities())
            {
                currentUser = db.Users.Where(r => r.UserID == userId).Single();
            }
            AccountManager am = new AccountManager();
            Session["UserExist"] = "True";
            Session["User"] = currentUser.UserName;
            Session["CurrentUser"] = currentUser;
           
        }



        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult LoginVerification(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }



        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            
            RegisterViewModel registerViewModel = new RegisterViewModel();
            RoleViewModel model = new RoleViewModel();         
            return View(registerViewModel);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            Helper helper = new Helper();
           
            if (ModelState.IsValid)
            {
                
                AccountManager accountManager = new AccountManager();
                accountManager.RegisterUser(model, (int)TempData["currentUserID"]);

            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Login", "Account");
        }


        //
        // POST: /Account/LogOff

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //WebSecurity.Logout();
            
            ClearSession();
            return RedirectToAction("Index", "Home");
        }

        public void ClearSession()
        {
            Session["UserExist"] = null;
            Session["User"] = null;
            Session["CurrentUser"] = null;
        }


        [AllowAnonymous]
        public ActionResult ApplicationSetting()
        {
            ApplicationSettingViewModel asvm = new ApplicationSettingViewModel();
            return View(asvm);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ApplicationSetting(ApplicationSettingViewModel asvm)
        {
            Properties.Settings.Default.Save();

            return View(asvm);
        }
    }
}