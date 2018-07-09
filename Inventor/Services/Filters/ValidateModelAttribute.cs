using Cadbury.Inventor.Core.DTO;
using System;
using System.Net;
using System.Web.Http.Controllers;

namespace Services.Filters
{
    public class ValidateModelAttribute : ActionAttributeBase
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ResultDTO resultDTO = new ResultDTO()
            {
                HttpStatusCode = HttpStatusCode.BadRequest
            };

            if (!actionContext.ModelState.IsValid)
            {
                foreach (var modelStateValue in actionContext.ModelState.Values)
                {
                    if (modelStateValue.Errors.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(modelStateValue.Errors[0].ErrorMessage))
                        {
                            resultDTO.Code = modelStateValue.Errors[0].ErrorMessage;
                            actionContext.Response = JsonResponse(resultDTO);
                            return;
                        }

                    }
                }
            }
        }
    }
}