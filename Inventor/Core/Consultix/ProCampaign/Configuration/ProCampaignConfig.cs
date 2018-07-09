using System.Configuration;

namespace Consultix.ProCampaign.Configuration
{

    public class ProCampaignKeyRepository
    {
        public static string CurrentEnvironment
        {
            get
            {
                string environment = ConfigurationManager.AppSettings["Environment"];
                if (!string.IsNullOrEmpty(environment))
                {
                    return environment.ToLower();
                }
                return string.Empty;
            }
        }
        static ProCampaignRetrieverSection Config = ConfigurationManager.GetSection("ProCampaignClients") as ProCampaignRetrieverSection;

        public static string GetApiKey(string locale, string environment)
        {
            if (environment == null)
            {
                environment = CurrentEnvironment;
            }

            foreach (ProCampaignElement item in Config.Clients)
            {
                if (item.Locale.ToLower() == locale.ToLower() &&
                    item.Environment.ToLower() == environment.ToLower())
                {
                    return item.Key;
                }
            }

            return null;
        }

        public static string GetApiSecret(string locale, string environment)
        {
            if (environment == null)
            {
                environment = CurrentEnvironment;
            }

            foreach (ProCampaignElement item in Config.Clients)
            {
                if (item.Locale.ToLower() == locale.ToLower() &&
                    item.Environment.ToLower() == environment.ToLower())
                {
                    return item.Secret;
                }
            }

            return null;
        }

        public static string GetApiKey()
        {
            return Config.Clients[0].Key;
        }
    }

    public class ProCampaignRetrieverSection : ConfigurationSection
    {
        [ConfigurationProperty("Clients")]
        public ProCampaignClientCollection Clients
        {
            get { return (ProCampaignClientCollection)this["Clients"]; }
        }
    }

    [ConfigurationCollection(typeof(ProCampaignElement))]
    public class ProCampaignClientCollection : ConfigurationElementCollection
    {
        public ProCampaignElement this[int index]
        {
            get { return (ProCampaignElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ProCampaignElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            //set to whatever Element SweetOneConfiguration you want to use for a key
            return ((ProCampaignElement)element);
        }
    }

    public class ProCampaignElement : ConfigurationElement
    {
        public ProCampaignElement()
        {

        }
        [ConfigurationProperty("Locale", DefaultValue = "", IsRequired = true)]
        public string Locale
        {
            get { return (string)this["Locale"]; }
            set { this["Locale"] = value; }
        }

        [ConfigurationProperty("Environment", DefaultValue = "", IsRequired = true)]
        public string Environment
        {
            get { return (string)this["Environment"]; }
            set { this["Environment"] = value; }
        }

        [ConfigurationProperty("Key", DefaultValue = "", IsRequired = true)]
        public string Key
        {
            get { return (string)this["Key"]; }
            set { this["Key"] = value; }
        }

        [ConfigurationProperty("Secret", DefaultValue = "", IsRequired = false)]
        public string Secret
        {
            get { return (string)this["Secret"]; }
            set { this["Secret"] = value; }
        }
    }
}
