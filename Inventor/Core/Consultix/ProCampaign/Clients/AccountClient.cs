using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Consultix.ProCampaign.Clients
{
    public class AccountClient : BaseClient, IAccountClient
    {
        public AccountClient(string key, string secret)
            : base(key, secret)
        {

        }
    }
}
