using Cadbury.Inventor.Core.DTO;
using System.Net;

namespace Cadbury.Inventor.Core.Module
{
    public class ModuleBase
    {
        public ResultDTO Result { get; set; }

        public ModuleBase()
        {
            Result = new ResultDTO()
            {
                HttpStatusCode = HttpStatusCode.BadRequest
            };
        }
    }
}
