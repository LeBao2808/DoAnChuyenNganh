using AirlineTickets.DAL.Contract;
using AirlineTickets.DAL.Implementation;
using AirlineTickets.Model.Dto;
using AirlineTickets.Service.Contract;
using MayNghien.Models.Request.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirlineTickets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class BookingHistoryController : Controller
    {
        private readonly IBookingHistoryService _bookingHistoryService;
        public BookingHistoryController(IBookingHistoryService bookingHistoryService)
        {
            _bookingHistoryService = bookingHistoryService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _bookingHistoryService.GetAll();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _bookingHistoryService.GetId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(BookingHistoryDto request)
        {
            var result = _bookingHistoryService.Create(request);
            return Ok(result);
        }
        [HttpPut]

        public IActionResult Edit(BookingHistoryDto request)
        {
            var result = _bookingHistoryService.Edit(request);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult Delete(Guid id)
        {

            var result = _bookingHistoryService.Delete(id);

            return Ok(result);

        }
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchUser([FromBody] SearchRequest request)
        {
            var result = await _bookingHistoryService.Search(request);

            return Ok(result);
        }


    }
}
