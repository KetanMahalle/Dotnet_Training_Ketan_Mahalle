using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.Interfaces
{
    public interface SecurityServiceInterface
    {
        Task<SecurityModel> RegisterSecurity(SecurityModel securityModel);

        Task<SecurityModel> GetSecurityByEmail(string Email);

        Task<SecurityModel> GetSecurityByUId(string UId);

        Task<SecurityModel> UpdateSecurity(SecurityModel securityModel);

        Task<string> DeleteSecurity(string UId);

    }
}
