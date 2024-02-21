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
    public class FlightService : IFlightService
    {
        private readonly IFlightRespository _flightRespository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public FlightService(IFlightRespository BoPhanRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _flightRespository = BoPhanRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<FlightDto> Create(FlightDto request)
        {
            var result = new AppResponse<FlightDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var tuyendung = new Flight();
                tuyendung = _mapper.Map<Flight>(request);
                tuyendung.Id = Guid.NewGuid();
                tuyendung.CreatedBy = UserName;

                _flightRespository.Add(tuyendung);

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
                var tuyendung = new Flight();
                tuyendung = _flightRespository.Get(Id);
                tuyendung.IsDeleted = true;

                _flightRespository.Edit(tuyendung);

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



        public AppResponse<FlightDto> Edit(FlightDto tuyendung)
        {
            var result = new AppResponse<FlightDto>();
            try
            {
                //var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                var request = new Flight();
                request = _mapper.Map<Flight>(tuyendung);
                //request.CreatedBy = UserName;
                _flightRespository.Edit(request);

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

        public AppResponse<List<FlightDto>> GetAll()
        {
            var result = new AppResponse<List<FlightDto>>();
            //string userId = "";
            try
            {
                var query = _flightRespository.GetAll().Where(x => x.IsDeleted == false);
                var list = query.Where(x => x.IsDeleted == false).Select(m => new FlightDto
                {
                    Id = m.Id,
                    FlightNumber = m.FlightNumber,
                    FlightTime = m.FlightTime,
                    StartDate = m.StartDate,
                    StartingPoint = m.StartingPoint,
                    EndingPoint = m.EndingPoint,
                    PartnerId = m.PartnerId,
                    NumberOfEmptySeats = m.NumberOfEmptySeats,
                    TicketPrice = m.TicketPrice,

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



        public AppResponse<FlightDto> GetId(Guid Id)
        {
            var result = new AppResponse<FlightDto>();
            try
            {
                var tuyendung = _flightRespository.Get(Id);
                var data = _mapper.Map<FlightDto>(tuyendung);
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
        private ExpressionStarter<Flight> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<Flight>(true);
                if (Filters != null)
                {
                    foreach (var filter in Filters)
                    {
                        switch (filter.FieldName)
                        {
                            case "partnerId":
                                predicate = predicate.And(m => m.PartnerId.ToString().Contains(filter.Value));
                                break;
                            case "flightNumber":
                                predicate = predicate.And(m => m.FlightNumber.Contains(filter.Value));
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

        public async Task<AppResponse<SearchResponse<FlightDto>>> Search(SearchRequest request)
        {
            var result = new AppResponse<SearchResponse<FlightDto>>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _flightRespository.CountRecordsByPredicate(query);

                var users = _flightRespository.FindByPredicate(query);
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize);
                //var dtoList = _mapper.Map<List<FlightDto>>(UserList);
                var dtoList =UserList.Select(m => new FlightDto
                {
                    Id = m.Id,
                    FlightNumber = m.FlightNumber,
                    FlightTime = m.FlightTime,
                    StartDate = m.StartDate,
                    StartingPoint = m.StartingPoint,
                    EndingPoint = m.EndingPoint,
                    PartnerId = m.PartnerId,
                    NumberOfEmptySeats = m.NumberOfEmptySeats,
                    TicketPrice = m.TicketPrice,
                }).ToList();
                
                var searchUserResult = new SearchResponse<FlightDto>
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
