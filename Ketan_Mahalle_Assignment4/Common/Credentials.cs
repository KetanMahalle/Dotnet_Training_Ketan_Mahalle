using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Visitor_Security_Clearance_System.Common
{
    public class Credentials
    {
        //Setting from launchsettings
        public static readonly string databaseName = Environment.GetEnvironmentVariable("databaseName");
        public static readonly string containerName = Environment.GetEnvironmentVariable("containerName");
        public static readonly string CosmosEndPoint = Environment.GetEnvironmentVariable("cosmosUrl");
        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");

        //Declaring Document Types
        public static readonly string VisitorDocumentType = "visitor";
        public static readonly string SecurityDocumentType = "security";
        public static readonly string ManagerDocumentType = "manager";
        public static readonly string OfficeDocumentType = "office";
        public static readonly string PassDocumentType = "pass";
    }
}
