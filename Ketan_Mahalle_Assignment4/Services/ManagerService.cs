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
    public class ManagerService : ManagerServiceInterface
    {
        public readonly ICosmosDBService _cosmosDBService;

        public ManagerService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        [HttpPost]

        //Register
        public async Task<ManagerModel> RegisterManager(ManagerModel managerModel)
        {
            ManagerEntity manager = new ManagerEntity();
            manager.UId = managerModel.UId;
            manager.Name = managerModel.Name;
            manager.Email = managerModel.Email;
            manager.PhoneNumber = managerModel.PhoneNumber;
            manager.Role = managerModel.Role;


            manager.Intialize(true, Credentials.ManagerDocumentType, "Ketan", "Mahalle");


            var response = await _cosmosDBService.RegisterManager(manager);

            var responseModel = new ManagerModel();
            responseModel.UId = response.UId;
            responseModel.Name = response.Name;
            responseModel.Email = response.Email;
            responseModel.PhoneNumber = response.PhoneNumber;
            responseModel.Role = response.Role;


            return responseModel;
        }

        //get by uid
        public async Task<ManagerModel> GetManagerByUId(string UId)
        {
            var response = await _cosmosDBService.GetManagerByUId(UId);

            var managerModel = new ManagerModel();
            managerModel.UId = response.UId;
            managerModel.Name = response.Name;
            managerModel.Email = response.Email;
            managerModel.PhoneNumber = response.PhoneNumber;
            managerModel.Role = response.Role;


            return managerModel;
        }

        //get manager by email
        public async Task<ManagerModel> GetManagerByEmail(string email)
        {
            var manager = await _cosmosDBService.GetManagerByEmail(email);
            if (manager != null)
            {
                var managerModel = new ManagerModel
                {
                    UId = manager.UId,
                    Name = manager.Name,
                    Email = manager.Email,
                    PhoneNumber = manager.PhoneNumber,
                    Role = manager.Role,
                };
                return managerModel;
            }
            return null;
        }

       

        //update manager
        public async Task<ManagerModel> UpdateManager(ManagerModel managerModel)
        {
            var existingManager = await _cosmosDBService.GetManagerByUId(managerModel.UId);
            existingManager.Active = false;
            existingManager.Archived = true;
            await _cosmosDBService.ReplaceAsync(existingManager);

            existingManager.Intialize(false, Credentials.ManagerDocumentType, "Ketan", "Mahalle");



            existingManager.UId = managerModel.UId;
            existingManager.Name = managerModel.Name;
            existingManager.Email = managerModel.Email;
            existingManager.PhoneNumber = managerModel.PhoneNumber;
            existingManager.Role = managerModel.Role;

            var response = await _cosmosDBService.RegisterManager(existingManager);

            var responseModel = new ManagerModel
            {
                UId = response.UId,
                Name = response.Name,
                Email = response.Email,
                PhoneNumber = response.PhoneNumber,
                Role = response.Role,


            };
            return responseModel;


        }

        //delete manager
        public async Task<string> DeleteManager(string uId)
        {
            var manager = await _cosmosDBService.GetManagerByUId(uId);
            manager.Active = false;
            manager.Archived = true;
            await _cosmosDBService.ReplaceAsync(manager);

            manager.Intialize(false, Credentials.ManagerDocumentType, "Ketan", "Mahalle");
            manager.Archived = true;

            var response = await _cosmosDBService.RegisterManager(manager);

            return "Record has been deleted.";

        }
    }
}
