using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultix.ProCampaignAccount.Models
{
    public class ResponseMapping<T>
    {
        public T Data { get; set; }
        public string JobId { get; set; }
        public int HttpStatusCode { get; set; }
        public string HttpStatusMessage { get; set; }
        public int StatusCode{ get; set; }
        public string StatusMessage{ get; set; }
        public bool IsSuccessful { get; set; }
    }
}
