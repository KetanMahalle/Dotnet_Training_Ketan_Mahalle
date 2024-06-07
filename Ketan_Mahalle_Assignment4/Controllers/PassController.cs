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
    public class PassController : Controller
    {
        private readonly PassServiceInterface _passService;

        public PassController(PassServiceInterface passService)
        {
            _passService = passService;
        }

        [HttpPost]
        //Creating Pass
        public async Task<PassModel> CreatePass(PassModel passModel)
        {

            var response = await _passService.CreatePass(passModel);
            return response;
        }



        [HttpPost("{uId}/status")]
        //Update pass if Exist
        public async Task<ActionResult<PassModel>> UpdatePassStatus(string uId, [FromBody] string newStatus)
        {
            var existingPass = await _passService.GetPassById(uId);

            if (existingPass == null)
            {
                return NotFound("Pass not found");
            }

            existingPass.Status = newStatus;

            var updatedPass = await _passService.UpdatePassStatus(uId, newStatus);

            return Ok(updatedPass);
        }
    }
}
