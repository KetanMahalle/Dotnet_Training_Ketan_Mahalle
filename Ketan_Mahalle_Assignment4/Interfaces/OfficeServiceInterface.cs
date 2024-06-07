using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.Interfaces
{
    public interface OfficeServiceInterface
    {
        Task<OfficeModel> RegisterOffice(OfficeModel officeModel);

        Task<OfficeModel> GetOfficeByUId(string UId);

        Task<OfficeModel> UpdateOffice(OfficeModel officeModel);

        Task<string> DeleteOffice(string UId);

    }
}
