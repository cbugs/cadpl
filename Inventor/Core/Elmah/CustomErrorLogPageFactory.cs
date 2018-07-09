using System;
using System.Reflection;
using System.Web;
using Elmah;
using System.Collections;
using System.Configuration;

namespace Cadbury.Inventor.Core.Elmah
{
    public class CustomErrorLogPageFactory : ErrorLogPageFactory
    {

        public override IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            var environment = context.Request.Params["Environment"];
            if (String.IsNullOrEmpty(environment))
            {
                if (context.Request.UrlReferrer != null && !"/stylesheet".Equals(context.Request.PathInfo, StringComparison.OrdinalIgnoreCase))
                {
                    environment = HttpUtility.ParseQueryString(context.Request.UrlReferrer.Query)["Environment"];
                    context.Response.Redirect(String.Format("{0}{1}environment={2}", 
                        context.Request.RawUrl, 
                        context.Request.Url.Query.Length > 0 ? "&" : "?", 
                        environment
                    ));
                }
            }
            IHttpHandler handler = base.GetHandler(context, requestType, url, pathTranslated);
            var err = new SqlErrorLog(this.GetConnectionString(environment));
            IDictionary config = (IDictionary)ConfigurationManager.GetSection("elmah/errorLog");
            err.ApplicationName = (string)config["applicationName"];
            object v = typeof(ErrorLog).GetField("_contextKey", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            context.Items[v] = err;

            return handler;
        }

        public string GetConnectionString(string environment)
        {
            if (string.IsNullOrEmpty(environment))
            {
                return ConfigurationManager.ConnectionStrings["DefaultElmah"].ConnectionString;
            }

            if (environment.ToLower().Equals("prod"))
            {
                return ConfigurationManager.ConnectionStrings["ProdElmah"].ConnectionString;
            }

            return ConfigurationManager.ConnectionStrings["DefaultElmah"].ConnectionString;
        }

    }
}
