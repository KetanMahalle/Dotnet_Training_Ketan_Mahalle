using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visitor_Security_Clearance_System.Interfaces;
using Visitor_Security_Clearance_System.CosmosDB;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entities;
using Visitor_Security_Clearance_System.Common;
using Microsoft.AspNetCore.Mvc;

namespace Visitor_Security_Clearance_System.Services
{
    public class OfficeService : OfficeServiceInterface
    {
        public readonly ICosmosDBService _cosmosDBService;

        public OfficeService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }
        [HttpPost]
        public async Task<OfficeModel> RegisterOffice(OfficeModel officeModel)
        {
            OfficeEntity office = new OfficeEntity();
            office.UId = officeModel.UId;
            office.Name = officeModel.Name;
            office.Location = officeModel.Location;
            office.ManagerId = officeModel.ManagerId;


            office.Intialize(true, Credentials.OfficeDocumentType, "Ketan", "Mahalle");


            var response = await _cosmosDBService.RegisterOffice(office);

            var responseModel = new OfficeModel();
            responseModel.UId = response.UId;
            responseModel.Name = response.Name;
            responseModel.Location = response.Location;
            responseModel.ManagerId = response.ManagerId;


            return responseModel;
        }

        //get office by uid
        public async Task<OfficeModel> GetOfficeByUId(string UId)
        {
            var response = await _cosmosDBService.GetOfficeByUId(UId);

            var officeModel = new OfficeModel();
            officeModel.UId = response.UId;
            officeModel.Name = response.Name;
            officeModel.Location = response.Location;
            officeModel.ManagerId = response.ManagerId;


            return officeModel;
        }

        //update office
        public async Task<OfficeModel> UpdateOffice(OfficeModel officeModel)
        {
            var existingOffice = await _cosmosDBService.GetOfficeByUId(officeModel.UId);
            existingOffice.Active = false;
            existingOffice.Archived = true;
            await _cosmosDBService.ReplaceAsync(existingOffice);

            existingOffice.Intialize(false, Credentials.OfficeDocumentType, "Ketan", "Mahalle");



            existingOffice.UId = officeModel.UId;
            existingOffice.Name = officeModel.Name;
            existingOffice.Location = officeModel.Location;
            existingOffice.ManagerId = officeModel.ManagerId;

            var response = await _cosmosDBService.RegisterOffice(existingOffice);

            var responseModel = new OfficeModel
            {
                UId = response.UId,
                Name = response.Name,
                Location = officeModel.Location,
                ManagerId = officeModel.ManagerId,
            };
            return responseModel;


        }


        //delete office
        public async Task<string> DeleteOffice(string uId)
        {
            var office = await _cosmosDBService.GetOfficeByUId(uId);
            office.Active = false;
            office.Archived = true;
            await _cosmosDBService.ReplaceAsync(office);

            office.Intialize(false, Credentials.OfficeDocumentType, "Ketan", "Mahalle");
            office.Archived = true;


            var response = await _cosmosDBService.RegisterOffice(office);

            return "Record has been deleted";

        }
    }
}
