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
    
    public partial class User
    {
        public User()
        {
            this.UserDetails = new HashSet<UserDetail>();
            this.UserRoles = new HashSet<UserRole>();
        }
    
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VerificationCode { get; set; }
        public string Role { get; set; }
    
        public virtual ICollection<UserDetail> UserDetails { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
