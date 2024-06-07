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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : Controller
    {
        private readonly SecurityServiceInterface _securityService;

        public SecurityController(SecurityServiceInterface securityService)
        {
            _securityService = securityService;
        }
        [HttpPost]
        //Register
        public async Task<ActionResult<SecurityModel>> RegisterSecurity(SecurityModel securityModel)
        {

            var existingSecurity = await _securityService.GetSecurityByEmail(securityModel.Email);
            if (existingSecurity != null)
            {
                return Ok("Email already exist");
            }
            var response = await _securityService.RegisterSecurity(securityModel);
            return response;
        }
        [HttpGet]
        //Get
        public async Task<SecurityModel> GetSecurityByUId(string UId)
        {
            var response = await _securityService.GetSecurityByUId(UId);
            return response;
        }

        [HttpPost]
        //update
        public async Task<SecurityModel> UpdateSecurity(SecurityModel securityModel)
        {
            var response = await _securityService.UpdateSecurity(securityModel);
            return response;
        }

        [HttpPost]
        //delete
        public async Task<string> DeleteSecurity(string UId)
        {
            var response = await _securityService.DeleteSecurity(UId);
            return response;
        }

    }
}
