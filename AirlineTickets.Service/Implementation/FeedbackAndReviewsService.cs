using AirlineTickets.DAL.Contract;
using AirlineTickets.DAL.Models.Entity;
using AirlineTickets.Model.Dto;
using AirlineTickets.Service.Contract;
using AutoMapper;
using LinqKit;
using MayNghien.Common.Helpers;
using MayNghien.Models.Request.Base;
using MayNghien.Models.Response.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.Service.Implementation
{
    public class FeedbackAndReviewsService:IFeedbackAndReviewsService
    {
        private readonly IFeedbackAndReviewsRespository _feedbackAndReviewsRespository;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public FeedbackAndReviewsService(IFeedbackAndReviewsRespository BoPhanRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _feedbackAndReviewsRespository = BoPhanRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppResponse<FeedbackAndReviewsDto> Create(FeedbackAndReviewsDto request)
        {
            var result = new AppResponse<FeedbackAndReviewsDto>();
            try
            {
                var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                if (UserName == null)
                {
                    return result.BuildError("Cannot find Account by this user");
                }
                var tuyendung = new FeedbackAndReviews();
                tuyendung = _mapper.Map<FeedbackAndReviews>(request);
                tuyendung.Id = Guid.NewGuid();
                tuyendung.CreatedBy = UserName;

                _feedbackAndReviewsRespository.Add(tuyendung);

                request.Id = tuyendung.Id;
                result.IsSuccess = true;
                result.Data = request;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
        }

        public AppResponse<string> Delete(Guid Id)
        {
            var result = new AppResponse<string>();
            try
            {
                var tuyendung = new FeedbackAndReviews();
                tuyendung = _feedbackAndReviewsRespository.Get(Id);
                tuyendung.IsDeleted = true;

                _feedbackAndReviewsRespository.Edit(tuyendung);

                result.IsSuccess = true;
                result.Data = "Delete Sucessfuly";
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + ":" + ex.StackTrace;
                return result;

            }
        }



        public AppResponse<FeedbackAndReviewsDto> Edit(FeedbackAndReviewsDto tuyendung)
        {
            var result = new AppResponse<FeedbackAndReviewsDto>();
            try
            {
                //var UserName = ClaimHelper.GetClainByName(_httpContextAccessor, "UserName");
                var request = new FeedbackAndReviews();
                request = _mapper.Map<FeedbackAndReviews>(tuyendung);
                //request.CreatedBy = UserName;
                _feedbackAndReviewsRespository.Edit(request);

                result.IsSuccess = true;
                result.Data = tuyendung;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
        }

        public AppResponse<List<FeedbackAndReviewsDto>> GetAll()
        {
            var result = new AppResponse<List<FeedbackAndReviewsDto>>();
            //string userId = "";
            try
            {
                var query = _feedbackAndReviewsRespository.GetAll().Where(x => x.IsDeleted == false);
                var list = query.Where(x => x.IsDeleted == false).Select(m => new FeedbackAndReviewsDto
                {
                    Id = m.Id,
                    CustomersId = m.CustomersId,
                    Feedback = m.Feedback,
                    FlightId = m.FlightId,
                    PointEvaluation = m.PointEvaluation,
                    ReactionTime = m.ReactionTime,
                    

                }).ToList();
                result.IsSuccess = true;
                result.Data = list;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;
            }
        }



        public AppResponse<FeedbackAndReviewsDto> GetId(Guid Id)
        {
            var result = new AppResponse<FeedbackAndReviewsDto>();
            try
            {
                var tuyendung = _feedbackAndReviewsRespository.Get(Id);
                var data = _mapper.Map<FeedbackAndReviewsDto>(tuyendung);
                result.IsSuccess = true;
                result.Data = data;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + " " + ex.StackTrace;
                return result;

            }
        }
        private ExpressionStarter<FeedbackAndReviews> BuildFilterExpression(IList<Filter> Filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<FeedbackAndReviews>(true);
                if (Filters != null)
                {
                    foreach (var filter in Filters)
                    {
                        switch (filter.FieldName)
                        {
                            case "customersId":
                                predicate = predicate.And(m => m.CustomersId.ToString().Contains(filter.Value));
                                break;

                            default:
                                break;
                        }
                    }
                }
                predicate = predicate.And(m => m.IsDeleted == false);
                return predicate;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AppResponse<SearchResponse<FeedbackAndReviewsDto>>> Search(SearchRequest request)
        {
            var result = new AppResponse<SearchResponse<FeedbackAndReviewsDto>>();
            try
            {
                var query = BuildFilterExpression(request.Filters);
                var numOfRecords = _feedbackAndReviewsRespository.CountRecordsByPredicate(query);

                var users = _feedbackAndReviewsRespository.FindByPredicate(query);
                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 1;
                int startIndex = (pageIndex - 1) * (int)pageSize;
                var UserList = users.Skip(startIndex).Take(pageSize).ToList();
                var dtoList = _mapper.Map<List<FeedbackAndReviewsDto>>(UserList);
                //if (dtoList != null && dtoList.Count > 0)
                //{
                //    for (int i = 0; i < UserList.Count; i++)
                //    {
                //        var dtouser = dtoList[i];
                //        var identityUser = UserList[i];
                //        dtouser.Role = (await _userManager.GetRolesAsync(identityUser)).First();
                //    }
                //}
                var searchUserResult = new SearchResponse<FeedbackAndReviewsDto>
                {
                    TotalRows = numOfRecords,
                    TotalPages = SearchHelper.CalculateNumOfPages(numOfRecords, pageSize),
                    CurrentPage = pageIndex,
                    Data = dtoList,
                };

                result.Data = searchUserResult;
                result.IsSuccess = true;

                return result;

            }
            catch (Exception ex)
            {

                return result.BuildError(ex.ToString());
            }
        }

    }
}
