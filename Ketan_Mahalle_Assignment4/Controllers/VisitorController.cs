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
    public class VisitorController : Controller
    {
        private readonly VisitorServiceInterface _visitorService;

        public VisitorController(VisitorServiceInterface visitorService)
        {
            _visitorService = visitorService;
        }


        [HttpPost]


        //To Register Visitor
        public async Task<ActionResult<VisitorModel>> RegisterVisitor(VisitorModel visitorModel)
        {

            //Checking visitor already has account  
            var existingVisitor = await _visitorService.GetVisitorByEmail(visitorModel.Email);
            if (existingVisitor != null)
            {
                return Ok("Email already exist");
            }


            var response = await _visitorService.RegisterVisitor(visitorModel);
            return response;
        }


        [HttpGet]

        //Retriving by UID
        public async Task<VisitorModel> GetVisitorByUId(string UId)
        {
            var response = await _visitorService.GetVisitorByUId(UId);
            return response;
        }

        [HttpPost]

        //Update visitor
        public async Task<VisitorModel> UpdateVisitor(VisitorModel visitorModel)
        {
            var response = await _visitorService.UpdateVisitor(visitorModel);
            return response;
        }

        [HttpPost]

        //Delete visitor
        public async Task<string> DeleteVisitor(string UId)
        {
            var response = await _visitorService.DeleteVisitor(UId);
            return response;
        }

        [HttpGet]

        //Get visitor by Status
        public async Task<ActionResult<List<VisitorModel>>> GetVisitorByStatus(string status)
        {
            var visitors = await _visitorService.GetVisitorsByStatus(status);
            return Ok(visitors);
        }
    }
}
