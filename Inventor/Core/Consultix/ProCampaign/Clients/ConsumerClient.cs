using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Consultix.ProCampaign.Clients
{
    public class ConsumerClient : BaseClient, IConsumerClient
    {
        public ConsumerClient(string key, string secret)
            : base(key, secret)
        {

        }

        public bool Post(Dictionary<string, string> attributes, string source = null, string transactionName = null)
        {
            //if (attributes == null)
            //{
            //    throw new ArgumentNullException("attributes is null");
            //}
            //var apiUtility = new APIUtility();
            //apiUtility.APIKey = ApiKey;

            //var rootObject = apiUtility.RootObject;

            //foreach (var item in attributes)
            //{
            //    rootObject.Attributes.Add(new TransactionAttribute { Name = item.Key, Value = item.Value });
            //}

            throw new NotImplementedException();

        }

        public ResponseRootObject GetProfileByConsumerId(APIUtility transactionObject, string consumerId, bool requireApiKey = false)
        {
            transactionObject.APIKey = ApiKey;
            transactionObject.APISecret = ApiSecret;
            transactionObject.Method = APIUtility.HttpVerb.GET;
            return transactionObject.GetProfileByConsumerId(consumerId, requireApiKey, true);
        }

        public ResponseRootObject UpdateProfileByConsumerId(APIUtility transactionObject, string consumerId)
        {
            transactionObject.APIKey = ApiKey;
            transactionObject.APISecret = ApiSecret;
            transactionObject.Method = APIUtility.HttpVerb.PUT;

            return transactionObject.UpdateProfileByConsumerId(consumerId);
        }

        public bool Subscribe(APIUtility transactionObject)
        {

            transactionObject.APIKey = ApiKey;
            transactionObject.APISecret = ApiSecret;

            //var rootObject = transactionObject.RootObject;
            //rootObject.Source = "CNFR151001_ChristmasGame_Sweepstake_CarteNoire_Campaign";
            //rootObject.Attributes.Add(new Attribute { Name = "Salutation", Value = "1" });
            //rootObject.Attributes.Add(new Attribute { Name = "Lastname", Value = "Lastname" });
            //rootObject.Attributes.Add(new Attribute { Name = "Firstname", Value = "Firstname" });
            //rootObject.Attributes.Add(new Attribute { Name = "Email", Value = "Email" });
            //rootObject.Attributes.Add(new Attribute { Name = "Birthday", Value = "Birthday" });
            //rootObject.Attributes.Add(new Attribute { Name = "Street1", Value = "Street1" });
            //rootObject.Attributes.Add(new Attribute { Name = "Zipcode", Value = "Zipcode" });
            //rootObject.Attributes.Add(new Attribute { Name = "City", Value = "City" });
            //rootObject.Attributes.Add(new Attribute { Name = "PhonePrivate", Value = "PhonePrivate" });
            //rootObject.Attributes.Add(new Attribute { Name = "list:CarteNoire_Email", Value = "" });
            //transactionObject.RefreshAccessToken = APIUtility.HttpVerb.POST;

            //rootObject.Transactions.Add(new Transaction
            //{
            //    Name = "CNFR151001 Carte Noire Christmas Game Sweepstake (IN)",
            //    Source = "CNFR151001_ChristmasGame_Sweepstake_CarteNoire_Campaign",
            //    Date_Created = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"),
            //    Parameters = new List<TransactionParameter> { new TransactionParameter { Name = "Ident_long", Value = "ADF12A67-E97D-4C5E-86FB-85FD5BF00B18" } }
            //});

            bool isSuccess = transactionObject.MakeRequest();
            return isSuccess;
        }

        public ResponseRootObject SubscribeWithRequestConsumerId(APIUtility transactionObject)
        {

            transactionObject.APIKey = ApiKey;
            transactionObject.APISecret = ApiSecret;
            transactionObject.Method = APIUtility.HttpVerb.POST;
            return transactionObject.SubscribeWithRequestConsumerId();
        }
    }
}
