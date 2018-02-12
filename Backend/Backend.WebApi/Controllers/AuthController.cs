﻿using Microsoft.AspNetCore.Mvc;
using Backend.WebApi.DTOs;
using Backend.WebApi.Services;
using System.Threading.Tasks;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly AuthService authService;

        public AuthController(AuthService authService) =>
            this.authService = authService;
        
        [HttpPost]
        [ActionName("auth")]
        public async Task<IActionResult> Auth(AuthDTO authDto)
        {
            if (authDto.GrantType == "password")
            {
                var re = await authService.Login(authDto);
                if (re == null)
                    return BadRequest("Incorrect credentials.");

                return Ok(re);
            }

            var result = authService.RefreshAccessToken(authDto);
            if (result == null)
                return BadRequest("Couldn't refresh access token");

            return Ok(result);
        }

        [HttpPost]
        [ActionName("register")]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            if (registerDTO == null)
                return BadRequest();

            var result = authService.Register(registerDTO);
            if (result != null)
                return Ok(result);

            return BadRequest("The user with the given username already exists.");
        }
    }
}