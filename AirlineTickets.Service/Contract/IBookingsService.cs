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
    public interface IBookingsService
    {
        public AppResponse<BookingsDto> Create(BookingsDto request);
        public AppResponse<string> Delete(Guid Id);
        public AppResponse<BookingsDto> Edit(BookingsDto tuyendung);
        public AppResponse<List<BookingsDto>> GetAll();
        public AppResponse<BookingsDto> GetId(Guid Id);
        Task<AppResponse<SearchResponse<BookingsDto>>> Search(SearchRequest request);
    }
}
