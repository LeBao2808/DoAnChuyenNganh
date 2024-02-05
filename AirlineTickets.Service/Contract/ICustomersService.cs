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
    public interface ICustomersService
    {
        public AppResponse<CustomersDto> Create(CustomersDto request);
        public AppResponse<string> Delete(Guid Id);
        public AppResponse<CustomersDto> Edit(CustomersDto tuyendung);
        public AppResponse<List<CustomersDto>> GetAll();
        public AppResponse<CustomersDto> GetId(Guid Id);
        Task<AppResponse<SearchResponse<CustomersDto>>> Search(SearchRequest request);
    }
}
