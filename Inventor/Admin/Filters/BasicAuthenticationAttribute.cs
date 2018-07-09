using System;
using System.Configuration;
using System.Web.Mvc;

namespace Admin.Filters
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        public string BasicRealm { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public BasicAuthenticationAttribute()
        {
            this.Username = ConfigurationManager.AppSettings["AdminUser"];
            this.Password = ConfigurationManager.AppSettings["AdminPass"];
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var req = filterContext.HttpContext.Request;
            var auth = req.Headers["Authorization"];
            if (!String.IsNullOrEmpty(auth))
            {
                var cred = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
                var user = new { Name = cred[0], Pass = cred[1] };
                if (user.Name == Username && user.Pass == Password) return;
            }
            filterContext.HttpContext.Response.AddHeader("WWW-Authenticate", String.Format("Basic realm=\"{0}\"", BasicRealm ?? "Restricted Area"));

            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}