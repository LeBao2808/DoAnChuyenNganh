﻿using AirlineTickets.Model.Dto;
using AirlineTickets.Service.Contract;
using MayNghien.Models.Request.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirlineTickets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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


    }
}
