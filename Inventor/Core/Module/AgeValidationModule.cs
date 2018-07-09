using Cadbury.Inventor.Core.DTO;
using System;
using System.Net;

namespace Cadbury.Inventor.Core.Module
{
    public class AgeValidationModule : ModuleBase, IModule
    {
        public DateTime BirthDate { get; set; }
        public int MaximumAge { get; set; }

        public ResultDTO Process()
        {
            if (BirthDate.AddYears(MaximumAge) > DateTime.UtcNow)
            {
                Result.Code = CodeKeys.INVALID_AGE;
                return Result;
            }

            Result.HttpStatusCode = HttpStatusCode.OK;

            return Result;
        }
    }
}
