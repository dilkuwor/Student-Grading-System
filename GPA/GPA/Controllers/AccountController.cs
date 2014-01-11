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
 * Module Name: User Administration Module(001)
 * Developer Name: Dil Kuwor, Laxaman Adhikari
 * Version: 0.1
 * Date Modified:01/06/2014 Dil Kuwor
 * Date Modified:01/10/2014 Laxman Adhikari
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
                Session["currentUserID"] = user.UserID;
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
            int userId = (int)Session["currentUserID"];

            AccountManager am = new AccountManager();
            if (am.GetUserVerificationCode(userId) == model.VerificationCode)
            {
                String returnUrl = null;   
                UserDetail udetail = am.FindUserByUserID(userId);
                List<Role> roles =am.GetUsrRoles(udetail.RegistrationID);
                if (roles.Count() > 1)
                {
                    return RedirectToAction("RoleSelectionPartial", "Account");
                }
                else
                {

                    CreateSession(userId, roles[0].Role_ID);
                    return RedirectToLocal(returnUrl);
                }
                
            }
            else
            {
                ModelState.AddModelError("", "The verification code is incorrect");
                return View(model);
            }        
           
                       
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginWithRole(RoleViewModel model)
        {
            String returnUrl = null;
            int userId = (int)Session["currentUserID"];
            CreateSession(userId,model.RoleID);
            return RedirectToLocal(returnUrl);
        }

        public void CreateSession(int userId,int roleid)
        {
            UserDetail currentUser = null;           
            using (var db = new GPAEntities())
            {
                currentUser = db.UserDetails.Where(r => r.UserID == userId).Single();
                
            }
            AccountManager am = new AccountManager();
            
            Session["CurrentUser"] = currentUser;
            Session["Role"] = am.GetRoleByRoleID(roleid);
           
        }



        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult LoginVerification(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult RoleSelectionPartial()
        {
            //UserVerificationViewModel uverification = new UserVerificationViewModel();
            AccountManager amanager = new AccountManager();
            int userId = (int)Session["currentUserID"];
            UserDetail udetail = amanager.FindUserByUserID(userId);
            RoleViewModel rolemodel = new RoleViewModel();
            rolemodel.RoleList = (from r in amanager.GetUsrRoles(udetail.RegistrationID)
                                          select new SelectListItem
                                          {
                                              Text = r.RoleName,
                                              Value = r.Role_ID.ToString()
                                          }).ToList();
            //uverification.RoleViewModel = rolemodel;
            return View(rolemodel);
        }



        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            
            RegisterViewModel registerViewModel = new RegisterViewModel();
            int currentUserId = (int)Session["currentUserID"];
            AccountManager amanager = new AccountManager();
            User user = amanager.GetDefaultUserByID(currentUserId);
            registerViewModel.RegisterUserViewModel = new RegisterUserViewModel();
            registerViewModel.RegisterUserViewModel.Email = user.UserName;
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
                accountManager.RegisterUser(model, (int)Session["currentUserID"]);

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
            return RedirectToAction("Login", "Account");
        }

        public void ClearSession()
        {
            Session.Clear();
            //Session["UserExist"] = null;
            //Session["User"] = null;
            //Session["CurrentUser"] = null;
            //Session["currentUserID"] = null;
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