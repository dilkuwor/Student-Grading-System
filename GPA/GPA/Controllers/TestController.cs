using GPA.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPA.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoadData()
        {
            AccountManager accountManager = new AccountManager();
            accountManager.InsertTestData();
            return RedirectToAction("Index", "Home");
        }

	}
}