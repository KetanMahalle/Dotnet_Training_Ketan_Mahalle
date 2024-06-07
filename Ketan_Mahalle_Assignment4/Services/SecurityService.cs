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
    public class SecurityService : SecurityServiceInterface
    {
        public readonly ICosmosDBService _cosmosDBService;

        public SecurityService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }
        [HttpPost]
        public async Task<SecurityModel> RegisterSecurity(SecurityModel securityModel)
        {
            SecurityEntity security = new SecurityEntity();
            security.Name = securityModel.Name;
            security.Email = securityModel.Email;
            security.PhoneNumber = securityModel.PhoneNumber;
            security.Role = securityModel.Role;


            security.Intialize(true, Credentials.SecurityDocumentType, "Ketan", "Mahalle");


            var response = await _cosmosDBService.RegisterSecurity(security);

            var responseModel = new SecurityModel();
            responseModel.UId = response.UId;
            responseModel.Name = response.Name;
            responseModel.Email = response.Email;
            responseModel.PhoneNumber = response.PhoneNumber;
            responseModel.Role = response.Role;


            return responseModel;
        }

        //get security by email
        public async Task<SecurityModel> GetSecurityByEmail(string email)
        {
            var security = await _cosmosDBService.GetSecurityByEmail(email);
            if (security != null)
            {
                var securityModel = new SecurityModel
                {
                    UId = security.UId,
                    Name = security.Name,
                    Email = security.Email,
                    PhoneNumber = security.PhoneNumber,
                    Role = security.Role,
                };
                return securityModel;
            }
            return null;
        }
        //get security by uid
        public async Task<SecurityModel> GetSecurityByUId(string UId)
        {
            var response = await _cosmosDBService.GetSecurityByUId(UId);

            var securityModel = new SecurityModel();
            securityModel.UId = response.UId;
            securityModel.Name = response.Name;
            securityModel.Email = response.Email;
            securityModel.PhoneNumber = response.PhoneNumber;
            securityModel.Role = response.Role;


            return securityModel;
        }

        //update security
        public async Task<SecurityModel> UpdateSecurity(SecurityModel securityModel)
        {
            var existingSecurity = await _cosmosDBService.GetSecurityByUId(securityModel.UId);
            existingSecurity.Active = false;
            existingSecurity.Archived = true;
            await _cosmosDBService.ReplaceAsync(existingSecurity);

            existingSecurity.Intialize(false, Credentials.SecurityDocumentType, "Ketan", "Mahalle");



            existingSecurity.UId = securityModel.UId;
            existingSecurity.Name = securityModel.Name;
            existingSecurity.Email = securityModel.Email;
            existingSecurity.PhoneNumber = securityModel.PhoneNumber;

            var response = await _cosmosDBService.RegisterSecurity(existingSecurity);

            var responseModel = new SecurityModel
            {
                UId = response.UId,
                Name = response.Name,
                Email = response.Email,
                PhoneNumber = response.PhoneNumber,
                Role = response.Role,


            };
            return responseModel;


        }

        //delete security
        public async Task<string> DeleteSecurity(string uId)
        {
            var security = await _cosmosDBService.GetSecurityByUId(uId);
            security.Active = false;
            security.Archived = true;
            await _cosmosDBService.ReplaceAsync(security);

            security.Intialize(false, Credentials.SecurityDocumentType, "Ketan", "Mahalle");
            security.Archived = true;



            var response = await _cosmosDBService.RegisterSecurity(security);

            return "Record has been deleted";

        }
    }
}
