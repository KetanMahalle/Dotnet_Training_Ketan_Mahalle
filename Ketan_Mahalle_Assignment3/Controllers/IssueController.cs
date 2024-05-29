using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IssueController : Controller
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "Library";
        public string ContainerName = "Issue";

        public Container container;

        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;


        }
        public IssueController()
        {
            container = GetContainer();
        }

        [HttpPost]
        public async Task<Models.IssueModel> IssueBook(Models.IssueModel issueModel)
        {

            Entities.IssueBookEntity issue = new Entities.IssueBookEntity();
            issue.UId = issueModel.UId;
            issue.IssueDate = issueModel.IssueDate;
            issue.MemberId = issueModel.MemberId;
            issue.ReturnDate = issueModel.ReturnDate;
            issue.isReturned = issueModel.isReturned;


            issue.Id = Guid.NewGuid().ToString();
            issue.UId = issue.Id;
            issue.DocumentType = "Issue";
            issue.CreatedBy = "Ketan";
            issue.CreatedOn = DateTime.Now;
            issue.UpdatedBy = "Ketan";
            issue.UpdatedOn = DateTime.Now;
            issue.Version = 1;
            issue.Active = true;
            issue.Archived = false;

            
            Entities.IssueBookEntity response = await container.CreateItemAsync(issue);

            
            Models.IssueModel responseModel = new Models.IssueModel();
            responseModel.UId = response.UId;
            responseModel.MemberId = response.MemberId;
            responseModel.IssueDate = response.IssueDate;
            responseModel.ReturnDate = response.ReturnDate;
            responseModel.isReturned = response.isReturned;
            return responseModel;

        }
        [HttpGet]
        public async Task<Models.IssueModel> GetIssuebyUID(String uId)
        {
            var issue = container.GetItemLinqQueryable<Models.IssueModel>(true).Where(q => q.UId == uId).FirstOrDefault();
            return issue;
        }
        [HttpPost]
        public async Task<Models.IssueModel> UpdateIssueBook(Models.IssueModel issue)
        {

            var existingMember = container.GetItemLinqQueryable<Entities.IssueBookEntity>(true).Where(q => q.UId == issue.UId && q.Active == true && q.Archived == false).FirstOrDefault();

            existingMember.Archived = true;
            existingMember.Active = false;
            await container.ReplaceItemAsync(existingMember, existingMember.Id);


            existingMember.UpdatedBy = "Ketan";
            existingMember.UpdatedOn = DateTime.Now;
            existingMember.Version = existingMember.Version + 1;
            existingMember.Active = true;
            existingMember.Archived = false;

            existingMember.BookId = issue.BookId;
            existingMember.MemberId = issue.MemberId;
            existingMember.IssueDate = issue.IssueDate;
            existingMember.ReturnDate = issue.ReturnDate;
            existingMember.isReturned = issue.isReturned;

            existingMember = await container.CreateItemAsync(existingMember);

            

            Models.IssueModel response = new Models.IssueModel();
            response.UId = existingMember.UId;
            response.BookId = existingMember.BookId;
            response.MemberId = existingMember.MemberId;
            response.IssueDate = existingMember.IssueDate;
            response.ReturnDate = existingMember.ReturnDate;
            response.isReturned = existingMember.isReturned;
            return response;

        }


    }
}
