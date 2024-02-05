using AirlineTickets.DAL.Contract;
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
    public class PayService : IPayService
    {
        private readonly IPayRespository _payRespository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public PayService(IPayRespository BoPhanRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _payRespository = BoPhanRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<PayDto> Create(PayDto request)
        {
            var result = new AppResponse<PayDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var tuyendung = new Pay();
                tuyendung = _mapper.Map<Pay>(request);
                tuyendung.Id = Guid.NewGuid();
                tuyendung.CreatedBy = UserName;

                _payRespository.Add(tuyendung);

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
                var tuyendung = new Pay();
                tuyendung = _payRespository.Get(Id);
                tuyendung.IsDeleted = true;

                _payRespository.Edit(tuyendung);

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



        public AppResponse<PayDto> Edit(PayDto tuyendung)
        {
            var result = new AppResponse<PayDto>();
            try
            {
                //var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                var request = new Pay();
                request = _mapper.Map<Pay>(tuyendung);
                //request.CreatedBy = UserName;
                _payRespository.Edit(request);

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

        public AppResponse<List<PayDto>> GetAll()
        {
            var result = new AppResponse<List<PayDto>>();
            //string userId = "";
            try
            {
                var query = _payRespository.GetAll().Where(x => x.IsDeleted == false);
                var list = query.Where(x => x.IsDeleted == false).Select(m => new PayDto
                {
                    Id = m.Id,
                  

                }).ToList();
                result.IsSuccess = true;
                result.Data = list;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
        }



        public AppResponse<PayDto> GetId(Guid Id)
        {
            var result = new AppResponse<PayDto>();
            try
            {
                var tuyendung = _payRespository.Get(Id);
                var data = _mapper.Map<PayDto>(tuyendung);
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
        private ExpressionStarter<Pay> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<Pay>(true);
                if (Filters != null)
                {
                    foreach (var filter in Filters)
                    {
                        switch (filter.FieldName)
                        {
                            case "customersId":
                                predicate = predicate.And(m => m.CustomersId.ToString().Contains(filter.Value));
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

        public async Task<AppResponse<SearchResponse<PayDto>>> Search(SearchRequest request)
        {
            var result = new AppResponse<SearchResponse<PayDto>>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _payRespository.CountRecordsByPredicate(query);

                var users = _payRespository.FindByPredicate(query);
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<PayDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchResponse<PayDto>
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
