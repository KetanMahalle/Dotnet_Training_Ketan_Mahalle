using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visitor_Security_Clearance_System.Common;

namespace Visitor_Security_Clearance_System.Entities
{
    public class VisitorEntity : BaseEntity
    {


        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "companyName", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyName { get; set; }

        [JsonProperty(PropertyName = "phoneNumber", NullValueHandling = NullValueHandling.Ignore)]
        public long PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "purpose", NullValueHandling = NullValueHandling.Ignore)]
        public string Purpose { get; set; }

        [JsonProperty(PropertyName = "inTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime InTime { get; set; }

        [JsonProperty(PropertyName = "outTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime OutTime { get; set; }

        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }


    }
}
