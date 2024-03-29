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
    public interface ILuggagesService
    {
        public AppResponse<LuggagesDto> Create(LuggagesDto request);
        public AppResponse<string> Delete(Guid Id);
        public AppResponse<LuggagesDto> Edit(LuggagesDto tuyendung);
        public AppResponse<List<LuggagesDto>> GetAll();
        public AppResponse<LuggagesDto> GetId(Guid Id);
        Task<AppResponse<SearchResponse<LuggagesDto>>> Search(SearchRequest request);
    }
}
