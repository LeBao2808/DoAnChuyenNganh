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
    public class UserManagementController : Controller
    {
       private readonly IUserService _userManagementService;
        public UserManagementController(IUserService userManagementService)
        {
            _userManagementService = userManagementService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userManagementService.GetAllUser();
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> RestPassWordUser(UserModel Id)
        {
            var result = await _userManagementService.Password(Id);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserModel request)
        {
            var result = await _userManagementService.CreateUser(request);

            return Ok(result);
        }
        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var result = await _userManagementService.DeleteUser(Id);

            return Ok(result);
        }
        //[HttpGet]
        //[Route("{id}")]
        //public async Task<IActionResult> GetUser(string id)
        //{
        //    var result = await _userManagementService.GetUser(id);

        //    return Ok(result);
        //}
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Search([FromBody] SearchRequest request)
        {
            var result = await _userManagementService.Search(request);

            return Ok(result);
        }
        //[HttpPut]
        //[Route("{id}")]
        //public async Task<IActionResult> EditUser([FromBody] UserModel request)
        //{
        //    var result = await _userManagementService.EditUser(request);

        //    return Ok(result);
        //}
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserIdentity(string id)
        {
            var result = await _userManagementService.GetUserIdentity(id);

            return Ok(result);
        }
        //[HttpPut]
        //[Route("{id}")]
        //public async Task<IActionResult> Xacthucs(string? id)
        //{
        //    var resut = await _userManagementService.XacThuc(id);
        //    return Ok(resut);
        //}
    }
}
