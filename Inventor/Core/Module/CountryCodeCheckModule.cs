using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Cadbury.Inventor.Core.DTO;

namespace Cadbury.Inventor.Core.Module
{
    public class CountryCodeCheckModule : ModuleBase, IModule
    {
        public string CountryCode { get; set; }

        public ResultDTO Process()
        {
            if (CountryCode != CountryKeys.UK && CountryCode != CountryKeys.IE)
            {
                Result.HttpStatusCode = HttpStatusCode.BadRequest;
                Result.Code = CodeKeys.INVALID_COUNTRY_CODE;

                return Result;
            }

            Result.HttpStatusCode = HttpStatusCode.OK;

            return Result;
        }
    }
}
