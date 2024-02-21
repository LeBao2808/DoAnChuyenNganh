using AirlineTickets.DAL.Contract;
using AirlineTickets.DAL.Implementation;
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
    public class BookTicketsService : IBookTicketsService
    {
        private readonly IBookTicketsRespository _bookTicketsRespository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public BookTicketsService(IBookTicketsRespository BoPhanRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
           _bookTicketsRespository = BoPhanRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<BookTicketsDto> Create(BookTicketsDto request)
        {
            var result = new AppResponse<BookTicketsDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var bookTickets = new BookTickets();
                bookTickets = _mapper.Map<BookTickets>(request);
                bookTickets.Id = Guid.NewGuid();
                bookTickets.CreatedBy = UserName;

                _bookTicketsRespository.Add(bookTickets);

                request.Id = bookTickets.Id;
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
                var bookTickets = new BookTickets();
                bookTickets = _bookTicketsRespository.Get(Id);
                bookTickets.IsDeleted = true;

                _bookTicketsRespository.Edit(bookTickets);

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



        public AppResponse<BookTicketsDto> Edit(BookTicketsDto tuyendung)
        {
            var result = new AppResponse<BookTicketsDto>();
            try
            {
                //var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                var request = new BookTickets();
                request = _mapper.Map<BookTickets>(tuyendung);
                //request.CreatedBy = UserName;
                _bookTicketsRespository.Edit(request);

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

        public AppResponse<List<BookTicketsDto>> GetAll()
        {
            var result = new AppResponse<List<BookTicketsDto>>();
            //string userId = "";
            try
            {
                var query = _bookTicketsRespository.GetAll().Where(x => x.IsDeleted == false);
                var list = query.Where(x => x.IsDeleted == false).Select(m => new BookTicketsDto
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



        public AppResponse<BookTicketsDto> GetId(Guid Id)
        {
            var result = new AppResponse<BookTicketsDto>();
            try
            {
                var bookTickets = _bookTicketsRespository.Get(Id);
                var data = _mapper.Map<BookTicketsDto>(bookTickets);
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
        private ExpressionStarter<BookTickets> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<BookTickets>(true);
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

        public async Task<AppResponse<SearchResponse<BookTicketsDto>>> Search(SearchRequest request)
        {
            var result = new AppResponse<SearchResponse<BookTicketsDto>>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _bookTicketsRespository.CountRecordsByPredicate(query);

                var users = _bookTicketsRespository.FindByPredicate(query);
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<BookTicketsDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchResponse<BookTicketsDto>
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
