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
    public class CustomersController : Controller
    {
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersService ChucVuService)
        {
            _customersService = ChucVuService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _customersService.GetAll();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _customersService.GetId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(CustomersDto request)
        {
            var result = _customersService.Create(request);
            return Ok(result);
        }
        [HttpPut]

        public IActionResult Edit(CustomersDto request)
        {
            var result = _customersService.Edit(request);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult Delete(Guid id)
        {

            var result = _customersService.Delete(id);

            return Ok(result);

        }
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchUser([FromBody] SearchRequest request)
        {
            var result = await _customersService.Search(request);

            return Ok(result);
        }
        [HttpPost("Download")]
        public async Task<IActionResult> Dowloadexcel(SearchRequest request)
        {
            var ex = await _customersService.ExportToExcel(request);
            MemoryStream stream = new MemoryStream(ex);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SelectedRows.xlsx");
        }

    }
}
