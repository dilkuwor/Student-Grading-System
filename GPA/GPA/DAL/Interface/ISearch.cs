using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
 * Project Name: GPA  
 * Date Started: 01/03/2014
 * Description: Handles Student business logic
 * Module Name: Search Module
 * Developer Name: Kengsreng Tang
 * Version: 0.1
 * Date Modified:
 */

namespace GPA.DAL.Extended
{
    interface ISearch<T,R>
    {
       R FindByName(T input);
    }
}