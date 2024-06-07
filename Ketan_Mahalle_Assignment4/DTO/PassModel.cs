using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Visitor_Security_Clearance_System.DTO
{
    public class PassModel
    {
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "visitorID", NullValueHandling = NullValueHandling.Ignore)]
        public string VisitorID { get; set; }

        [JsonProperty(PropertyName = "officeID", NullValueHandling = NullValueHandling.Ignore)]
        public string OfficeID { get; set; }

        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "validFrom", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ValidFrom { get; set; }

        [JsonProperty(PropertyName = "validUntil", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ValidTill { get; set; }


        [JsonProperty(PropertyName = "issuedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string IssuedBy { get; set; }

        [JsonProperty(PropertyName = "createdDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "visitorEmail", NullValueHandling = NullValueHandling.Ignore)]
        public string VisitorEmail { get; set; }

    }
}
