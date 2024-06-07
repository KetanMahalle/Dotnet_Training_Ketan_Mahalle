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
    public class OfficeController : Controller
    {
        private readonly OfficeServiceInterface _officeService;

        public OfficeController(OfficeServiceInterface officeService)
        {
            _officeService = officeService;
        }

        [HttpPost]
        // Register Office
        public async Task<OfficeModel> RegisterOffice(OfficeModel officeModel)
        {

            var response = await _officeService.RegisterOffice(officeModel);
            return response;
        }
        [HttpGet]
        // Get Office By Uid
        public async Task<OfficeModel> GetOfficeByUId(string UId)
        {
            var response = await _officeService.GetOfficeByUId(UId);
            return response;
        }

        [HttpPost]
        // Update Office
        public async Task<OfficeModel> UpdateOffice(OfficeModel officeModel)
        {
            var response = await _officeService.UpdateOffice(officeModel);
            return response;
        }

        [HttpPost]
        // Delete Office
        public async Task<string> DeleteOffice(string UId)
        {
            var response = await _officeService.DeleteOffice(UId);
            return response;
        }
    }
}
