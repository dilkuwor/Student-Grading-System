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
    
    public partial class Feedback
    {
        public int FeedbackID { get; set; }
        public string Comment { get; set; }
        public int FromID { get; set; }
        public int ToID { get; set; }
        public string Subject { get; set; }
    
        public virtual Registration Registration { get; set; }
        public virtual Registration Registration1 { get; set; }
    }
}
