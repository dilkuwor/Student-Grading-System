﻿using GPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPA.DAL.Extended
{
    public class ECourse: Cours
    {
        public bool IsRequested { get; set; }
    }
}