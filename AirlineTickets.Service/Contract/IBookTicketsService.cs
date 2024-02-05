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
    public interface IBookTicketsService
    {
        public AppResponse<BookTicketsDto> Create(BookTicketsDto request);
        public AppResponse<string> Delete(Guid Id);
        public AppResponse<BookTicketsDto> Edit(BookTicketsDto tuyendung);
        public AppResponse<List<BookTicketsDto>> GetAll();
        public AppResponse<BookTicketsDto> GetId(Guid Id);
        Task<AppResponse<SearchResponse<BookTicketsDto>>> Search(SearchRequest request);
    }
}
