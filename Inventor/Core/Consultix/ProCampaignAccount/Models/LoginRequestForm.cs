using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultix.ProCampaignAccount.Models
{
    public class LoginRequestForm : RequestModelBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AntiForgeryToken { get;set; }
        public bool Remember { get; set; }
    }
}
