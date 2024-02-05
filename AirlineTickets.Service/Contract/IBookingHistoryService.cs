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
    public interface IBookingHistoryService
    {
        public AppResponse<BookingHistoryDto> Create(BookingHistoryDto request);
        public AppResponse<string> Delete(Guid Id);
        public AppResponse<BookingHistoryDto> Edit(BookingHistoryDto tuyendung);
        public AppResponse<List<BookingHistoryDto>> GetAll();
        public AppResponse<BookingHistoryDto> GetId(Guid Id);
        Task<AppResponse<SearchResponse<BookingHistoryDto>>> Search(SearchRequest request);
    }
}
