using AirlineTickets.Model.Dto;
using MayNghien.Models.Request.Base;
using MayNghien.Models.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.Service.Contract
{
    public interface IFlightsService
    {
        public AppResponse<FlightsDto> Create(FlightsDto request);
        public AppResponse<string> Delete(Guid Id);
        public AppResponse<FlightsDto> Edit(FlightsDto tuyendung);
        public AppResponse<List<FlightsDto>> GetAll();
        public AppResponse<FlightsDto> GetId(Guid Id);
        public AppResponse<List<FlightsDto>> GetAlltrip();
        public AppResponse<List<AirplaneSeatsDto>> GetFlights(Guid Id);
        Task<AppResponse<SearchResponse<FlightsDto>>> Search(SearchRequest request);
    }
}
