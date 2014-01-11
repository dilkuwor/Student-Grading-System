using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPA.Models;

namespace GPA.DAL.Extended
{
    public class StaffSearh : ISearch<string, IQueryable<UserDetail>>
    {
        private GPAEntities db = new GPAEntities();
        public IQueryable<UserDetail> FindByName(string search)
        {
            var staffs = (from u in db.Users
                          join ud in db.UserDetails on u.UserID equals ud.UserID
                          where u.Role == "Staff"
                          select ud);
            if (!String.IsNullOrEmpty(search))
            {
                staffs = staffs.Where(s => s.FName.ToUpper().Contains(search.ToUpper())
                    || s.LName.ToUpper().Contains(search.ToUpper()));
            }
            return staffs;
        }
    }
}