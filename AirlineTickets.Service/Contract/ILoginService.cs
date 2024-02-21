using AirlineTickets.Model.Dto;
using MayNghien.Models.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.Service.Contract
{
    public interface ILoginService
    {
        Task<AppResponse<string>> AuthenticateUser(UserModel user);
        Task<AppResponse<string>> CreateUser(UserModel user);
    }
}
