using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultix.ProCampaignAccount.Models
{
    public class ConsumerServiceResponse<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public T Data { get; set; }
    }
}
