using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Consultix.ProCampaign.Clients;
using Consultix.ProCampaign.Configuration;

namespace Consultix.ProCampaign
{
    public class ProCampaignService
    {
        private string _apiKey;

        public string APIKey
        {
            get { return _apiKey; }
        }

        private string _apiSecret;

        public string APISecret
        {
            get { return _apiSecret; }
        }

        private ConsumerClient _consumerClient;

        public ConsumerClient ConsumerClient
        {
            get { return _consumerClient; }
            set { _consumerClient = value; }
        }

        private static readonly object Padlock = new object();

        internal ProCampaignService()
        {

        }

        internal ProCampaignService(string locale, string environement = null)
        {
            var key = ProCampaignKeyRepository.GetApiKey(locale, environement);
            if (key == null)
            {
                throw new Exception("Configuration failed Key not found");
            }

            var secret = ProCampaignKeyRepository.GetApiSecret(locale, environement);
            this._apiSecret = !string.IsNullOrEmpty(secret) ? secret : string.Empty;
            this._apiKey = key;
            this._consumerClient = new ConsumerClient(APIKey, APISecret);
        }
    }
}
