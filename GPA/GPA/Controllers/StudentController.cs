using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GPA.Models;
using GPA.DAL.Extended;

/*
 * Project Name: GPA  
 * Date Started: 01/03/2014
 * Description: Handles Student business logic
 * Module Name: Search Module
 * Developer Name: Kengsreng Tang
 * Version: 0.1
 * Date Modified:
 */

namespace GPA.Controllers
{
    public class StudentController : Controller
    {
        private GPAEntities db = new GPAEntities();
        private ISearch<string, IQueryable<UserDetail>> studentSearch = new StudentSearch();
        // GET: /Student/
        public ActionResult Index(string searchString)
        {
            return View(studentSearch.FindByName(searchString));
        }
            
        // GET: /Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail user = (from u in db.UserDetails
                               where u.UserID == id
                               select u).SingleOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

      

        
        // GET: /Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail user = (from u in db.UserDetails
                              where u.UserID == id
                              select u).SingleOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
           
            return View(user);
        }

        // POST: /Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]        
        public ActionResult Edit(UserDetail user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }           
            return View(user);
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
