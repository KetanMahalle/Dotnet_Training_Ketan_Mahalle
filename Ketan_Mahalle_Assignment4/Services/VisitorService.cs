using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visitor_Security_Clearance_System.Interfaces;
using Visitor_Security_Clearance_System.CosmosDB;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entities;
using Visitor_Security_Clearance_System.Common;

namespace Visitor_Security_Clearance_System.Services
{
    public class VisitorService : VisitorServiceInterface
    {
        public readonly ICosmosDBService _cosmosDBService;

        public VisitorService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        public async Task<VisitorModel> RegisterVisitor(VisitorModel visitorModel)
        {
            VisitorEntity visitor = new VisitorEntity();
            visitor.Name = visitorModel.Name;
            visitor.Email = visitorModel.Email;
            visitor.PhoneNumber = visitorModel.PhoneNumber;
            visitor.Address = visitorModel.Address;
            visitor.CompanyName = visitorModel.CompanyName;
            visitor.Purpose = visitorModel.Purpose;
            visitor.InTime = visitorModel.InTime;
            visitor.OutTime = visitorModel.OutTime;
            visitor.Status = visitorModel.Status;
            visitor.Role = visitorModel.Role;



            visitor.Intialize(true, Credentials.VisitorDocumentType, "Ketan", "Mahalle");

            var response = await _cosmosDBService.RegisterVisitor(visitor);

            var responseModel = new VisitorModel();
            responseModel.UId = response.UId;
            responseModel.Name = response.Name;
            responseModel.Email = response.Email;
            responseModel.PhoneNumber = response.PhoneNumber;
            responseModel.Address = response.Address;
            responseModel.CompanyName = response.CompanyName;
            responseModel.Purpose = response.Purpose;
            responseModel.InTime = response.InTime;
            responseModel.OutTime = response.OutTime;
            responseModel.Status = response.Status;
            responseModel.Role = response.Role;

            return responseModel;
        }

        //get visitor by uid 
        public async Task<VisitorModel> GetVisitorByUId(string UId)
        {
            var response = await _cosmosDBService.GetVisitorByUId(UId);

            var visitorModel = new VisitorModel();
            visitorModel.UId = response.UId;
            visitorModel.Name = response.Name;
            visitorModel.Email = response.Email;
            visitorModel.PhoneNumber = response.PhoneNumber;
            visitorModel.Address = response.Address;
            visitorModel.CompanyName = response.CompanyName;
            visitorModel.Purpose = response.Purpose;
            visitorModel.InTime = response.InTime;
            visitorModel.OutTime = response.OutTime;
            visitorModel.Status = response.Status;
            visitorModel.Role = response.Role;

            return visitorModel;
        }

        //get visitor by email
        public async Task<VisitorModel> GetVisitorByEmail(string email)
        {
            var visitor = await _cosmosDBService.GetVisitorByEmail(email);
            if (visitor != null)
            {
                var visitorModel = new VisitorModel
                {
                    UId = visitor.UId,
                    Name = visitor.Name,
                    Email = visitor.Email,
                    PhoneNumber = visitor.PhoneNumber,
                    Address = visitor.Address,
                    CompanyName = visitor.CompanyName,
                    Purpose = visitor.Purpose,
                    InTime = visitor.InTime,
                    OutTime = visitor.OutTime,
                    Status = visitor.Status,
                    Role = visitor.Role,
                };
                return visitorModel;
            }
            return null;
        }

       //update visitor
        public async Task<VisitorModel> UpdateVisitor(VisitorModel visitorModel)
        {
            var existingVisitor = await _cosmosDBService.GetVisitorByUId(visitorModel.UId);
            existingVisitor.Active = false;
            existingVisitor.Archived = true;
            await _cosmosDBService.ReplaceAsync(existingVisitor);

            existingVisitor.Intialize(false, Credentials.VisitorDocumentType, "Ketan", "Mahalle");



            existingVisitor.UId = visitorModel.UId;
            existingVisitor.Name = visitorModel.Name;
            existingVisitor.Email = visitorModel.Email;
            existingVisitor.PhoneNumber = visitorModel.PhoneNumber;
            existingVisitor.Address = visitorModel.Address;
            existingVisitor.CompanyName = visitorModel.CompanyName;
            existingVisitor.Purpose = visitorModel.Purpose;
            existingVisitor.InTime = visitorModel.InTime;
            existingVisitor.OutTime = visitorModel.OutTime;
            existingVisitor.Status = visitorModel.Status;
            existingVisitor.Role = visitorModel.Role;
            var response = await _cosmosDBService.RegisterVisitor(existingVisitor);

            var responseModel = new VisitorModel
            {
                UId = response.UId,
                Name = response.Name,
                Email = response.Email,
                PhoneNumber = response.PhoneNumber,
                Address = response.Address,
                CompanyName = response.CompanyName,
                Purpose = response.Purpose,
                InTime = response.InTime,
                OutTime = response.OutTime,
                Status = response.Status,
                Role = response.Role,

            };
            return responseModel;


        }

        //delete visitor
        public async Task<string> DeleteVisitor(string uId)
        {

            var visitor = await _cosmosDBService.GetVisitorByUId(uId);
            visitor.Active = false;
            visitor.Archived = true;
            await _cosmosDBService.ReplaceAsync(visitor);

            visitor.Intialize(false, Credentials.VisitorDocumentType, "Ketan", "Mahalle");
            visitor.Archived = true;
            var response = await _cosmosDBService.RegisterVisitor(visitor);

            return "Record has been deleted";

        }

        //get visitor by status
        public async Task<List<VisitorModel>> GetVisitorsByStatus(string status)
        {
            
            var visitor = await _cosmosDBService.GetVisitorsByStatus(status);

            var visitorlist = visitor.Select(visitor => new VisitorModel
            {
                UId = visitor.UId,
                Email = visitor.Email,
                PhoneNumber = visitor.PhoneNumber,
                Address = visitor.Address,
                CompanyName = visitor.CompanyName,
                Purpose = visitor.Purpose,
                InTime = visitor.InTime,
                OutTime = visitor.OutTime,
                Status = visitor.Status,
                Role = visitor.Role,



            }).ToList();

            return visitorlist;
        }
    }
}
