using System;
using System.Web.Mvc;
using Cadbury.Inventor.Core.Domain.DAL;

namespace Admin.Controllers
{
    public class ContextController : Controller
    {
        [HttpPost]
        public ActionResult ChangeContext(string newContext)
        {
            if (Session["CurrentContext"] == null)
                Session["CurrentContext"] = ContextType.Default;

            ContextType selectedEnvironment = (ContextType)Enum.Parse(typeof(ContextType), Session["CurrentContext"].ToString(), true);


            if (Enum.TryParse<ContextType>(newContext, out selectedEnvironment))
            {
                using (InventorContext context = ContextSwitcher.CreateContext(selectedEnvironment))
                {
                    if (!context.Database.Exists())
                    {
                        return Json(new
                        {
                            Result = "FAILED",
                            Message = String.Format(
                                "Database not found for context \"{0}\" !",
                                newContext
                            )
                        });
                    }
                    else
                    {
                        Session["CurrentContext"] = newContext;
                    }
                }

                return Json(new
                {
                    Result = "OK"
                });
            }
            else
            {
                return Json(new
                {
                    Result = "FAILED",
                    Message = String.Format(
                        "Context \"{0}\" not found in contexts list !",
                        newContext
                    )
                });
            }
        }


        [HttpGet]
        public ActionResult GetContext()
        {
            if (Session["CurrentContext"] == null)
                Session["CurrentContext"] = ContextType.Default;

            return Json(
                Session["CurrentContext"].ToString(), 
                JsonRequestBehavior.AllowGet
            );
        }
    }
}