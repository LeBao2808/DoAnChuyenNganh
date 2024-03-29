﻿using AirlineTickets.Model.Dto;
using AirlineTickets.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AirlineTickets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        ILoginService _iloginService;

        public AccountController(ILoginService iloginService)
        {
            _iloginService = iloginService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserModel login)
        {
            var result = await _iloginService.AuthenticateUser(login);

            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Regisger(UserModel login)
        {
            var result = await _iloginService.CreateUser(login);

            return Ok(result);
        }

    }
}
