using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Consultix.ProCampaignAccount.Models;

namespace Consultix.ProCampaignAccount
{
    public class APIUtility
    {
        public enum HttpVerb
        {
            GET,

            POST,

            PUT,

            DELETE
        }

        public static ResponseMapping<T> MakeAccountRequest<T>(string url, HttpVerb Method, Object PostData, string APIKey, string token = "", string additionnalParam = "")
        {
            Dictionary<string,object> dict  = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(token))
            {
                dict.Add("Authorization", "Bearer "+token);
            }
            return JsonConvert.DeserializeObject<ResponseMapping<T>>(MakeRequest(url, Method, PostData, APIKey, dict, additionnalParam));
        }

        public static ResponseMapping<ConsumerInformationResponse> MakeConsumerRequest(string url, HttpVerb Method, Object PostData, string APIKey, string token = "", string additionnalParam = "")
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(token))
            {
                dict.Add("Authorization", "Bearer " + token);
            }
            return JsonConvert.DeserializeObject<ResponseMapping<ConsumerInformationResponse>>(MakeRequest(url, Method, PostData, APIKey, dict, additionnalParam, "Fiddler", "consumer.procampaignapi.com"));
        }

        public static string MakeRequest(string url, HttpVerb Method, Object PostData, string APIKey,Dictionary<string,object> additionnalHeader, string additionnalParam = "",string userAgent=null,string host=null)
        {
            var responseValue = string.Empty;
            try
            {
                var data = JsonConvert.SerializeObject(PostData);
                var request =
                    (HttpWebRequest)WebRequest.Create(string.Concat(url, "?apiKey=", APIKey, additionnalParam));
                request.Method = Method.ToString();
                request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";
                if (!string.IsNullOrEmpty(userAgent))
                {
                    request.UserAgent = userAgent;
                }
                if (!string.IsNullOrEmpty(host))
                {
                    request.Host = host;
                }
                
                foreach (var kv in additionnalHeader)
                {
                    request.Headers.Add(kv.Key, kv.Value.ToString());
                }
                
                if (!string.IsNullOrEmpty(data) && Method == HttpVerb.POST)
                {
                    var bytes = Encoding.UTF8.GetBytes(data);
                    request.ContentLength = bytes.Length;

                    using (var writeStream = request.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }
                //request.get
                using (var response = (HttpWebResponse)request.GetResponse())
                {

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }
                    return responseValue;
                }
            }
            catch (WebException ex)
            {
                //Logger.Log(ex);
                using (var responseStream = ((HttpWebResponse)ex.Response).GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }
                return responseValue;
            }
        }
    }
}
