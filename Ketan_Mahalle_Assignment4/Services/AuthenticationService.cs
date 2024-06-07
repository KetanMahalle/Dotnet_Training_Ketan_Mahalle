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
    public class AuthenticationService : AuthServiceInterface
    {
        public readonly ICosmosDBService _cosmosDBService;

        public AuthenticationService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }
        public async Task<LoginModel> Authenticate(string Email)
        {
            var response = await _cosmosDBService.GetUserByEmail(Email);
            return response;
        }

    }
}
