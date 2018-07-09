using Newtonsoft.Json;

namespace Cadbury.Inventor.Core.DTO
{
    public class ResultDTO
    {
        [JsonIgnore]
        public System.Net.HttpStatusCode HttpStatusCode { get; set; }
        public string Code { get; set; }
        public string Detail { get; set; }
        public string Message { get; set; }
        public dynamic Meta { get; set; }

        public ResultDTO()
        {
            Meta = new {};
        }
    }
}
