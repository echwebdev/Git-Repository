using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NNAuthorizationServer.API.Models
{
    public class CreateRoleBindingModel
    {
        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
    }

    public class UpdateRoleBindingModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
    }


    public class DeleteRoleBindingModel
    {
        [Required]
        public string Id { get; set; }
    }

    public class GetRoleBindingModel
    {
        [Required]
        public string Id { get; set; }
    }

    public class UsersInRoleModel
    {
        public string Id { get; set; }
        public List<string> EnrolledUsers { get; set; }
        public List<string> RemovedUsers { get; set; }
    }

    public class RoleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}