using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.Interfaces
{
    public interface ManagerServiceInterface
    {
        Task<ManagerModel> GetManagerByEmail(string Email);
        Task<ManagerModel> RegisterManager(ManagerModel managerModel);

        Task<ManagerModel> GetManagerByUId(string UId);

        Task<ManagerModel> UpdateManager(ManagerModel managerModel);

        Task<string> DeleteManager(string UId);

    }
}
