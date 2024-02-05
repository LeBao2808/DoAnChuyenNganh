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
    public class BookingHistoryService : IBookingHistoryService
    {
        private readonly IBookingHistoryRespository _bookingHistoryRespository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public BookingHistoryService(IBookingHistoryRespository bookTicketsRespository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _bookingHistoryRespository = bookTicketsRespository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<BookingHistoryDto> Create(BookingHistoryDto request)
        {
            var result = new AppResponse<BookingHistoryDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var tuyendung = new BookingHistory();
                tuyendung = _mapper.Map<BookingHistory>(request);
                tuyendung.Id = Guid.NewGuid();
                tuyendung.CreatedBy = UserName;

                _bookingHistoryRespository.Add(tuyendung);

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
                var tuyendung = new BookingHistory();
                tuyendung = _bookingHistoryRespository.Get(Id);
                tuyendung.IsDeleted = true;

                _bookingHistoryRespository.Edit(tuyendung);

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



        public AppResponse<BookingHistoryDto> Edit(BookingHistoryDto tuyendung)
        {
            var result = new AppResponse<BookingHistoryDto>();
            try
            {
                //var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                var request = new BookingHistory();
                request = _mapper.Map<BookingHistory>(tuyendung);
                //request.CreatedBy = UserName;
                _bookingHistoryRespository.Edit(request);

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

        public AppResponse<List<BookingHistoryDto>> GetAll()
        {
            var result = new AppResponse<List<BookingHistoryDto>>();
            //string userId = "";
            try
            {
                var query = _bookingHistoryRespository.GetAll().Where(x => x.IsDeleted == false);
                var list = query.Where(x => x.IsDeleted == false).Select(m => new BookingHistoryDto
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



        public AppResponse<BookingHistoryDto> GetId(Guid Id)
        {
            var result = new AppResponse<BookingHistoryDto>();
            try
            {
                var tuyendung = _bookingHistoryRespository.Get(Id);
                var data = _mapper.Map<BookingHistoryDto>(tuyendung);
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
        private ExpressionStarter<BookingHistory> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<BookingHistory>(true);
                if (Filters != null)
                {
                    foreach (var filter in Filters)
                    {
                        switch (filter.FieldName)
                        {
                            case "bookTicketsId":
                                predicate = predicate.And(m => m.BookTicketsId.ToString().Contains(filter.Value));
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

        public async Task<AppResponse<SearchResponse<BookingHistoryDto>>> Search(SearchRequest request)
        {
            var result = new AppResponse<SearchResponse<BookingHistoryDto>>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _bookingHistoryRespository.CountRecordsByPredicate(query);

                var users = _bookingHistoryRespository.FindByPredicate(query);
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<BookingHistoryDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchResponse<BookingHistoryDto>
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
