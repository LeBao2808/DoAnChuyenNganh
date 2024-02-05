using AirlineTickets.Model.Dto;
using AirlineTickets.Service.Contract;
using MayNghien.Models.Request.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirlineTickets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackAndReviewsController : Controller
    {
        private readonly IFeedbackAndReviewsService _feedbackAndReviewsService;
        public FeedbackAndReviewsController(IFeedbackAndReviewsService ChucVuService)
        {
            _feedbackAndReviewsService = ChucVuService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _feedbackAndReviewsService.GetAll();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _feedbackAndReviewsService.GetId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(FeedbackAndReviewsDto request)
        {
            var result = _feedbackAndReviewsService.Create(request);
            return Ok(result);
        }
        [HttpPut]

        public IActionResult Edit(FeedbackAndReviewsDto request)
        {
            var result = _feedbackAndReviewsService.Edit(request);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult Delete(Guid id)
        {

            var result = _feedbackAndReviewsService.Delete(id);

            return Ok(result);

        }
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchUser([FromBody] SearchRequest request)
        {
            var result = await _feedbackAndReviewsService.Search(request);

            return Ok(result);
        }

    }
}
