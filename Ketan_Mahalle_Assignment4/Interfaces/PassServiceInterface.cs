using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.Interfaces
{
    public interface PassServiceInterface
    {
        Task<PassModel> CreatePass(PassModel passModel);
        Task<PassModel> GetPassById(string uId);
        Task<PassModel> UpdatePassStatus(string uId, string newStatus);

    }
}
