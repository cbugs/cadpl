using Consultix.ProCampaign.Clients;
using Consultix.ProCampaign.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Consultix.ProCampaign
{
    public class ProCampaignFactory
    {
        private static readonly object Padlock = new object();
        private static Hashtable repositories = new Hashtable();

        public static ProCampaignService ConfigureProCampaignService(string locale, string environment = null)
        {
            if (string.IsNullOrEmpty(locale))
            {
                throw new ArgumentNullException("Locale cannot be null");
            }
            return InternalGetClient(locale, environment);
        }

        private static ProCampaignService InternalGetClient(string locale, string environment)
        {
            string key = string.Concat(locale, environment);
            ProCampaignService provider = null;
            if (string.IsNullOrEmpty(key))
            {
                key = "DEFAULT_PROVIDER";
            }

            if (!repositories.Contains(key))
            {
                lock (Padlock)
                {
                    if (!repositories.Contains(key))
                    {
                        provider = new ProCampaignService(locale, environment);
                        if (provider != null)
                        {
                            repositories.Add(key, provider);
                        }
                    }
                }
            }
            else
            {
                provider = repositories[key] as ProCampaignService;
            }

            return provider;
        }



    }
}
