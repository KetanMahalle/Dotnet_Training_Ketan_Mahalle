using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SendGrid;
using SendGrid.Helpers.Mail;
using Visitor_Security_Clearance_System.Interfaces;
using Visitor_Security_Clearance_System.CosmosDB;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entities;
using Visitor_Security_Clearance_System.Common;
using Document = iTextSharp.text.Document;

namespace Visitor_Security_Clearance_System.Services
{
    public class PassService : PassServiceInterface
    {
        private readonly ICosmosDBService _cosmosDBService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PassService(ICosmosDBService cosmosDBService, IWebHostEnvironment webHostEnvironment)
        {
            _cosmosDBService = cosmosDBService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<PassModel> CreatePass(PassModel passModel)
        {
            // Creating pass entity
            PassEntity passEntity = new PassEntity
            {
                UId = passModel.UId,
                VisitorID = passModel.VisitorID,
                OfficeID = passModel.OfficeID,
                Status = passModel.Status,
                ValidFrom = passModel.ValidFrom,
                ValidTill = passModel.ValidTill,
                IssuedBy = passModel.IssuedBy,
                CreatedOn = passModel.CreatedOn
            };

            passEntity.Intialize(true, Credentials.PassDocumentType, "Ketan", "Mahalle");

            var response = await _cosmosDBService.CreatePass(passEntity);

            // Generating pass in pdf format
            byte[] pdfBytes = GeneratePassPDF(passModel);

            // Send pass by email
            await SendPassEmail(passModel.VisitorEmail, pdfBytes);

            // Mapping response 
            var responseModel = new PassModel
            {
                UId = response.UId,
                VisitorID = response.VisitorID,
                OfficeID = response.OfficeID,
                Status = response.Status,
                ValidFrom = response.ValidFrom,
                ValidTill = response.ValidTill,
                IssuedBy = response.IssuedBy,
                CreatedOn = response.CreatedOn
            };

            return responseModel;
        }

        private byte[] GeneratePassPDF(PassModel passModel)
        {
            // Generating PDF 
            byte[] pdfBytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                document.Add(new Paragraph("Visitor Pass"));
                document.Add(new Paragraph($"Visitor ID: {passModel.VisitorID}"));
                document.Add(new Paragraph($"Office ID: {passModel.OfficeID}"));
                document.Add(new Paragraph($"Status: {passModel.Status}"));
                document.Add(new Paragraph($"Valid From: {passModel.ValidFrom}"));
                document.Add(new Paragraph($"Valid Until: {passModel.ValidTill}"));

                document.Close();
                pdfBytes = memoryStream.ToArray();
            }
            return pdfBytes;
        }

        //send pass to email
        private async Task SendPassEmail(string recipientEmail, byte[] pdfBytes)
        {
            string apiKey = "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"; 

            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage
            {
                From = new EmailAddress("tidkeshubham10@gmail.com", "ShubhamTidke"),
                Subject = "Visitor Pass",
                HtmlContent = "Please collect your pass.",
                PlainTextContent = " "
            };
            msg.AddTo(new EmailAddress(recipientEmail));
            msg.AddAttachment("Visitor_Pass.pdf", Convert.ToBase64String(pdfBytes), "application/pdf", "attachment");

            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response);
            Console.WriteLine(response.ToString());
        }

        //get pass by uid
        public async Task<PassModel> GetPassById(string uId)
        {
            var pass = await _cosmosDBService.GetPassById(uId);

            var passModel = new PassModel
            {
                UId = pass.UId,
                Status = pass.Status
            };

            return passModel;
        }


        //update pass status
        public async Task<PassModel> UpdatePassStatus(string uId, string newStatus)
        {
            var existingPass = await _cosmosDBService.GetPassById(uId);

            if (existingPass == null)
            {
                return null;
            }

            existingPass.Status = newStatus;

            var updatedPass = await _cosmosDBService.UpdatePass(existingPass);

            var responseModel = new PassModel
            {
                UId = updatedPass.UId,
                VisitorID = updatedPass.VisitorID,
                OfficeID = updatedPass.OfficeID,
                ValidFrom = updatedPass.ValidFrom,
                ValidTill = updatedPass.ValidTill,
                IssuedBy = updatedPass.IssuedBy,
                CreatedOn = updatedPass.CreatedOn,
                Status = updatedPass.Status,
            };

            return responseModel;
        }
    }
}
