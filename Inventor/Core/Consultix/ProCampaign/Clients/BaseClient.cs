using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Consultix.ProCampaign.Clients
{
    public abstract class BaseClient
    {
        private string _apiKey;
        public string ApiKey
        {
            get { return _apiKey; }
            set { _apiKey = value; }
        }

        private string _apiSecret;
        public string ApiSecret
        {
            get { return _apiSecret; }
            set { _apiSecret = value; }
        }

        public BaseClient(string key, string secret)
        {
            // TODO: Complete member initialization
            this._apiKey = key;
            this._apiSecret = secret;
        }

    }
}
