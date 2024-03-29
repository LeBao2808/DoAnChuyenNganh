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
    public interface IPaymentsService
    {
        public AppResponse<PaymentsDto> Create(PaymentsDto request);
        public AppResponse<string> Delete(Guid Id);
        public AppResponse<PaymentsDto> Edit(PaymentsDto tuyendung);
        public AppResponse<List<PaymentsDto>> GetAll();
        public AppResponse<PaymentsDto> GetId(Guid Id);
        Task<AppResponse<SearchResponse<PaymentsDto>>> Search(SearchRequest request);
    }
}
