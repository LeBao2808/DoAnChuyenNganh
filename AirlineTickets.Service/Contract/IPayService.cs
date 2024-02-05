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
    public interface IPayService
    {
        public AppResponse<PayDto> Create(PayDto request);
        public AppResponse<string> Delete(Guid Id);
        public AppResponse<PayDto> Edit(PayDto tuyendung);
        public AppResponse<List<PayDto>> GetAll();
        public AppResponse<PayDto> GetId(Guid Id);
        Task<AppResponse<SearchResponse<PayDto>>> Search(SearchRequest request);
    }
}
