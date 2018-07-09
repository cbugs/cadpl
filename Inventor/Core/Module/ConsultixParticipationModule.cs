using Cadbury.Inventor.Core.Domain.Entities;
using Cadbury.Inventor.Core.Domain.Manager;
using Cadbury.Inventor.Core.DTO;
using Consultix.ProCampaign;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Cadbury.Inventor.Core.Module
{
    public class ConsultixParticipationModule : ModuleBase, IModule
    {
        public string CountryCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobilePrivate { get; set; }
        public string City { get; set; }
        public string Birthday { get; set; }
        public bool CadburyEmail { get; set; }
        public string Ingredient1Category { get; set; }
        public string Ingredient1Name { get; set; }
        public string Ingredient1Colour { get; set; }
        public string Ingredient2Category { get; set; }
        public string Ingredient2Name { get; set; }
        public string Ingredient2Colour { get; set; }
        public string Ingredient3Category { get; set; }
        public string Ingredient3Name { get; set; }
        public string Ingredient3Colour { get; set; }
        public Ingredient Ingredient2 { get; set; }
        public Ingredient Ingredient3 { get; set; }
        public string BarColour { get; set; }
        public string BarDescription { get; set; }
        public string BarName { get; set; }
        public string ParticipationId { get; set; }
        public Guid EntryId { get; set; }


        public ResultDTO Process()
        {
            var source = (CountryCode == CountryKeys.IE) ? "CBIE180101_Inventor_Promotion_Cadbury_Campaign" : "CBUK180501_Inventor_Promotion_Cadbury_Campaign";
            var transactionName = (CountryCode == CountryKeys.IE) ? "CBIE180101 Cadbury Inventor Promotion Participation (IN)" : "CBUK180501 Cadbury Inventor Promotion Participation (IN)";
            var participantsListName = (CountryCode == CountryKeys.IE) ? "list:CBIE180101_Participants" : "list:CBUK180501_Participants";
            var termsAndConditionsName = (CountryCode == CountryKeys.IE) ? "list:CBIE180101_TermsAndConditions" : "list:CBUK180501_TermsAndConditions";

            APIUtility transactionObject = new APIUtility();
            var rootObject = transactionObject.RootObject;
            rootObject.Source = source;
            rootObject.Attributes.Add(new TransactionAttribute { Name = "Firstname", Value = FirstName });
            rootObject.Attributes.Add(new TransactionAttribute { Name = "Lastname", Value = LastName });
            rootObject.Attributes.Add(new TransactionAttribute { Name = "Email", Value = Email });
            rootObject.Attributes.Add(new TransactionAttribute { Name = "MobilePrivate", Value = MobilePrivate });
            rootObject.Attributes.Add(new TransactionAttribute { Name = "City", Value = City });
            rootObject.Attributes.Add(new TransactionAttribute { Name = "Birthday", Value = Birthday });
            rootObject.Attributes.Add(new TransactionAttribute { Name = "list:Cadbury_Email", Value = (CadburyEmail) ? 1 : 0 });
            rootObject.Attributes.Add(new TransactionAttribute { Name = participantsListName, Value = 1 });
            rootObject.Attributes.Add(new TransactionAttribute { Name = "list:Privacy_Policy_EN", Value = 1 });
            rootObject.Attributes.Add(new TransactionAttribute { Name = termsAndConditionsName, Value = 1 });
            transactionObject.Method = APIUtility.HttpVerb.POST;
            rootObject.Transactions.Add(new Transaction
            {
                Name = transactionName,
                Source = source,

                Date_Created = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"),
                Parameters = new List<TransactionParameter>
                {
                    new TransactionParameter { Name = "Q1", Value = Ingredient1Category },
                    new TransactionParameter { Name = "Q2", Value = Ingredient1Name },
                    new TransactionParameter { Name = "Q3", Value = Ingredient1Colour },
                    new TransactionParameter { Name = "Q4", Value = Ingredient2Category },
                    new TransactionParameter { Name = "Q5", Value = Ingredient2Name },
                    new TransactionParameter { Name = "Q6", Value = Ingredient2Colour },
                    new TransactionParameter { Name = "Q7", Value = Ingredient3Category },
                    new TransactionParameter { Name = "Q8", Value = Ingredient3Name },
                    new TransactionParameter { Name = "Q9", Value = Ingredient3Colour },
                    new TransactionParameter { Name = "Q10", Value = BarColour },
                    new TransactionParameter { Name = "Q11", Value = BarDescription },
                    new TransactionParameter { Name = "Q12", Value = BarName },
                    new TransactionParameter { Name = "Q15", Value = ParticipationId },
                }
            });

            ResponseRootObject apiResult = null;
            try
            {
                apiResult = ProCampaignFactory.ConfigureProCampaignService(CountryCode)
                    .ConsumerClient
                    .SubscribeWithRequestConsumerId(transactionObject);
            }
            catch (WebException ex)
            {
                using (var responseStream = ((HttpWebResponse)ex.Response).GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            var responseValue = reader.ReadToEnd();
                            try
                            {
                                apiResult = JsonConvert.DeserializeObject<ResponseRootObject>(responseValue);
                            }
                            catch(Exception exc) { }
                        }
                }
            }

            var proCampaignStatus = new ProCampaignStatus()
            {
                Id = Guid.NewGuid(),
                EntryId = EntryId,
                IsSuccessful = (apiResult == null) ? false : apiResult.IsSuccessful,
                ResponseCode = (apiResult == null) ? 0 : apiResult.StatusCode,
                ResponseText = (apiResult == null) ? String.Empty : apiResult.StatusMessage,
                HttpStatusCode = (apiResult == null) ? 0 : apiResult.HttpStatusCode,
                HttpStatusMessage = (apiResult == null) ? String.Empty : apiResult.HttpStatusMessage,
                CreatedOn = DateTime.UtcNow
            };
            ProCampaignStatusManager.Insert(proCampaignStatus);

            if (apiResult != null && apiResult.IsSuccessful)
            {
                Result.HttpStatusCode = HttpStatusCode.OK;
                Result.Meta = new
                {
                    ConsumerId = apiResult.Data.ConsumerId
                };
            }
            else
            {
                Result.HttpStatusCode = HttpStatusCode.InternalServerError;
                Result.Code = CodeKeys.SYSTEM_ERROR_02;

                var proCampaignTransaction = new ProCampaignTransaction()
                {
                    Id = Guid.NewGuid(),
                    ProCampaignStatusId = proCampaignStatus.Id,
                    Status = ProCampaignTransactionStatusKeys.NEW,
                    Database = CountryCode,
                    TransactionObject = JsonConvert.SerializeObject(
                        transactionObject, 
                        new JsonSerializerSettings()
                        {
                            TypeNameHandling = TypeNameHandling.All
                        }),
                    CreatedOn = DateTime.UtcNow
                };
                ProCampaignTransactionManager.Insert(proCampaignTransaction);
            }

            return Result;
        }
    }
}
