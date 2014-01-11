using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPA.DAL;
using GPA.Models;

/*
 * Project Name: GPA  
 * Date Started: 01/03/2014
 * Description: Handles Faculty business logic
 * Module Name: Search Module
 * Developer Name: Kengsreng Tang
 * Version: 0.1
 * Date Modified:
 */

namespace GPA.DAL.Extended
{
    public class FacultySearch : ISearch<string, IQueryable<UserDetail>>
    {
        private GPAEntities db = new GPAEntities();
        public IQueryable<UserDetail> FindByName(string search)
        {
            var faculties = (from u in db.Users
                             join ud in db.UserDetails on u.UserID equals ud.UserID
                             where u.Role == "Faculty"
                             select ud);
            if (!String.IsNullOrEmpty(search))
            {
                faculties = faculties.Where(s => s.FName.ToUpper().Contains(search.ToUpper())
                  || s.LName.ToUpper().Contains(search.ToUpper()));
            }
            return faculties;
        }
    }
}