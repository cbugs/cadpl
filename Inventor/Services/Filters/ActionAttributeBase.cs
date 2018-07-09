using Cadbury.Inventor.Core.DTO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace Services.Filters
{
    public class ActionAttributeBase : ActionFilterAttribute
    {
        public HttpResponseMessage JsonResponse(ResultDTO resultDTO)
        {
            string response = JsonConvert.SerializeObject(resultDTO);
            HttpResponseMessage responseMessage = new HttpResponseMessage()
            {
                Content = new StringContent(response)
            };
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            responseMessage.StatusCode = resultDTO.HttpStatusCode;

            return responseMessage;
        }
    }
}