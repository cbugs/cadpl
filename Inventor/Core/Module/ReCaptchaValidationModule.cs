using Cadbury.Inventor.Core.DTO;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Cadbury.Inventor.Core.Module
{
    public class ReCaptchaValidationModule : ModuleBase, IModule
    {
        public string CaptchaSecret { get; set; }
        public string CaptchaResponse { get; set; }

        public ResultDTO Process()
        {
            var apiRequest = string.Format(
                "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                CaptchaSecret,
                CaptchaResponse);
            var webClient = new WebClient();
            var jsonStr = webClient.DownloadString(apiRequest);
            JToken token = JObject.Parse(jsonStr);
            var success = token.SelectToken("success").ToString().ToLower();
            if (string.IsNullOrEmpty(success) || success == "false")
            {
                Result.Code = CodeKeys.INVALID_CAPTCHA;
                return Result;
            }

            Result.HttpStatusCode = HttpStatusCode.OK;

            return Result;
        }
    }
}
