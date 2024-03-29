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
    public interface IAirlinesService
    {
        public AppResponse<AirlinesDto> Create(AirlinesDto request);
        public AppResponse<string> Delete(Guid Id);
        public AppResponse<AirlinesDto> Edit(AirlinesDto tuyendung);
        public AppResponse<List<AirlinesDto>> GetAll();
        public AppResponse<AirlinesDto> GetId(Guid Id);
        Task<AppResponse<SearchResponse<AirlinesDto>>> Search(SearchRequest request);
    }
}
