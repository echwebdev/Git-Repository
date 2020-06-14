using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DataModels.Services.Auth;
using DataModels.Services.Roles;

namespace NNResourceServer.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/roles")]
    //[EnableCors("*", "*", "*")]
    public class RolesController : BaseApiController
    {
        private RolesService _rolesSvc;

        public RolesController()
        {
            _rolesSvc = new RolesService();
        }

        private void Init()
        {
            BaseInit();
        }

        [HttpPost]
        [Route("GetRole")]
        [ResponseType(typeof(GetRoleOutput))]
        [Extensions.Auth]
        public IHttpActionResult GetRole(GetRoleInput input)
        {
            Init();
            input.AccessToken = ApiSession.AccessToken;
            GetRoleOutput output = _rolesSvc.GetRole(input);
            return Ok(output);
        }

        [HttpPost]
        [Route("GetAllRoles")]
        [ResponseType(typeof(GetAllRolesOutput))]
        [Extensions.Auth]
        public IHttpActionResult GetAllRoles(GetAllRolesInput input)
        {
            Init();
            input.AccessToken = ApiSession.AccessToken;
            GetAllRolesOutput output = _rolesSvc.GetAllRoles(input);
            return Ok(output);
        }

        [HttpPost]
        [Route("CreateRole")]
        [ResponseType(typeof(CreateRoleOutput))]
        [Extensions.Auth]
        public IHttpActionResult CreateRole(CreateRoleInput input)
        {
            Init();
            input.AccessToken = ApiSession.AccessToken;
            CreateRoleOutput output = _rolesSvc.CreateRole(input);
            return Ok(output);
        }

        [HttpPost]
        [Route("UpdateRole")]
        [ResponseType(typeof(UpdateRoleOutput))]
        [Extensions.Auth]
        public IHttpActionResult UpdateRole(UpdateRoleInput input)
        {
            Init();
            input.AccessToken = ApiSession.AccessToken;
            UpdateRoleOutput output = _rolesSvc.UpdateRole(input);
            return Ok(output);
        }

        [HttpPost]
        [Route("DeleteRole")]
        [ResponseType(typeof(DeleteRoleOutput))]
        [Extensions.Auth]
        public IHttpActionResult DeleteRole(DeleteRoleInput input)
        {
            Init();
            input.AccessToken = ApiSession.AccessToken;
            DeleteRoleOutput output = _rolesSvc.DeleteRole(input);
            return Ok(output);
        }

        [HttpPost]
        [Route("UsersInRole")]
        [ResponseType(typeof(ManageUsersInRoleOutput))]
        [Extensions.Auth]
        public IHttpActionResult ManageUsersInRole(ManageUsersInRoleInput input)
        {
            Init();
            input.AccessToken = ApiSession.AccessToken;
            ManageUsersInRoleOutput output = _rolesSvc.ManageUsersInRole(input);
            return Ok(output);
        }
    }
}