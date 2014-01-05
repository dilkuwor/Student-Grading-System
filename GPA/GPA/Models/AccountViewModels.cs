using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GPA.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
       // [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

   

    /// <summary>
    /// Binds multiple view model
    /// </summary>
    public class RegisterViewModel
    {
        public UserViewModel UserViewModel { get; set; }
        public RegisterUserViewModel RegisterUserViewModel { get; set; }
       // public RoleViewModel RoleViewModel { get; set; }

    }
    public class AuthenticateUser
    {
        public bool IsUserAuthenticate { get; set; }
    }

    public class UserViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

       
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class UserVerificationViewModel
    {
        [Required(ErrorMessage="Verification code is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [Display(Name = "Enter Verification Code")]
        public string VerificationCode { get; set; }
    }

    public class RegisterUserViewModel
    {
        

        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LName { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
         
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "Zip")]
        public string Zip { get; set; }


        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Display(Name = "Landline Number")]
        public string LandNumber { get; set; }

    }

    public class RoleViewModel
    {

        public RoleViewModel()
        {
            RoleList = new  List<SelectListItem>();
        }
         [Display(Name = "Role name")]
        public int RoleID { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }       
        public string Role { get; set; }

    }
}
