using AirlineTickets.DAL.Contract;
using AirlineTickets.DAL.Implementation;
using AirlineTickets.DAL.Models.Entity;
using AirlineTickets.Model.Dto;
using AirlineTickets.Model.Response;
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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AirlineTickets.Service.Implementation
{
    public class FlightsService : IFlightsService
    {
        private readonly IFlightsRespository _flightRespository;
        private readonly ITicketsRespository _ticketsRespository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IAirplaneSeatsRespository _airplaneSeatsRespository;
       
        public FlightsService(IFlightsRespository BoPhanRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ITicketsRespository ticketsRespository, IAirplaneSeatsRespository airplaneSeatsRespository)
        {
            _flightRespository = BoPhanRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _ticketsRespository = ticketsRespository;
            _airplaneSeatsRespository = airplaneSeatsRespository;
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
                tuyendung.CountStatus = 1;
                tuyendung.NumberOfEmptySeats = request.TotalNumberOfSeats;
                _flightRespository.Add(tuyendung);
                var listSeat = new List<AirplaneSeats>();

                for (int i = 0; i < tuyendung.TotalNumberOfSeats; i++)
                {
                    var seat = new AirplaneSeatsDto
                    {
                        Id = Guid.NewGuid(),
                        FlightsId = tuyendung.Id,
                        IsAirplane = false,
                        Seats = i + 1
                    };
                    var seatEntity = _mapper.Map<AirplaneSeatsDto, AirplaneSeats>(seat);
                    listSeat.Add(seatEntity);
                }

           _airplaneSeatsRespository.AddRange(listSeat);

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

      public AppResponse<List<AirplaneSeatsDto>> GetFlights(Guid Id)
        {
            var result = new AppResponse<List<AirplaneSeatsDto>>();
            try
            {
                var listseat = _airplaneSeatsRespository.GetAll().Where(x => x.FlightsId == Id);

                var idListSeats = listseat.OrderBy(x => x.Seats).Select(x => new AirplaneSeatsDto {
                   Id = x.Id,
                   FlightsId = x.FlightsId,
                   IsAirplane = x.IsAirplane,
                }).ToList();


                result.IsSuccess = true;
                result.Data = idListSeats;
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
        public void UpdateStatusAutomatically()
        {
            try
            {
                var currentDate = DateTime.Now.Date;
       
               
               var listflight = _flightRespository.FindByPredicate(x => x.IsDeleted == false && x.StartDate <= currentDate ).ToList();
              if( listflight != null )
                {
                    foreach( var flight in listflight )
                    {
                        flight.CountStatus = 0;
                        flight.IsDeleted = true;
                        _flightRespository.Edit(flight);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
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
        private ExpressionStarter<Flights> BuildFilterExpression(IList<Filter> filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<Flights>(false); // Bắt đầu với giá trị false

                bool isPredicateEmpty = true; // Biến để kiểm tra xem có điều kiện nào được thêm vào predicate hay không

                if (filters != null)
                {
                    foreach (var filter in filters)
                    {
                        switch (filter.FieldName)
                        {
                            case "flightNumber":
                                predicate = predicate.Or(m => m.FlightNumber.Contains(filter.Value));
                                isPredicateEmpty = false; // Đánh dấu là có điều kiện đã được thêm vào predicate
                                break;

                            case "startingPoint":
                                predicate = predicate.Or(m => m.StartingPoint.Contains(filter.Value));
                                isPredicateEmpty = false; // Đánh dấu là có điều kiện đã được thêm vào predicate
                                break;

                            case "endingPoint":
                                predicate = predicate.Or(m => m.EndingPoint.Contains(filter.Value));
                                isPredicateEmpty = false; // Đánh dấu là có điều kiện đã được thêm vào predicate
                                break;

                            case "flightTime":
                                predicate = predicate.Or(m => m.FlightTime.ToString().Contains(filter.Value));
                                isPredicateEmpty = false; // Đánh dấu là có điều kiện đã được thêm vào predicate
                                break;

                            case "startDate":
                                if (DateTime.TryParse(filter.Value, out DateTime startDate))
                                {
                                    var startDateWithoutTime = startDate.Date;
                                    var year = startDateWithoutTime.Year;
                                    var month = startDateWithoutTime.Month;
                                    var day = startDateWithoutTime.Day;

                                    predicate = predicate.Or(m => m.StartDate.Value.Year == year && m.StartDate.Value.Month == month && m.StartDate.Value.Day == day);
                                    isPredicateEmpty = false; // Đánh dấu là có điều kiện đã được thêm vào predicate
                                }
                                else
                                {
                                    var today = DateTime.Today;
                                    var years = today.Year;
                                    var months = today.Month;
                                    var days = today.Day;

                                    predicate = predicate.Or(m => m.StartDate.Value.Year >= years &&
                                                                   m.StartDate.Value.Month >= months &&
                                                                   m.StartDate.Value.Day >= days);
                                    isPredicateEmpty = false; // Đánh dấu là có điều kiện đã được thêm vào predicate
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }

                // Nếu không có điều kiện nào được thêm vào predicate, thêm điều kiện mới
                if (isPredicateEmpty)
                {
                    var today = DateTime.Today;
                    var years = today.Year;
                    var months = today.Month;
                    var days = today.Day;

                    predicate = PredicateBuilder.New<Flights>(true)
                        .Or(m => m.StartDate.Value.Year >= years &&
                                 m.StartDate.Value.Month >= months &&
                                 m.StartDate.Value.Day >= days);
                }

                predicate = predicate.And(m => m.IsDeleted == false);

                return predicate;
            }
            catch (Exception ex)
            {
                throw new Exception("Error building filter expression", ex);
            }
        }


        private ExpressionStarter<Flights> BuildFilterExpression2(IList<Filter> filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<Flights>(false); // Bắt đầu với giá trị false

                bool isPredicateEmpty = true; // Biến để kiểm tra xem có điều kiện nào được thêm vào predicate hay không

                if (filters != null)
                {
                   
                // Nếu không có điều kiện nào được thêm vào predicate, thêm điều kiện mới
                if (isPredicateEmpty)
                {
                        var today = DateTime.Today;
                        var years = today.Year;
                        var months = today.Month;
                        var days = today.Day;

                        predicate = predicate.Or(m => m.StartDate.Value.Year >= years &&
                                                       m.StartDate.Value.Month >= months &&
                                                       m.StartDate.Value.Day >= days);
                    }
                }
                predicate = predicate.And(m => m.IsDeleted == false);

                return predicate;
            }
            catch (Exception ex)
            {
                throw new Exception("Error building filter expression", ex);
            }
        }

        public async Task<AppResponse<SearchResponse<FlightsDto>>> Search(SearchRequest request)
        {
            var result = new AppResponse<SearchResponse<FlightsDto>>();
            try
            {
                var coutstatus = _flightRespository.FindByPredicate(x => x.CountStatus == 1).ToList();
                if (coutstatus.Count > 0)
                {
                    UpdateStatusAutomatically();
                }

                var query = BuildFilterExpression(request.Filters);
                

                var numOfRecords = _flightRespository.CountRecordsByPredicate(query);
                if (numOfRecords == 0)
                {

                    var query2 = BuildFilterExpression2(request.Filters);
                    query = query2;
                    numOfRecords = _flightRespository.CountRecordsByPredicate(query);
                }
                var users = _flightRespository.FindByPredicate(query);
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize);

                var dtoList = UserList.Select(m => new FlightsDto
                {
                    Id = m.Id,
                    FlightNumber = m.FlightNumber,
                    FlightTime = m.FlightTime,
                    StartDate = m.StartDate,
                    TotalNumberOfSeats = m.TotalNumberOfSeats,
                    FlightTimeEnd = m.FlightTimeEnd,
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
