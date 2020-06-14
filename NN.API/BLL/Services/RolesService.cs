using BLL.Common;
using DataModels.Core;
using DataModels.Services.Auth;
using DataModels.Services.Roles;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace BLL.Services
{
    public class RolesService : BaseService, Interfaces.IRolesService
    {
        private AccessToken _CT;

        public RolesService()
        {
            _CT = new AccessToken();
        }

        //Public Methods
        public GetAllRolesOutput GetAllRoles(GetAllRolesInput input)
        {
            try
            {
                RestHTTP http = new RestHTTP();
                RestRequest req = new RestRequest("api/roles", RestSharp.Method.POST);
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("Authorization", "Bearer " + input.AccessToken);
                req.AddJsonBody(input);

                RestSharp.RestClient client = new RestSharp.RestClient(WebConfigurationManager.AppSettings["AuthServerAPI"]);
                var response = client.Execute<List<RoleModel>>(req);
                List<RoleModel> result = JsonConvert.DeserializeObject<List<RoleModel>>(response.Content);

                GetAllRolesOutput output = new GetAllRolesOutput();
                output.Roles = result;

                return output;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("GetAllRoles : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
            }
            return null;
        }

        public GetRoleOutput GetRole(GetRoleInput input)
        {
            try
            {
                RestHTTP http = new RestHTTP();
                RestRequest req = new RestRequest("api/roles/getRole", RestSharp.Method.POST);
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("Authorization", "Bearer " + input.AccessToken);
                req.AddJsonBody(input);
                GetRoleOutput response = http.HttpPost<GetRoleOutput>(req);

                return response;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("GetRole : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
            }
            return null;
        }

        public CreateRoleOutput CreateRole(CreateRoleInput input)
        {
            try
            {
                RestHTTP http = new RestHTTP();
                RestRequest req = new RestRequest("api/roles/create", RestSharp.Method.POST);
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("Authorization", "Bearer " + input.AccessToken);
                req.AddJsonBody(input);
                CreateRoleOutput response = http.HttpPost<CreateRoleOutput>(req);

                return response;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("CreateRole : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
            }
            return null;
        }

        public UpdateRoleOutput UpdateRole(UpdateRoleInput input)
        {
            try
            {
                RestHTTP http = new RestHTTP();
                RestRequest req = new RestRequest("api/roles/update", RestSharp.Method.POST);
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("Authorization", "Bearer " + input.AccessToken);
                req.AddJsonBody(input);
                UpdateRoleOutput response = http.HttpPost<UpdateRoleOutput>(req);

                return response;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("UpdateRole : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
            }
            return null;
        }

        public DeleteRoleOutput DeleteRole(DeleteRoleInput input)
        {
            try
            {
                RestHTTP http = new RestHTTP();
                RestRequest req = new RestRequest("api/roles/delete", RestSharp.Method.DELETE);
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("Authorization", "Bearer " + input.AccessToken);
                req.AddJsonBody(input);
                DeleteRoleOutput response = http.HttpPost<DeleteRoleOutput>(req);

                return response;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("DeleteRole : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
            }
            return null;
        }

        public ManageUsersInRoleOutput ManageUsersInRole(ManageUsersInRoleInput input)
        {
            try
            {
                RestHTTP http = new RestHTTP();
                RestRequest req = new RestRequest("api/roles/manageUsersInRole", RestSharp.Method.POST);
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("Authorization", "Bearer " + input.AccessToken);
                req.AddJsonBody(input);
                ManageUsersInRoleOutput response = http.HttpPost<ManageUsersInRoleOutput>(req);

                return response;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("ManageUsersInRole : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
            }
            return null;
        }
    }
}
