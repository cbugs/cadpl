using Cadbury.Inventor.Core.DTO;
using System;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Web.Http.Controllers;

namespace Services.Filters
{
    public class ValidateSiteStatusAttribute : ActionAttributeBase
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ResultDTO resultDTO = new ResultDTO()
            {
                HttpStatusCode = HttpStatusCode.BadRequest
            };

            try
            {
                var campaignStart = DateTime.ParseExact(
                    ConfigurationManager.AppSettings["CampaignStart"],
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture
                );
                var campaignEnd = DateTime.ParseExact(
                    ConfigurationManager.AppSettings["CampaignEnd"],
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture
                );
                if (campaignStart > DateTime.UtcNow)
                {
                    resultDTO.Code = CodeKeys.CAMPAIGN_NOT_STARTED;
                    actionContext.Response = JsonResponse(resultDTO);
                    return;
                }
                else if (campaignEnd < DateTime.UtcNow)
                {
                    resultDTO.Code = CodeKeys.CAMPAIGN_ENDED;
                    actionContext.Response = JsonResponse(resultDTO);
                    return;
                }
            }
            catch (Exception ex) { }
        }
    }
}