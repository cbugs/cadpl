using System;
using System.Collections.Generic;
using Consultix.ProCampaign.Configuration;
using Consultix.ProCampaignAccount.Models;
using System.Configuration;

namespace Consultix.ProCampaignAccount
{
    /// <summary>
    /// API Utility to call Consultix REST API. 
    /// </summary>
    public class ProCampaignAccountService
    {

        /// <summary>
        /// Logs in the user. https://consumer.procampaignapi.com/Help/Api/POST-Account-Login.
        /// </summary>
        public ConsumerServiceResponse<UserInfo> Login(string login, string password, string local, bool remember = false)
        {
            try
            {
                var apikey = GetApiKey(local, null);
                var response = APIUtility.MakeAccountRequest<UserInfo>(
                    AccountApiUrl + "/Login", 
                    APIUtility.HttpVerb.POST,
                    new LoginRequestForm()
                    {
                        UserName = login,
                        Password = password,
                        ApiKey = apikey,
                        Remember = remember
                    }, 
                    apikey
                );
                if (response.IsSuccessful)
                {
                    return new ConsumerServiceResponse<UserInfo>() {
                        StatusCode = response.StatusCode,
                        Data = response.Data,
                        IsSuccessful = response.IsSuccessful
                    };
                }
                return new ConsumerServiceResponse<UserInfo>() {
                    StatusCode = response.StatusCode,
                    Data = null,
                    IsSuccessful = response.IsSuccessful
                };
            }
            catch (Exception ex)
            {
                //Logger.Log(ex);
                return null;
            }
        }

        /// <summary>
        /// Registers a new user. https://consumer.procampaignapi.com/Help/Api/POST-Account-Register.
        /// </summary>
        public ConsumerServiceResponse<UserInfo> Register(RegisterRequestForm userinfo, string local)
        {
            var apikey = GetApiKey(local, null);
            userinfo.ApiKey = apikey;
            var response = APIUtility.MakeAccountRequest<RegisterDataModel>(
                AccountApiUrl + "/Register",
                APIUtility.HttpVerb.POST, 
                userinfo, 
                apikey);
            if (response.IsSuccessful)
            {
                return new ConsumerServiceResponse<UserInfo>() {
                    StatusCode = response.StatusCode,
                    Data = response.Data.UserInfo,
                    IsSuccessful = response.IsSuccessful
                };
            }
            return new ConsumerServiceResponse<UserInfo>() {
                StatusCode = response.StatusCode,
                Data = null,
                IsSuccessful = response.IsSuccessful
            };
        }

        /// <summary>
        /// Gets the user's info. https://consumer.procampaignapi.com/Help/Api/GET-Consumer-Attributes_attributes.
        /// </summary>
        public ConsumerServiceResponse<Dictionary<string, Object>> GetUserInfoByToken(string userToken, string local, List<string> attributesName)
        {
            var apikey = GetApiKey(local, null);
            var attrString = String.Join(",", attributesName);
            var response = APIUtility.MakeConsumerRequest(
                ConsumerApiUrl + "/Attributes", 
                APIUtility.HttpVerb.GET, 
                null, 
                apikey, 
                userToken, 
                "&attributes=" + attrString
            );
            Dictionary<string, Object> res = new Dictionary<string, object>();
            if (response.IsSuccessful)
            {
                foreach (var attr in response.Data.Attributes)
                {
                    res[attr.Name] = attr.Value;
                }
                return new ConsumerServiceResponse<Dictionary<string, object>>()
                {
                    Data = res,
                    IsSuccessful = true
                };
            }
            return new ConsumerServiceResponse<Dictionary<string, object>>()
            {
                StatusCode = response.StatusCode,
                IsSuccessful = false
            };
        }

        /// <summary>
        /// Initiates a reset password request. An email will be send to the given contact attribute. https://consumer.procampaignapi.com/Help/Api/POST-Account-TriggerResetPassword.
        /// </summary>
        public ConsumerServiceResponse<Object> TriggerResetPassword(string email, string local)
        {
            var apikey = GetApiKey(local, null);
            var response = APIUtility.MakeAccountRequest<Object>(
                AccountApiUrl + "/TriggerResetPassword", 
                APIUtility.HttpVerb.POST, 
                new {
                    Email = email,
                    ApiKey = apikey
                }, 
                apikey
            );
            return new ConsumerServiceResponse<Object>() {
                StatusCode = response.StatusCode,
                IsSuccessful = response.IsSuccessful
            };
        }

        /// <summary>
        /// Resets the user's password. Token is provided by the email sent in /TriggerResetPassword. https://consumer.procampaignapi.com/Help/Api/POST-Account-FinalizeResetPassword.
        /// </summary>
        public ConsumerServiceResponse<Object> FinalizeResetPassword(string email, string newpass, string tokenfromemail, string local)
        {
            var apikey = GetApiKey(local, null);
            var response = APIUtility.MakeAccountRequest<Object>(
                AccountApiUrl + "/FinalizeResetPassword", 
                APIUtility.HttpVerb.POST, 
                new ResetPasswordRequestForm()
                {
                    Email = email,
                    Token = tokenfromemail,
                    ApiKey = apikey,
                    NewPassword = newpass
                }, 
                apikey
            );
            return new ConsumerServiceResponse<Object>() {
                StatusCode = response.StatusCode,
                IsSuccessful = response.IsSuccessful
            };
        }

        /// <summary>
        /// Changes the password of a user. https://consumer.procampaignapi.com/Help/Api/POST-Account-ChangePassword.
        /// </summary>
        public ConsumerServiceResponse<String> ChangePassword(string oldPassword, string newPassword, string token, string local)
        {
            var apikey = GetApiKey(local, null);
            var response = APIUtility.MakeAccountRequest<Object>(
                AccountApiUrl + "/ChangePassword", 
                APIUtility.HttpVerb.POST, 
                new
                {
                    OldPassword = oldPassword,
                    NewPassword = newPassword,
                    ApiKey = apikey
                }, 
                apikey,
                token
            );
            return new ConsumerServiceResponse<String>() {
                StatusCode = response.StatusCode,
                IsSuccessful = response.IsSuccessful,
                Data = response.StatusMessage
            };
        }


        #region configuration

        static ProCampaignRetrieverSection Config = ConfigurationManager.GetSection("ProCampaignClients") as ProCampaignRetrieverSection;

        public string GetApiKey(string locale, string environment)
        {
            if (environment == null)
            {
                environment = CurrentEnvironment;
            }
            foreach (ProCampaignElement item in Config.Clients)
            {
                if (item.Locale.ToLower() == locale.ToLower() && item.Environment.ToLower() == environment.ToLower())
                {
                    return item.Key;
                }
            }

            return null;
        }

        public string CurrentEnvironment
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

        public string AccountApiUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ConsultixConsumerRestAPIUrl"] + "/Account";
            }
        }
        public string ConsumerApiUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ConsultixConsumerRestAPIUrl"] + "/Consumer";
            }
        }

        #endregion
    }
}
