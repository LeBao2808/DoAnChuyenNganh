﻿using AirlineTickets.DAL.Contract;
using AirlineTickets.DAL.Models.Context;
using AirlineTickets.Model.Dto;
using AirlineTickets.Service.Contract;
using AutoMapper;
using LinqKit;
using MayNghien.Common.Helpers;
using MayNghien.Models.Request.Base;
using MayNghien.Models.Response.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MayNghien.Common.CommonMessage.AuthResponseMessage;

namespace AirlineTickets.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private AirlineTicketsDBContext _context;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserService(IMapper mapper, AirlineTicketsDBContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
        }

        public async Task<AppResponse<List<UserModel>>> GetAllUser()
        {
            var result = new AppResponse<List<UserModel>>();
            try
            {
                List<Filter> Filters = new List<Filter>();
                var query = BuildFilterExpression(Filters);
                var users = _userRepository.FindByPredicate(query);
                var UserList = users.ToList();
                var dtoList = _mapper.Map<List<UserModel>>(UserList);


                if (dtoList != null && dtoList.Count > 0)
                {
                    for (int i = 0; i < UserList.Count; i++)
                    {
                        var dtouser = dtoList[i];

                        var identityUser = UserList[i];

                        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();

                    }
                }
                return result.BuildResult(dtoList);
            }
            catch (Exception ex)
            {

                return result.BuildError(ex.ToString());
            }

        }
        public async Task<AppResponse<string>> ResetPassWordUser(string Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var user = _userRepository.FindUser(Id.ToString());
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, "dungroi");
                result.IsSuccess = true;
                result.Data = "dungroi";
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
        }
        public async Task<AppResponse<string>> Password(UserModel user)
        {
            var result = new AppResponse<string>();
            try
            {
                var userid = _userRepository.FindById(user.Id.ToString());
                await _userManager.RemovePasswordAsync(userid);
                await _userManager.AddPasswordAsync(userid, user.Password);
                result.IsSuccess = true;
                result.Data = user.Password;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
        }
        public async Task<AppResponse<string>> CreateUser(UserModel user)
        {

            var result = new AppResponse<string>();
            try
            {
                if (string.IsNullOrEmpty(user.Email))
                {
                    return result.BuildError(ERR_MSG_EmailIsNullOrEmpty);
                }
                var identityUser = await _userManager.FindByNameAsync(user.UserName);
                if (identityUser != null)
                {
                    return result.BuildError(ERR_MSG_UserExisted);
                }
                var newIdentityUser = new IdentityUser { Email = user.Email, UserName = user.Email };

                var createResult = await _userManager.CreateAsync(newIdentityUser);
                await _userManager.AddPasswordAsync(newIdentityUser, user.Password);
                if (!(await _roleManager.RoleExistsAsync(user.Role)))
                {
                    IdentityRole role = new IdentityRole { Name = user.Role };
                    await _roleManager.CreateAsync(role);
                }
                await _userManager.AddToRoleAsync(newIdentityUser, user.Role);
                newIdentityUser = await _userManager.FindByEmailAsync(user.Email);
                return result.BuildResult(INFO_MSG_UserCreated);
            }
            catch (Exception ex)
            {

                return result.BuildError(ex.ToString());
            }
        }


        public async Task<AppResponse<string>> DeleteUser(string id)
        {
            var result = new AppResponse<string>();
            try
            {

                IdentityUser identityUser = new IdentityUser();

                identityUser = await _userManager.FindByIdAsync(id);
                if (identityUser != null)
                {
                    if (await _userManager.IsInRoleAsync(identityUser, "tenant"))
                    {

                        var user = _context.Users.FirstOrDefault(x => x.Id == id);
                        _context.Users.Remove(user);
                        //_userManager.DeleteAsync(user);
                    }
                    else
                    {
                        var user = _context.Users.FirstOrDefault(x => x.Id == id);
                        await _userManager.DeleteAsync(user);

                    }

                }
                return result.BuildResult(INFO_MSG_UserDeleted);
            }
            catch (Exception ex)
            {

                return result.BuildError(ex.ToString());
            }
        }

        public async Task<AppResponse<string>> EditUser(UserModel model)
        {
            var result = new AppResponse<string>();
            if (model.Email == null)
            {
                return result.BuildError(ERR_MSG_EmailIsNullOrEmpty);
            }
            try
            {
                var identityUser = await _userManager.FindByIdAsync(model.Email);

                if (identityUser != null)
                {

                    //model.Id = identityUser.Id;
                    model.UserName = identityUser.UserName;
                    model.Email = identityUser.Email;
                    //model.LockoutEnabled  =  identityUser.LockoutEnabled ;


                }
                return result.BuildResult("ok");
            }
            catch (Exception ex)
            {

                return result.BuildError(ex.ToString());
            }
        }

        public async Task<AppResponse<UserModel>> GetUser(string Id)
        {
            var result = new AppResponse<UserModel>();
            try
            {
                List<Filter> Filters = new List<Filter>();
                var query = BuildFilterExpression(Filters);

                //var identityUser = _userRepository.FindById(id);
                var identityUser = _userRepository.FindById(Id);

                if (identityUser == null)
                {
                    return result.BuildError("User not found");
                }
                var dtouser = _mapper.Map<UserModel>(identityUser);

                dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();

                return result.BuildResult(dtouser);
            }
            catch (Exception ex)
            {

                return result.BuildError(ex.ToString());
            }
        }


        // Get identityuser
        public async Task<AppResponse<IdentityUser>> GetUserIdentity(string Id)
        {
            var result = new AppResponse<IdentityUser>();
            try
            {
                List<Filter> Filters = new List<Filter>();
                var query = BuildFilterExpression(Filters);

                //var identityUser = _userRepository.FindById(id);
                var identityUser = _userRepository.FindById(Id);

                if (identityUser == null)
                {
                    return result.BuildError("User not found");
                }
                var dtouser = _mapper.Map<IdentityUser>(identityUser);


                return result.BuildResult(dtouser);
            }
            catch (Exception ex)
            {

                return result.BuildError(ex.ToString());
            }
        }

        public async Task<AppResponse<IdentityUser>> XacThuc(string? Id)
        {
            var result = new AppResponse<IdentityUser>();
            IdentityUser user = _userRepository.FindById(Id);
            try
            {

                if (user == null)
                {
                    return result.BuildError("Người dùng không tìm thấy");
                }
                user.EmailConfirmed = true;
                return result.BuildResult(user);
            }
            catch (Exception ex)
            {
                if (user == null)
                {
                    return result.BuildError("Người dùng không tìm thấy");
                }
                else
                {
                    return result.BuildError(ex.ToString());
                }
            }
        }



        public async Task<AppResponse<SearchResponse<UserModel>>> Search(SearchRequest request)
        {
            var result = new AppResponse<SearchResponse<UserModel>>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _userRepository.CountRecordsByPredicate(query);

                var users = _userRepository.FindByPredicate(query);
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = new List<UserModel>();
                foreach (var user in UserList)
                {
                    var userModel = new UserModel
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        passwordHash = user.PasswordHash,
                    };

                  
                    var roles = await _userManager.GetRolesAsync(user);
                    userModel.Role = roles.FirstOrDefault();

                    dtoList.Add(userModel);
                }
                var searchUserResult = new SearchResponse<UserModel>
                {
                    TotalRows = numOfRecords,
                    TotalPages = SearchHelper.CalculateNumOfPages(numOfRecords, pageSize),
                    CurrentPage = pageIndex,
                    Data = dtoList,
                };
                return result.BuildResult(searchUserResult);

            }
            catch (Exception ex)
            {

                return result.BuildError(ex.ToString());
            }
        }
        private ExpressionStarter<IdentityUser> BuildFilterExpression(IList<Filter>? Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<IdentityUser>(true);
                if (Filters != null)
                {

                    foreach (var filter in Filters)
                    {
                        switch (filter.FieldName)
                        {
                            case "userName":
                                predicate = predicate.And(m => m.UserName.Contains(filter.Value));
                                break;

                            default:
                                break;
                        }
                    }
                }

                return predicate;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
