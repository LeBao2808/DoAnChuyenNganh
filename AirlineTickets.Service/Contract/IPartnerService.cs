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
    public interface IPartnerService
    {
        public AppResponse<PartnerDto> Create(PartnerDto request);
        public AppResponse<string> Delete(Guid Id);
        public AppResponse<PartnerDto> Edit(PartnerDto tuyendung);
        public AppResponse<List<PartnerDto>> GetAll();
        public AppResponse<PartnerDto> GetId(Guid Id);
        Task<AppResponse<SearchResponse<PartnerDto>>> Search(SearchRequest request);
    }
}
