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
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.Service.Implementation
{
    public class FlightsService : IFlightsService
    {
        private readonly IFlightsRespository _flightRespository;
        private readonly ITicketsRespository _ticketsRespository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public FlightsService(IFlightsRespository BoPhanRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ITicketsRespository ticketsRespository)
        {
            _flightRespository = BoPhanRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _ticketsRespository = ticketsRespository;
        }

        public AppResponse<FlightsDto> Create(FlightsDto request)
        {
            var result = new AppResponse<FlightsDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }


                var tuyendung = _mapper.Map<Flights>(request);
                tuyendung.Id = Guid.NewGuid();
                tuyendung.StartDate = request.StartDate.Value.AddDays(1);
                tuyendung.CreatedBy = UserName;

                _flightRespository.Add(tuyendung);

                _ticketsRespository.Add(new Tickets
                {
                    Id = Guid.NewGuid(),
                    FlightsId = tuyendung.Id,
                    Price = tuyendung.TicketPrice
                });
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
                var tuyendung = new Flights();
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



        public AppResponse<FlightsDto> Edit(FlightsDto tuyendung)
        {
            var result = new AppResponse<FlightsDto>();
            try
            {
                //var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                var request = new Flights();
                request = _mapper.Map<Flights>(tuyendung);
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

        public AppResponse<List<FlightsDto>> GetAll()
        {
            var result = new AppResponse<List<FlightsDto>>();
            //string userId = "";
            try
            {
                var query = _flightRespository.GetAll().Where(x => x.IsDeleted == false);
                var list = query.Where(x => x.IsDeleted == false).Include(x => x.Airlines).Select(m => new FlightsDto
                {
                    Id = m.Id,
                    FlightNumber = m.FlightNumber,
                    FlightTime = m.FlightTime,
                    StartDate = m.StartDate,
                    StartingPoint = m.StartingPoint,
                    EndingPoint = m.EndingPoint,
                    LoGo = m.Airlines.LoGo,
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



        public AppResponse<FlightsDto> GetId(Guid Id)
        {
            var result = new AppResponse<FlightsDto>();
            try
            {
                var tuyendung = _flightRespository.Get(Id);
                var data = _mapper.Map<FlightsDto>(tuyendung);
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
        public AppResponse<List<FlightsDto>> GetAlltrip()
        {
            var result = new AppResponse<List<FlightsDto>>();
            try
            {
                var query = _flightRespository.GetAll().Where(x => !x.IsDeleted);

                var distinctQuery = query
                    .Select(x => new { x.StartingPoint, x.EndingPoint })
                    .Distinct();

                var orderedList = distinctQuery.OrderBy(x => x.EndingPoint); 

                var list = orderedList.Select(group => new FlightsDto
                {
                    StartingPoint = group.StartingPoint,
                    EndingPoint = group.EndingPoint
                }).ToList();

                result.IsSuccess = true;
                result.Data = list;
                return result;
            }
            catch (Exception ex)
            {
                return result.BuildError(ex.ToString());
            }
        }
        private ExpressionStarter<Flights> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<Flights>(true);
                if (Filters != null)
                {
                    foreach (var filter in Filters)
                    {
                        switch (filter.FieldName)
                        {
                           
                            case "flightNumber":
                                predicate = predicate.And(m => m.FlightNumber.Contains(filter.Value));
                                break;
                            case "startingPoint":
                                predicate = predicate.And(m => m.StartingPoint.Contains(filter.Value));
                                break;
                            case "endingPoint":
                                predicate = predicate.And(m => m.EndingPoint.Contains(filter.Value));
                                break;
                            case "flightTime":
                                predicate = predicate.And(m => m.FlightTime.ToString().Contains(filter.Value));
                                break;
                            case "startDate":

                                if (DateTime.TryParse(filter.Value, out DateTime startDate))
                                {
                                    // Lấy ngày, tháng, năm của startDate
                                    var startDateWithoutTime = startDate.Date;
                                    var year = startDateWithoutTime.Year;
                                    var month = startDateWithoutTime.Month;
                                    var day = startDateWithoutTime.Day;

                                    // So sánh với ngày, tháng, năm của trường StartDate
                                    predicate = predicate.And(m => m.StartDate.Value.Year == year && m.StartDate.Value.Month == month && m.StartDate.Value.Day == day);
                                }
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

        public async Task<AppResponse<SearchResponse<FlightsDto>>> Search(SearchRequest request)
        {
            var result = new AppResponse<SearchResponse<FlightsDto>>();
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
                var dtoList =UserList.Select(m => new FlightsDto
                {
                    Id = m.Id,
                    FlightNumber = m.FlightNumber,
                    FlightTime = m.FlightTime,
                    StartDate = m.StartDate,
                    StartingPoint = m.StartingPoint,
                    EndingPoint = m.EndingPoint,
                    AirlinesId = m.AirlinesId,
                    LoGo = m.Airlines.LoGo,
                    NumberOfEmptySeats = m.NumberOfEmptySeats,
                    TicketPrice = m.TicketPrice,
                }).ToList();
                
                var searchUserResult = new SearchResponse<FlightsDto>
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
