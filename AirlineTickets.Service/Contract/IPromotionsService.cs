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
    public interface IPromotionsService
    {
        public AppResponse<PromotionsDto> Create(PromotionsDto request);
        public AppResponse<string> Delete(Guid Id);
        public AppResponse<PromotionsDto> Edit(PromotionsDto tuyendung);
        public AppResponse<List<PromotionsDto>> GetAll();
        public AppResponse<PromotionsDto> GetId(Guid Id);
        Task<AppResponse<SearchResponse<PromotionsDto>>> Search(SearchRequest request);
    }
}
