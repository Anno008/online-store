﻿using System.Threading.Tasks;
using Backend.WebApi.DTOs.RequestDTOs;
using Backend.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService) =>
            _authService = authService;

        [HttpPost]
        [ActionName("auth")]
        public async Task<IActionResult> Auth([FromBody] AuthRequestDTO authDto)
        {
            if (authDto.GrantType == "password")
            {
                var retVal = await _authService.Login(authDto);
                if (retVal == null)
                    return BadRequest("Incorrect credentials.");

                return Ok(retVal);
            }

            var result = _authService.RefreshAccessToken(authDto);
            if (result == null)
                return BadRequest("Couldn't refresh access token");

            return Ok(result);
        }

        [HttpPost]
        [ActionName("register")]
        public IActionResult Register([FromBody] RegisterRequestDTO registerDto)
        {
            if (registerDto == null)
                return BadRequest();

            var result = _authService.Register(registerDto);
            if (result != null)
                return Ok(result);

            return BadRequest("The user with the given username already exists.");
        }
    }
}
