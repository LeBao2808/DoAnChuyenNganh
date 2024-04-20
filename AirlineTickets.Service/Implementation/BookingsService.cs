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
    public class BookingsService : IBookingsService
    {
        private readonly IBookingsRespository _bookTicketsRespository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomersRespository _CustomersRespository;
        public BookingsService(IBookingsRespository BoPhanRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor , ICustomersRespository customersRespository)
        {
            _bookTicketsRespository = BoPhanRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _CustomersRespository = customersRespository;
        }

        public AppResponse<BookingsDto> Create(BookingsDto request)
        {
            var result = new AppResponse<BookingsDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                    return result.BuildError("Cannot find Account by this user");

                var Customer = _mapper.Map<Customers>(request.Customers);
                Customer.Id = new Guid();
                _CustomersRespository.Add(Customer);

                var bookTickets = new Bookings
                {
                   TicketsId = request.TicketsId,
                    CustomersId = Customer.Id,
                    CorrespondingTicketPrices = request.CorrespondingTicketPrices,
                    BookingDate = request.BookingDate,
                    Quantity = request.Quantity,
                    Seats = request.Seats,
                    TicketBookingStatus = request.TicketBookingStatus,
                    LuggagesId = request.LuggagesId,
                    CreatedBy = UserName
                };
    
                bookTickets.Id = new Guid();
                _bookTicketsRespository.Add(bookTickets);

                
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
                var bookTickets = new Bookings();
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



        public AppResponse<BookingsDto> Edit(BookingsDto tuyendung)
        {
            var result = new AppResponse<BookingsDto>();
            try
            {
                //var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                var request = new Bookings();
                request = _mapper.Map<Bookings>(tuyendung);
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

        public AppResponse<List<BookingsDto>> GetAll()
        {
            var result = new AppResponse<List<BookingsDto>>();
            //string userId = "";
            try
            {
                var query = _bookTicketsRespository.GetAll().Where(x => x.IsDeleted == false);
                var list = query.Where(x => x.IsDeleted == false).Select(m => new BookingsDto
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



        public AppResponse<BookingsDto> GetId(Guid Id)
        {
            var result = new AppResponse<BookingsDto>();
            try
            {
                var bookTickets = _bookTicketsRespository.Get(Id);
                var data = _mapper.Map<BookingsDto>(bookTickets);
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
        private ExpressionStarter<Bookings> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<Bookings>(true);
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

        public async Task<AppResponse<SearchResponse<BookingsDto>>> Search(SearchRequest request)
        {
            var result = new AppResponse<SearchResponse<BookingsDto>>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _bookTicketsRespository.CountRecordsByPredicate(query);

                var users = _bookTicketsRespository.FindByPredicate(query);
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<BookingsDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchResponse<BookingsDto>
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
