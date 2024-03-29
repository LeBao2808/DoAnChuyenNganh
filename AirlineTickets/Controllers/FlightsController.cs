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
    public class FlightsController : Controller
    {
        private readonly IFlightsService _flightService;
        public FlightsController(IFlightsService ChucVuService)
        {
            _flightService = ChucVuService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _flightService.GetAll();
            return Ok(result);
        }
        [HttpGet]
        [Route("Trip")]
        public IActionResult GetAlltrip()
        {
            var result = _flightService.GetAlltrip();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _flightService.GetId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(FlightsDto request)
        {
            var result = _flightService.Create(request);
            return Ok(result);
        }
        [HttpPut]

        public IActionResult Edit(FlightsDto request)
        {
            var result = _flightService.Edit(request);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult Delete(Guid id)
        {

            var result = _flightService.Delete(id);

            return Ok(result);

        }
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchUser([FromBody] SearchRequest request)
        {
            var result = await _flightService.Search(request);

            return Ok(result);
        }
    }
}
