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
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRespository _customersRespository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public CustomersService(ICustomersRespository BoPhanRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _customersRespository = BoPhanRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<CustomersDto> Create(CustomersDto request)
        {
            var result = new AppResponse<CustomersDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var tuyendung = new Customers();
                tuyendung = _mapper.Map<Customers>(request);
                tuyendung.Id = Guid.NewGuid();
                tuyendung.CreatedBy = UserName;

                _customersRespository.Add(tuyendung);

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
                var tuyendung = new Customers();
                tuyendung = _customersRespository.Get(Id);
                tuyendung.IsDeleted = true;

                _customersRespository.Edit(tuyendung);

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



        public AppResponse<CustomersDto> Edit(CustomersDto tuyendung)
        {
            var result = new AppResponse<CustomersDto>();
            try
            {
                //var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                var request = new Customers();
                request = _mapper.Map<Customers>(tuyendung);
                //request.CreatedBy = UserName;
                _customersRespository.Edit(request);

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

        public AppResponse<List<CustomersDto>> GetAll()
        {
            var result = new AppResponse<List<CustomersDto>>();
            //string userId = "";
            try
            {
                var query = _customersRespository.GetAll().Where(x => x.IsDeleted == false);
                var list = query.Where(x => x.IsDeleted == false).Select(m => new CustomersDto
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



        public AppResponse<CustomersDto> GetId(Guid Id)
        {
            var result = new AppResponse<CustomersDto>();
            try
            {
                var tuyendung = _customersRespository.Get(Id);
                var data = _mapper.Map<CustomersDto>(tuyendung);
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
        private ExpressionStarter<Customers> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<Customers>(true);
                if (Filters != null)
                {
                    foreach (var filter in Filters)
                    {
                        switch (filter.FieldName)
                        {
                            case "phoneNumber":
                                predicate = predicate.And(m => m.PhoneNumber.Contains(filter.Value));
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

        public async Task<AppResponse<SearchResponse<CustomersDto>>> Search(SearchRequest request)
        {
            var result = new AppResponse<SearchResponse<CustomersDto>>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _customersRespository.CountRecordsByPredicate(query);

                var users = _customersRespository.FindByPredicate(query);
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<CustomersDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchResponse<CustomersDto>
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
