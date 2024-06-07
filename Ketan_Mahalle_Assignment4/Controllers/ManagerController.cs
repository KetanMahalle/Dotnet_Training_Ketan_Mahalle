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
    public class ManagerController : Controller
    {
        private readonly ManagerServiceInterface _managerService;

        public ManagerController(ManagerServiceInterface managerService)
        {
            _managerService = managerService;
        }

        [HttpPost]
        //Register Manager
        public async Task<ActionResult<ManagerModel>> RegisterManager(ManagerModel managerModel)
        {
            //Checking For Exixting Account  
            var existingManager = await _managerService.GetManagerByEmail(managerModel.Email);
            if (existingManager != null)
            {
                return Ok("Email already exist");
            }
            var response = await _managerService.RegisterManager(managerModel);
            return response;
        }
        [HttpGet]
         //Get Manager by uid
        public async Task<ManagerModel> GetManagerByUId(string UId)
        {
            var response = await _managerService.GetManagerByUId(UId);
            return response;
        }

        [HttpPost]
        //Update Manager
        public async Task<ManagerModel> UpdateManager(ManagerModel managerModel)
        {
            var response = await _managerService.UpdateManager(managerModel);
            return response;
        }
        //Delete Manager

        [HttpPost]
        public async Task<string> DeleteManager(string UId)
        {
            var response = await _managerService.DeleteManager(UId);
            return response;
        }
    }
}
