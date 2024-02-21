using AirlineTickets.Model.Dto;
using MayNghien.Models.Request.Base;
using MayNghien.Models.Response.Base;
using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.Service.Contract
{
    public interface IUserService
    {
        public Task<AppResponse<string>> ResetPassWordUser(string Id);
        public Task<AppResponse<string>> Password(UserModel user);
        public Task<AppResponse<string>> CreateUser(UserModel model);
        public Task<AppResponse<string>> DeleteUser(string id);
        public Task<AppResponse<string>> EditUser(UserModel model);
        public Task<AppResponse<SearchResponse<UserModel>>> Search(SearchRequest request);

        public Task<AppResponse<List<UserModel>>> GetAllUser();
        public Task<AppResponse<UserModel>> GetUser(string email);
        public Task<AppResponse<IdentityUser>> GetUserIdentity(string Id);
    }
}
