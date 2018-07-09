namespace Consultix.ProCampaign
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
    /// API Utility to call Consultix REST API.
    /// </summary>
    public class APIUtility
    {
        #region Properties

        /// <summary>
        /// The end point of the REST API.
        /// </summary>
        public string EndPoint { get; set; }

        /// <summary>
        /// Type of method to be used to make the call.
        /// </summary>
        /// <remarks>
        /// Possible values are : GET, POST, PUT and DELETE.
        /// </remarks>
        public HttpVerb Method { get; set; }

        /// <summary>
        /// API key to be used to make the call.
        /// </summary>
        //public string APIKey
        //{
        //    get
        //    {
        //        return string.Concat("?apiKey=", ConfigurationManager.AppSettings["ConsultixRestAPIKey"]);
        //    }
        //}

        private string _apiKey;

        public string APIKey
        {
            get { return _apiKey; }
            set { _apiKey = value; }
        }

        private string _apiSecret;

        public string APISecret
        {
            get { return _apiSecret; }
            set { _apiSecret = value; }
        }

        public APIUtility()
        {

        }

        public string APIUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ConsultixRestAPIUrl"];
            }
        }

        /// <summary>
        /// Data to be posted in JSON format.
        /// </summary>
        private string PostData
        {
            get
            {
                return JsonConvert.SerializeObject(RootObject);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Make a call to the REST API.
        /// </summary>
        /// <param name="Parameters">Parameters to be used when making the call.</param>
        /// <returns></returns>
        public bool MakeRequest()
        {
            var data = PostData;
            var request = (HttpWebRequest)WebRequest.Create(string.Concat(APIUrl, "?apiKey=", APIKey));
            request.Method = Method.ToString();
            request.Accept = "application/json";
            request.ContentType = "application/json";

            if (!string.IsNullOrEmpty(data) && Method == HttpVerb.POST)
            {
                var bytes = Encoding.UTF8.GetBytes(data);
                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

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
                var responseResult = JsonConvert.DeserializeObject<ResponseRootObject>(responseValue);

                return responseResult.StatusMessage == "Success";
            }
        }

        public string MakeRequest2()
        {
            var data = PostData;
            var client = new HttpClient { BaseAddress = new Uri(string.Concat(APIUrl, APIKey)) };
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage messge = client.PostAsync(string.Concat(APIUrl, APIKey), content).Result;
            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                description = result;
            }

            return description;
        }

        public ResponseRootObject GetProfileByConsumerId(string consumerId, bool requireApiKey = false, bool requireAuthorization = false)
        {
            var queryString = "";
            foreach (var data in RootObject.Attributes)
            {
                var separator = string.IsNullOrEmpty(queryString) ? "" : "&";
                queryString += string.Format("{0}{1}={2}", separator, data.Name, data.Value);
            }
            if (requireApiKey)
            {
                var separator = string.IsNullOrEmpty(queryString) ? "" : "&";
                queryString += string.Format("{0}{1}={2}", separator, "apiKey", APIKey);
            }
            var request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}?{2}", APIUrl, consumerId, queryString));
            if (requireAuthorization)
                SetBasicAuthHeader(request, APIKey, APISecret);
            request.Method = Method.ToString();
            request.Accept = "application/json";
            request.ContentType = "application/json";
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

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
                var responseResult = JsonConvert.DeserializeObject<ResponseRootObject>(responseValue);

                return responseResult.StatusMessage == "Success" ? responseResult : null;
            }
        }

        public ResponseRootObject UpdateProfileByConsumerId(string consumerId, bool requireAuthorization = true)
        {
            if (string.IsNullOrEmpty(consumerId))
            {
                throw new ArgumentNullException("consumerId is null");
            }
            var data = PostData;
            var request = (HttpWebRequest)WebRequest.Create(string.Concat(APIUrl, "/", consumerId));
            if (requireAuthorization)
                SetBasicAuthHeader(request, APIKey, APISecret);
            request.Method = Method.ToString();
            request.Accept = "application/json";
            request.ContentType = "application/json";

            if (!string.IsNullOrEmpty(data) && Method == HttpVerb.PUT)
            {
                var bytes = Encoding.UTF8.GetBytes(data);
                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

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
                var responseResult = JsonConvert.DeserializeObject<ResponseRootObject>(responseValue);

                return responseResult.StatusMessage == "Success" ? responseResult : null;
            }            
        }

        public ResponseRootObject SubscribeWithRequestConsumerId(bool requireAuthorization = true)
        {
            var data = PostData;
            var request = (HttpWebRequest)WebRequest.Create(string.Concat(APIUrl, "?apiKey=", APIKey, "&requestConsumerId=true"));
            if (requireAuthorization)
                SetBasicAuthHeader(request, APIKey, APISecret);
            request.Method = Method.ToString();
            request.Accept = "application/json";
            request.ContentType = "application/json";

            if (!string.IsNullOrEmpty(data) && Method == HttpVerb.POST)
            {
                var bytes = Encoding.UTF8.GetBytes(data);
                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

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
                var responseResult = JsonConvert.DeserializeObject<ResponseRootObject>(responseValue);
                return responseResult.StatusMessage == "Success" ? responseResult : null;
            }
        }

        private void SetBasicAuthHeader(WebRequest request, string userName, string userPassword)
        {
            string authInfo = userName + ":" + userPassword;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
        }

        private RootObject _rootObject = new RootObject();

        public RootObject RootObject
        {
            get { return _rootObject; }
            set { _rootObject = value; }
        }


        private Transaction _transaction = new Transaction();

        public Transaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        public ResponseRootObject ResponseRootObject { get; set; }

        #endregion

        #region Enumerations

        /// <summary>
        /// Types of method call.
        /// </summary>
        /// <value>GET</value>
        /// <value>POST</value>
        /// <value>PUT</value>
        /// <value>DELETE</value>
        public enum HttpVerb
        {
            GET,

            POST,

            PUT,

            DELETE
        }

        #endregion

    }

    #region Classes

    public class TransactionAttribute
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

    public class TransactionParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class Transaction
    {
        public Transaction()
        {
            Parameters = new List<TransactionParameter>();
        }
        public string Name { get; set; }
        public string Source { get; set; }
        public string Date_Created { get; set; }
        public List<TransactionParameter> Parameters { get; set; }
    }

    public class RootObject
    {
        public RootObject()
        {
            Attributes = new List<TransactionAttribute>();
            Transactions = new List<Transaction>();
        }
        public string Source { get; set; }
        public List<TransactionAttribute> Attributes { get; set; }
        public List<Transaction> Transactions { get; set; }
    }

    public class Data
    {
        public string ConsumerId { get; set; }
        public List<TransactionAttribute> Attributes { get; set; }
        public List<Transaction> Transactions { get; set; }
        public string Source { get; set; }
        public string UID { get; set; }
        public string Html { get; set; }
        public IList<HtmlVersion> Versions { get; set; }

        public Data()
        {
            Versions = new List<HtmlVersion>();
        }
    }

    public class HtmlVersion
    {
        public string LegalTextName { get; set; }
        public int Version { get; set; }
    }

    public class ResponseRootObject
    {
        public Data Data { get; set; }
        public string JobId { get; set; }
        public int HttpStatusCode { get; set; }
        public string HttpStatusMessage { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public bool IsSuccessful { get; set; }
    }

    #endregion
}
