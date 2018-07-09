using System;
using System.Net;
using Cadbury.Inventor.Core.Domain.Entities;
using Cadbury.Inventor.Core.Domain.Manager;
using Cadbury.Inventor.Core.DTO;
using Consultix.ProCampaign;
using Newtonsoft.Json;

namespace Cadbury.Inventor.Core.Module
{
    public class ConsultixRetryModule : ModuleBase, IModule
    {
        public ResultDTO Process()
        {
            // Get all new failed transactions
            var proCampaignTransactions = ProCampaignTransactionManager.GetByStatus(ProCampaignTransactionStatusKeys.NEW);

            foreach (var proCampaignTransaction in proCampaignTransactions)
            {
                // Deserialise transaction object
                ProCampaignStatus proCampaignStatus = proCampaignTransaction.ProCampaignStatus;
                var transactionObject = (APIUtility)JsonConvert.DeserializeObject(
                    proCampaignTransaction.TransactionObject,
                    new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All
                    }
                );

                // Send the transaction object to Consultix
                ResponseRootObject apiResult = null;
                try
                {
                    apiResult = ProCampaignFactory.ConfigureProCampaignService(proCampaignTransaction.Database)
                        .ConsumerClient
                        .SubscribeWithRequestConsumerId(transactionObject);
                    if (apiResult != null && apiResult.IsSuccessful == true)
                    {
                        // Update proCampaignTransaction
                        proCampaignTransaction.Status = ProCampaignTransactionStatusKeys.SENT;
                        proCampaignTransaction.TransactionObject = String.Empty;
                        proCampaignTransaction.UpdatedOn = DateTime.UtcNow;
                        ProCampaignTransactionManager.Update(proCampaignTransaction);
                        // Update proCampaignStatus
                        proCampaignStatus.IsSuccessful = apiResult.IsSuccessful;
                        proCampaignStatus.ResponseCode = apiResult.StatusCode;
                        proCampaignStatus.ResponseText = apiResult.StatusMessage;
                        proCampaignStatus.HttpStatusCode = apiResult.HttpStatusCode;
                        proCampaignStatus.HttpStatusMessage = apiResult.HttpStatusMessage;
                        proCampaignStatus.UpdatedOn = DateTime.UtcNow;
                        ProCampaignStatusManager.Update(proCampaignStatus);
                        // Update participant
                        Participant participant = proCampaignStatus.Entry.Participant;
                        participant.ConsumerId = apiResult.Data.ConsumerId;
                        participant.UpdatedOn = DateTime.UtcNow;
                        ParticipantManager.Update(participant);
                    }
                }
                catch (WebException ex)
                {
                    proCampaignTransaction.UpdatedOn = DateTime.UtcNow;
                    ProCampaignTransactionManager.Update(proCampaignTransaction);
                }
            }

            Result.HttpStatusCode = HttpStatusCode.OK;

            return Result;
        }
    }
}
