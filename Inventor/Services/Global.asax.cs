using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cadbury.Inventor.Core.WordTree;


namespace Services
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            //WordTree for bad word detection --------------------------------------------------------------------
            string badWordFilename = System.Configuration.ConfigurationManager.AppSettings["BadWordList"];
            string charsetFilename = System.Configuration.ConfigurationManager.AppSettings["Charset"];

            badWordFilename = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/" + badWordFilename);
            charsetFilename = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/" + charsetFilename);

            //read charset data
            string[] charsetInfo = System.IO.File.ReadAllLines(charsetFilename);
            string allowedChars = charsetInfo[0];
            string separators = charsetInfo[1];
            string wildcards = charsetInfo[2];

            //create tree
            WordTree tree = new WordTree(allowedChars, separators, wildcards, new string[] { }, System.IO.File.ReadAllLines(badWordFilename));

            //add maps
            Dictionary<char, string> fuzzyMap = new Dictionary<char, string>();
            for (int i = 3; i < charsetInfo.Length; i++)
            {
                string[] keyValueMap = charsetInfo[i].Split(new char[] { '\t' });
                fuzzyMap.Add(keyValueMap[0][0], keyValueMap[1]);
            }
            Charset.ExtendFuzzyMap(fuzzyMap);

            Application["WordTree"] = tree;
            //-----------------------------------------------------------------------------------------------------

        }

    }
}
