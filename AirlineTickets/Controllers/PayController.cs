using AirlineTickets.Model.Dto;
using AirlineTickets.Service.Contract;
using MayNghien.Models.Request.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirlineTickets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayController : Controller
    {
        private readonly IPayService _payService;
        public PayController(IPayService ChucVuService)
        {
           _payService = ChucVuService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _payService.GetAll();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _payService.GetId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(PayDto request)
        {
            var result = _payService.Create(request);
            return Ok(result);
        }
        [HttpPut]

        public IActionResult Edit(PayDto request)
        {
            var result = _payService.Edit(request);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult Delete(Guid id)
        {

            var result = _payService.Delete(id);

            return Ok(result);

        }
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchUser([FromBody] SearchRequest request)
        {
            var result = await _payService.Search(request);

            return Ok(result);
        }
    }
}
