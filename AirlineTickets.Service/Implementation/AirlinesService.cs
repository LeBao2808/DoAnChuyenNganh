﻿using AirlineTickets.DAL.Contract;
using AirlineTickets.DAL.Models.Entity;
using AirlineTickets.Model.Dto;
using AirlineTickets.Service.Contract;
using AutoMapper;
using LinqKit;
using MayNghien.Common.Helpers;
using MayNghien.Models.Request.Base;
using MayNghien.Models.Response.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.Service.Implementation
{
    public class AirlinesService : IAirlinesService
    {
        private readonly IAirlinesRespository _partnerRespository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public AirlinesService(IAirlinesRespository BoPhanRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _partnerRespository = BoPhanRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        public AppResponse<AirlinesDto> Create(AirlinesDto request)
        {
            var result = new AppResponse<AirlinesDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var tuyendung = new Airlines();
                tuyendung = _mapper.Map<Airlines>(request);
                tuyendung.Id = Guid.NewGuid();
                tuyendung.CreatedBy = UserName;

                _partnerRespository.Add(tuyendung);

                request.Id = tuyendung.Id;
                result.IsSuccess = true;
                result.Data = request;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
        }

        public AppResponse<string> Delete(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var tuyendung = new Airlines();
                tuyendung = _partnerRespository.Get(Id);
                tuyendung.IsDeleted = true;

                _partnerRespository.Edit(tuyendung);

                result.IsSuccess = true;
                result.Data = "Delete Sucessfuly";
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + ":" + ex.StackTrace;
                return result;

            }
        }



        public AppResponse<AirlinesDto> Edit(AirlinesDto tuyendung)
        {
            var result = new AppResponse<AirlinesDto>();
            try
            {
                //var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                var request = new Airlines();
                request = _mapper.Map<Airlines>(tuyendung);
                //request.CreatedBy = UserName;
                _partnerRespository.Edit(request);

                result.IsSuccess = true;
                result.Data = tuyendung;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
        }

        public AppResponse<List<AirlinesDto>> GetAll()
        {
            var result = new AppResponse<List<AirlinesDto>>();
            //string userId = "";
            try
            {
                var query = _partnerRespository.GetAll().Where(x => x.IsDeleted == false);
                var list = query.Where(x => x.IsDeleted == false).Select(m => new AirlinesDto
                {
                    Id = m.Id,
                    Address = m.Address,
                    Email = m.Email,
                    Information = m.Information,
                    LoGo = m.LoGo,
                    Name = m.Name,
                    PhoneNumber = m.PhoneNumber,

                }).ToList();
                //result.IsSuccess = true;
                //result.Data = list;
                //return result;
                return result.BuildResult(list);
            
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
        }



        public AppResponse<AirlinesDto> GetId(Guid Id)
        {
            var result = new AppResponse<AirlinesDto>();
            try
            {
                var tuyendung = _partnerRespository.Get(Id);
                var data = _mapper.Map<AirlinesDto>(tuyendung);
                result.IsSuccess = true;
                result.Data = data;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;

            }
        }
        private ExpressionStarter<Airlines> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<Airlines>(true);
                if (Filters != null)
                {
                    foreach (var filter in Filters)
                    {
                        switch (filter.FieldName)
                        {
                            case "name":
                                predicate = predicate.And(m => m.Name.Contains(filter.Value));
                                break;

                            default:
                                break;
                        }
                    }
                }
                predicate = predicate.And(m => m.IsDeleted == false);
                return predicate;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AppResponse<SearchResponse<AirlinesDto>>> Search(SearchRequest request)
        {
            var result = new AppResponse<SearchResponse<AirlinesDto>>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _partnerRespository.CountRecordsByPredicate(query);

                var users = _partnerRespository.FindByPredicate(query);
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<AirlinesDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchResponse<AirlinesDto>
                {
                    TotalRows = numOfRecords,
                    TotalPages = SearchHelper.CalculateNumOfPages(numOfRecords, pageSize),
                    CurrentPage = pageIndex,
                    Data = dtoList,
                };

                result.Data = searchUserResult;
                result.IsSuccess = true;

                return result;

            }
            catch (Exception ex)
            {

                return result.BuildError(ex.ToString());
            }
        }

    }
}
