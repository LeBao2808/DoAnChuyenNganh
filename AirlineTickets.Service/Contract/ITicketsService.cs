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
   public interface ITicketsService
    {
        public AppResponse<TicketsDto> Create(TicketsDto request);
        public AppResponse<string> Delete(Guid Id);
        public AppResponse<TicketsDto> Edit(TicketsDto tuyendung);
        public AppResponse<List<TicketsDto>> GetAll();
        public AppResponse<TicketsDto> GetId(Guid Id);
        Task<AppResponse<SearchResponse<TicketsDto>>> Search(SearchRequest request);
        public AppResponse<Guid?> GetIdTicket(Guid flightId);
    }
}
