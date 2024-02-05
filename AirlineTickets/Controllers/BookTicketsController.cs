using AirlineTickets.DAL.Contract;
using AirlineTickets.Model.Dto;
using AirlineTickets.Service.Contract;
using MayNghien.Models.Request.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirlineTickets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookTicketsController : Controller
    {
        private readonly IBookTicketsService _bookTicketsService;

        public BookTicketsController(IBookTicketsService ChucVuService)
        {
            _bookTicketsService = ChucVuService;
        }
        [HttpGet]
        public IActionResult GetAllChucVu()
        {
            var result = _bookTicketsService.GetAll();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _bookTicketsService.GetId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(BookTicketsDto request)
        {
            var result = _bookTicketsService.Create(request);
            return Ok(result);
        }
        [HttpPut]

        public IActionResult Edit(BookTicketsDto request)
        {
            var result = _bookTicketsService.Edit(request);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult Delete(Guid id)
        {

            var result = _bookTicketsService.Delete(id);

            return Ok(result);

        }
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchUser([FromBody] SearchRequest request)
        {
            var result = await _bookTicketsService.Search(request);

            return Ok(result);
        }



    }
}
