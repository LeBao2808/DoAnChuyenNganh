﻿using AirlineTickets.Model.Dto;
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
    public class PromotionsController : Controller
    {
        private readonly IPromotionsService _promotionService;
        public PromotionsController(IPromotionsService ChucVuService)
        {
            _promotionService = ChucVuService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _promotionService.GetAll();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _promotionService.GetId(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(PromotionsDto request)
        {
            var result = _promotionService.Create(request);
            return Ok(result);
        }
        [HttpPut]

        public IActionResult Edit(PromotionsDto request)
        {
            var result = _promotionService.Edit(request);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult Delete(Guid id)
        {

            var result = _promotionService.Delete(id);

            return Ok(result);

        }
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchUser([FromBody] SearchRequest request)
        {
            var result = await _promotionService.Search(request);

            return Ok(result);
        }
    }
}
