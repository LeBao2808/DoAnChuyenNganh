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
    public interface IAirplaneSeatsService
    {
        public AppResponse<AirplaneSeatsDto> Create(AirplaneSeatsDto request);
        public AppResponse<string> Delete(Guid Id);
        public AppResponse<AirplaneSeatsDto> Edit(AirplaneSeatsDto tuyendung);
        public AppResponse<List<AirplaneSeatsDto>> GetAll();
        public AppResponse<AirplaneSeatsDto> GetId(Guid Id);
        Task<AppResponse<SearchResponse<AirplaneSeatsDto>>> Search(SearchRequest request);
    }
}
