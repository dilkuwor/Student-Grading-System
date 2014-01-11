﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPA.Models;

/*
 * Project Name: GPA  
 * Date Started: 01/03/2014
 * Description: Handles Staff business logic
 * Module Name: Search Module
 * Developer Name: Kengsreng Tang
 * Version: 0.1
 * Date Modified:
 */

namespace GPA.DAL.Extended
{
    public class StaffSearh : ISearch<string, IQueryable<UserDetail>>
    {
        private GPAEntities db = new GPAEntities();
        public IQueryable<UserDetail> FindByName(string search)
        {
            var staffs = (from u in db.UserDetails
                          join ur in db.UserRoles on u.RegistrationID equals ur.UserRefID
                          join role in db.Roles on ur.RoleRef_ID equals role.Role_ID
                          where role.RoleName == "Staff"
                          select u);
            if (!String.IsNullOrEmpty(search))
            {
                staffs = staffs.Where(s => s.FName.ToUpper().Contains(search.ToUpper())
                    || s.LName.ToUpper().Contains(search.ToUpper()));
            }
            return staffs;
        }
    }
}