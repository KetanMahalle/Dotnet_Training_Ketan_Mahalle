using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visitor_Security_Clearance_System.Entities;
using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.CosmosDB
{
    public interface ICosmosDBService
    {
        //visitor
        Task<VisitorEntity> RegisterVisitor(VisitorEntity visitorEntity);

        Task<VisitorEntity> GetVisitorByEmail(string email);

        Task<VisitorEntity> GetVisitorByUId(string uId);

        Task ReplaceAsync(dynamic visitor);


        //Manager
        Task<ManagerEntity> RegisterManager(ManagerEntity managerEntity);

        Task<ManagerEntity> GetManagerByEmail(string email);

        Task<ManagerEntity> GetManagerByUId(string UId);

        //Office
        Task<OfficeEntity> RegisterOffice(OfficeEntity officeEntity);

        Task<OfficeEntity> GetOfficeByUId(string UId);



        //security
        Task<SecurityEntity> RegisterSecurity(SecurityEntity securityEntity);

        Task<SecurityEntity> GetSecurityByEmail(string email);

        Task<SecurityEntity> GetSecurityByUId(string uId);




        //login
        Task<LoginModel> GetUserByEmail(string email);

        //Pass
        Task<PassEntity> CreatePass(PassEntity passEntity);

        Task<PassEntity> GetPassByVisitorId(string visitorId);

        Task<List<VisitorEntity>> GetVisitorsByStatus(string status);

        Task<PassEntity> GetPassById(string uId);
        Task<PassEntity> UpdatePass(PassEntity passEntity);



    }
}
