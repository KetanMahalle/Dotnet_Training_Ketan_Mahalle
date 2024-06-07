using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.Interfaces
{
    public interface VisitorServiceInterface
    {

        Task<VisitorModel> GetVisitorByEmail(string Email);

        Task<VisitorModel> RegisterVisitor(VisitorModel visitorModel);

        Task<VisitorModel> GetVisitorByUId(string UId);


        Task<VisitorModel> UpdateVisitor(VisitorModel visitorModel);


        Task<string> DeleteVisitor(string uId);

        Task<List<VisitorModel>> GetVisitorsByStatus(string status);
    }
}
