using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPA.Models;

namespace GPA.DAL.Extended
{
    public class StuffSearh : ISearch<string, IQueryable<User>>
    {
        private GPAEntities db = new GPAEntities();
        public IQueryable<User> FindByName(string search)
        {
            var staffs = db.Users.Where(s => s.Role == "Staff");
            if (!String.IsNullOrEmpty(search))
            {
                staffs = staffs.Where(s => s.UserName.ToUpper().Contains(search.ToUpper()) && s.Role == "Staff");
            }
            return staffs;
        }
    }
}