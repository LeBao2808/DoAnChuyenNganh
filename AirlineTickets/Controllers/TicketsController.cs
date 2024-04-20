using AirlineTickets.Model.Dto;
using AirlineTickets.Service.Contract;
using MayNghien.Models.Request.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirlineTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TicketsController : Controller
    {
        private readonly ITicketsService _partnerService;
        public TicketsController(ITicketsService ChucVuService)
        {
            _partnerService = ChucVuService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _partnerService.GetAll();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _partnerService.GetId(id);
            return Ok(result);
        }
        [HttpGet]
        [Route("IdTicket/{flightsId}")]
        public IActionResult Getidticket(Guid flightsId)
        {
            var result = _partnerService.GetIdTicket(flightsId);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(TicketsDto request)
        {
            var result = _partnerService.Create(request);
            return Ok(result);
        }
        [HttpPut]

        public IActionResult Edit(TicketsDto request)
        {
            var result = _partnerService.Edit(request);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult Delete(Guid id)
        {

            var result = _partnerService.Delete(id);

            return Ok(result);

        }
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchUser([FromBody] SearchRequest request)
        {
            var result = await _partnerService.Search(request);

            return Ok(result);
        }
    }
}
