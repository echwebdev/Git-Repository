using DataModels.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Services.Roles
{
    public class GetAllRolesInput
    {
        public string AccessToken { get; set; }
    }

    public class GetAllRolesOutput : BaseResponseModel
    {
        public GetAllRolesOutput()
        {
            Roles = new List<RoleModel>();
        }
        public List<RoleModel> Roles { get; set; }
    }

    public class GetRoleInput
    {
        public string Id { get; set; }
        public string AccessToken { get; set; }
    }

    public class GetRoleOutput : BaseResponseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateRoleInput
    {
        public string Name { get; set; }
        public string AccessToken { get; set; }
    }

    public class CreateRoleOutput : BaseResponseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateRoleInput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AccessToken { get; set; }
    }

    public class UpdateRoleOutput : BaseResponseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }


    public class DeleteRoleInput
    {
        public string Id { get; set; }
        public string AccessToken { get; set; }
    }

    public class DeleteRoleOutput : BaseResponseModel
    {
    }

    public class ManageUsersInRoleInput
    {
        public string Id { get; set; }
        public List<string> EnrolledUsers { get; set; }
        public List<string> RemovedUsers { get; set; }
        public string AccessToken { get; set; }
    }

    public class ManageUsersInRoleOutput : BaseResponseModel
    {
        public string Id { get; set; }
    }

    public class RoleModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
    }
}
