using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visitor_Security_Clearance_System.Common;

namespace Visitor_Security_Clearance_System.Entities
{
    public class OfficeEntity : BaseEntity
    {
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "location", NullValueHandling = NullValueHandling.Ignore)]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "managerId", NullValueHandling = NullValueHandling.Ignore)]
        public string ManagerId { get; set; }
    }
}
