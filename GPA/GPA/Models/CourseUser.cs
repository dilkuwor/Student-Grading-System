//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GPA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CourseUser
    {
        public int Courses_Id { get; set; }
        public int Users_Id { get; set; }
        public int Id { get; set; }
    
        public virtual Cours Cours { get; set; }
        public virtual Registration Registration { get; set; }
    }
}
