using AirlineTickets.Model.Dto;
using AirlineTickets.Service.Contract;
using MayNghien.Models.Request.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirlineTickets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : Controller
    {
        private readonly IFlightService _flightService;
        public FlightController(IFlightService ChucVuService)
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
        [Route("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _flightService.GetId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(FlightDto request)
        {
            var result = _flightService.Create(request);
            return Ok(result);
        }
        [HttpPut]

        public IActionResult Edit(FlightDto request)
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
