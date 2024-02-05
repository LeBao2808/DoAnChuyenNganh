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
    public interface IPromotionService
    {
        public AppResponse<PromotionDto> Create(PromotionDto request);
        public AppResponse<string> Delete(Guid Id);
        public AppResponse<PromotionDto> Edit(PromotionDto tuyendung);
        public AppResponse<List<PromotionDto>> GetAll();
        public AppResponse<PromotionDto> GetId(Guid Id);
        Task<AppResponse<SearchResponse<PromotionDto>>> Search(SearchRequest request);
    }
}
