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
    public interface IFeedbackAndReviewsService
    {
        public AppResponse<FeedbackAndReviewsDto> Create(FeedbackAndReviewsDto request);
        public AppResponse<string> Delete(Guid Id);
        public AppResponse<FeedbackAndReviewsDto> Edit(FeedbackAndReviewsDto tuyendung);
        public AppResponse<List<FeedbackAndReviewsDto>> GetAll();
        public AppResponse<FeedbackAndReviewsDto> GetId(Guid Id);
        Task<AppResponse<SearchResponse<FeedbackAndReviewsDto>>> Search(SearchRequest request);
    }
}
