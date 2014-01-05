using GPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPA.DAL.Util
{
    public class GPADBConnection
    {


        public static GPAEntities DBConnection;
        public static GPAEntities DataContext()
        {
            if (DBConnection != null)
                return DBConnection;
            else
            {
                DBConnection = new GPAEntities();
                return DBConnection;
            }
        }


    }
}