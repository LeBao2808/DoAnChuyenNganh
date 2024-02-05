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
    public interface IFlightService
    {
        public AppResponse<FlightDto> Create(FlightDto request);
        public AppResponse<string> Delete(Guid Id);
        public AppResponse<FlightDto> Edit(FlightDto tuyendung);
        public AppResponse<List<FlightDto>> GetAll();
        public AppResponse<FlightDto> GetId(Guid Id);
        Task<AppResponse<SearchResponse<FlightDto>>> Search(SearchRequest request);
    }
}
