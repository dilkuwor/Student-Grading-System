using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPA.DAL;
using GPA.Models;

namespace GPA.DAL.Extended
{
    public class FacultySearch : ISearch<string, IQueryable<User>>
    {
        private GPAEntities db = new GPAEntities();
        public IQueryable<User> FindByName(string search)
        {
            var faculties = db.Users.Where(s => s.Role == "Faculty");
            if (!String.IsNullOrEmpty(search))
            {
                faculties = faculties.Where(s => s.UserName.ToUpper().Contains(search.ToUpper()) && s.Role == "Faculty");
            }
            return faculties;
        }
    }
}