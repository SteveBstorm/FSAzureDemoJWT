using DemoJWT.Models;
using DemoJWT.Services;
using DemoJWT.TokenTools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoJWT.Entities;
using Microsoft.AspNetCore.Authorization;

namespace DemoJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private ITokenManager _tokenManager;
        public UserController(IUserService us, ITokenManager tm)
        {
            _userService = us;
            _tokenManager = tm;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(UserLogin ul)
        {
            if (ul is null) return BadRequest("user null");

            User user = _tokenManager.Authentitcate(_userService.Login(ul.Email, ul.Password));

            if (user is null)
                return new ForbidResult("interdit");

            return Ok(user);
        }

        
        [HttpGet]
        [Authorize("user")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        
        [HttpGet("{id}")]
        [Authorize("admin")]
        public IActionResult Get(int id)
        {
            return Ok(_userService.GetAll().Where(x => x.Id == id));
        }
    }
}
