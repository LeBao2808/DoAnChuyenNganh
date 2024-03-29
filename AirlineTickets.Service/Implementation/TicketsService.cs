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
    public class TicketsService : ITicketsService
    {
        private readonly ITicketsRespository _ticketsRespository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public TicketsService(ITicketsRespository ticketsRespository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _ticketsRespository = ticketsRespository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<TicketsDto> Create(TicketsDto request)
        {
            var result = new AppResponse<TicketsDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var bookTickets = new Tickets();
                bookTickets = _mapper.Map<Tickets>(request);
                bookTickets.Id = Guid.NewGuid();
                bookTickets.CreatedBy = UserName;

                _ticketsRespository.Add(bookTickets);

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

        public AppResponse<Guid?> GetIdTicket(Guid flightId)
        {
            var result = new AppResponse<Guid?>();
            try
            {
                var ticket = _ticketsRespository.FindByPredicate(x => x.FlightsId == flightId).FirstOrDefault(x => x.IsDeleted == false);
                if (ticket != null)
                {
                    result.IsSuccess = true;
                    result.Data = ticket.Id;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Ticket not found for the provided flight ID.";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
            }
            return result;
        }

        public AppResponse<string> Delete(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var bookTickets = new Tickets();
                bookTickets = _ticketsRespository.Get(Id);
                bookTickets.IsDeleted = true;

                _ticketsRespository.Edit(bookTickets);

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



        public AppResponse<TicketsDto> Edit(TicketsDto tuyendung)
        {
            var result = new AppResponse<TicketsDto>();
            try
            {
                //var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                var request = new Tickets();
                request = _mapper.Map<Tickets>(tuyendung);
                //request.CreatedBy = UserName;
                _ticketsRespository.Edit(request);

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

        public AppResponse<List<TicketsDto>> GetAll()
        {
            var result = new AppResponse<List<TicketsDto>>();
            //string userId = "";
            try
            {
                var query = _ticketsRespository.GetAll().Where(x => x.IsDeleted == false);
                var list = query.Where(x => x.IsDeleted == false).Select(m => new TicketsDto
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



        public AppResponse<TicketsDto> GetId(Guid Id)
        {
            var result = new AppResponse<TicketsDto>();
            try
            {
                var bookTickets = _ticketsRespository.Get(Id);
                var data = _mapper.Map<TicketsDto>(bookTickets);
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
        private ExpressionStarter<Tickets> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<Tickets>(true);
                if (Filters != null)
                {
                    foreach (var filter in Filters)
                    {
                        switch (filter.FieldName)
                        {
                            //case "customersId":
                            //    predicate = predicate.And(m => m.CustomersId.ToString().Contains(filter.Value));
                            //    break;

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

        public async Task<AppResponse<SearchResponse<TicketsDto>>> Search(SearchRequest request)
        {
            var result = new AppResponse<SearchResponse<TicketsDto>>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _ticketsRespository.CountRecordsByPredicate(query);

                var users = _ticketsRespository.FindByPredicate(query);
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<TicketsDto>>(UserList);
                
                var searchUserResult = new SearchResponse<TicketsDto>
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
