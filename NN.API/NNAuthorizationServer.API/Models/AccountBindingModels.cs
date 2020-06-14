using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NNAuthorizationServer.API.Models
{
    public class AccountBindingModels
    {
        public class CreateUserBindingModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Role Name")]
            public string RoleName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public class ChangePasswordBindingModel
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

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmNewPassword { get; set; }

        }

        public class ForgotPasswordBindingModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public class ResetPasswordBindingModel
        {
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmNewPassword { get; set; }
            public string resetCode { get; set; }
        }

        public class GetUserBindingModel
        {
            public string Id { get; set; }
            public string Username { get; set; }
        }

        public class UpdateUserBindingModel
        {
            public string Id { get; set; }
            public string Username { get; set; }
            public string AccessToken { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class DeleteUserBindingModel
        {
            public string Id { get; set; }
        }
    }
}