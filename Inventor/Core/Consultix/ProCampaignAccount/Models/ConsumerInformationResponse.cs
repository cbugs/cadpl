using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultix.ProCampaignAccount.Models
{
    public class ConsumerInformationResponse
    {
        public List<RequestModelBase.Attribute> Attributes { get; set; }
        public List<RequestModelBase.Transaction> Transactions { get; set; }
        public string Source { get; set; }
        public string UID { get; set; }
    }
}
