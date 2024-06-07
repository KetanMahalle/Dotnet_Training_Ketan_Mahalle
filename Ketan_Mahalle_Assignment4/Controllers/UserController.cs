using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visitor_Security_Clearance_System.Interfaces;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Services;

namespace Visitor_Security_Clearance_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AuthServiceInterface _authService;

        public UserController(AuthServiceInterface authService)
        {
            _authService = authService;
        }



        [HttpPost("login")]
        public async Task<ActionResult<LoginModel>> Login([FromBody] LoginModel loginRequest)
        {
            try
            {
                var userLoginDTO = await _authService.Authenticate(loginRequest.Email);

                if (userLoginDTO != null)
                {
                    // Checking Match
                    if (userLoginDTO.Role == loginRequest.Role)
                    {
                        return Ok(new { message = "Login successful" });
                    }
                    else
                    {

                        return Unauthorized(new { message = "Invalid role" });
                    }
                }
                else
                {
                    return Unauthorized(new { message = "Invalid credentials" });
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = "An error occurred " });
            }
        }

    }
}
