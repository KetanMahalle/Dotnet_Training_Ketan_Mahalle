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
    public class MemberController : Controller
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "Library";
        public string ContainerName = "Member";

        public Container container;

        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;


        }
        public MemberController()
        {
            container = GetContainer();
        }
        [HttpPost]
        public async Task<Models.MemberModel> AddMember(Models.MemberModel membermodel)
        {
            Entities.MemberEntity member = new Entities.MemberEntity();
            member.UId = membermodel.UId;
            member.Name = membermodel.Name;
            member.DateOfBirth = membermodel.DateOfBirth;
            member.Email = membermodel.Email;
           


            member.Id = Guid.NewGuid().ToString();
            member.UId = member.Id;
            member.DocumentType = "Member";
            member.CreatedBy = "Ketan";
            member.CreatedOn = DateTime.Now;
            member.UpdatedBy = "Ketan";
            member.UpdatedOn = DateTime.Now;
            member.Version = 1;
            member.Active = true;
            member.Archived = false;


            Entities.MemberEntity response = await container.CreateItemAsync(member);


            Models.MemberModel responseModel = new Models.MemberModel();
            responseModel.UId = response.UId;
            responseModel.Name = response.Name;
            responseModel.DateOfBirth = response.DateOfBirth;
            responseModel.Email = response.Email;
            return responseModel;

        }
        [HttpGet]
        public async Task<Models.MemberModel> GetMemberByUID(String uId)
        {
            var member = container.GetItemLinqQueryable<Models.MemberModel>(true).Where(q => q.UId == uId).FirstOrDefault();
            return member;
        }
        [HttpGet]
        public async Task<List<Models.MemberModel>> GetAllMembers()
        {
            var member = container.GetItemLinqQueryable<Models.MemberModel>(true).ToList();
            return member;
        }
        [HttpPost]
        public async Task<Models.MemberModel> UpdateMember(Models.MemberModel member)
        {

            var existingMember = container.GetItemLinqQueryable<Entities.MemberEntity>(true).Where(q => q.UId == member.UId && q.Active == true && q.Archived == false).FirstOrDefault();

            existingMember.Archived = true;
            existingMember.Active = false;
            await container.ReplaceItemAsync(existingMember, existingMember.Id);


            existingMember.UpdatedBy = "Ketan";
            existingMember.UpdatedOn = DateTime.Now;
            existingMember.Version = existingMember.Version + 1;
            existingMember.Active = true;
            existingMember.Archived = false;

            existingMember.Name = member.Name;
            existingMember.DateOfBirth = member.DateOfBirth;
            existingMember.Email = member.Email;


            existingMember = await container.CreateItemAsync(existingMember);

            //6. Return

            Models.MemberModel response = new Models.MemberModel();
            response.UId = existingMember.UId;
            response.Name = existingMember.Name;
            response.DateOfBirth = existingMember.DateOfBirth;
            response.Email = existingMember.Email;
            return response;

        }







    }

}
