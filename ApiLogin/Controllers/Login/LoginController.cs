using ApiLogin.Models.Request.Login;
using ApiLogin.Services.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ApiLogin.Controllers.Login
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginApiClient _loginApiClient;

        public LoginController(ILoginApiClient loginApiClient)
        {
            _loginApiClient = loginApiClient;
        }

        [HttpPost("login")]
        public async Task<IActionResult> AutenticateUser([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _loginApiClient.PostLogin(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Manejo de errores general
                return StatusCode(500,ex.Message);
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _loginApiClient.PostUser(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Manejo de errores general
                return StatusCode(500, ex.Message);
            }
        }
    }
}
