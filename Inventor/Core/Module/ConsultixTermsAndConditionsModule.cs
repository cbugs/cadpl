using System;
using System.IO;
using System.Net;
using Cadbury.Inventor.Core.DTO;
using Consultix.ProCampaign;
using Consultix.ProCampaign.Configuration;
using Newtonsoft.Json;

namespace Cadbury.Inventor.Core.Module
{
    public class ConsultixTermsAndConditionsModule : ModuleBase, IModule
    {
        public string TermsAndConditionsName { get; set; }
        public string Locale { get; set; }
        public string Environment { get; set; }

        public ResultDTO Process()
        {
            try
            {
                // Build request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                string.Format(
                    "https://api.procampaignapi.com/Consumer/Documents/{0}/{1}?apiKey={2}",
                    TermsAndConditionsName,
                    TermsAndConditionsName,
                    ProCampaignKeyRepository.GetApiKey(Locale, Environment)
                ));
                request.Method = "Get";
                request.KeepAlive = true;

                // Get response
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                request.ContentType = "application/json";
                Result.HttpStatusCode = HttpStatusCode.OK;
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string responseValue = streamReader.ReadToEnd();
                    ResponseRootObject responseResult = JsonConvert.DeserializeObject<ResponseRootObject>(responseValue);
                    if(responseResult.IsSuccessful)
                    {
                        Result.HttpStatusCode = HttpStatusCode.OK;
                        Result.Meta = new
                        {
                            PolicyText = String.Empty,
                            PolicyVersion = String.Empty,
                            TnCText = responseResult.Data.Html,
                            TnCVersion = responseResult.Data.Versions[0].Version
                        };
                    }
                    else
                    {
                        Result.HttpStatusCode = HttpStatusCode.InternalServerError;
                        Result.Code = CodeKeys.SYSTEM_ERROR_01;
                    }
                }
            }
            catch(Exception ex)
            {
                Result.HttpStatusCode = HttpStatusCode.InternalServerError;
                Result.Code = CodeKeys.SYSTEM_ERROR_01;
            }

            return Result;
        }
    }
}
