using Admin.Filters;
using Admin.Models;
using Cadbury.Inventor.Core.Domain.DAL;
using System.Web.Mvc;

namespace Admin.Controllers
{
    //[BasicAuthentication(Username = "proxadmin", Password = "proxpass", BasicRealm = "Prox Administration")]
    [BasicAuthentication]
    public class BaseController : Controller
    {
        protected ContextType CurrentContextType;

        protected string Filter {
            get
            {
                return Session[SessionKeys.Filter] as string;
            }
            set
            {
                Session[SessionKeys.Filter] = value;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            AssignContext();

            if (HttpContext.Request.HttpMethod == "GET")
            {
                ResetFilter();
            }
        }

        private void AssignContext()
        {
            if (Session["CurrentContext"] == null)
                Session["CurrentContext"] = ContextType.Default;
            string contextFromSession = Session["CurrentContext"].ToString();
            CurrentContextType = ContextSwitcher.GetUserContext(contextFromSession);
        }

        protected void ResetFilter()
        {
            Filter = null;
        }
    }
}