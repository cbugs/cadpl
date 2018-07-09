using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultix.ProCampaignAccount.Models
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public string LoginIssued{ get; set; }
        public DateTime LoginExpires{ get; set; }
        public DateTime TokenExpires{ get; set; }
        public string Token { get; set; }
        public bool IsLoggedIn{ get; set; }
        public string UserId{ get; set; }
        public int LoginStatus { get; set; }
        public bool IsNative{ get; set; }
        public int LoginVersion{ get; set; }
        public List<string> Roles { get; set; }
        public List<string> Permissions { get; set; }
    }
}
