using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Services.Controllers
{
    public class DummyController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Root()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}