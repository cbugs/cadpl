using Cadbury.Inventor.Core.DTO;
using System;
using System.Globalization;
using System.Net;

namespace Cadbury.Inventor.Core.Module
{
    public class DateFormatValidationModule : ModuleBase, IModule
    {
        public string DateString { get; set; }

        public ResultDTO Process()
        {
            try
            {
                DateTime.ParseExact(DateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                Result.HttpStatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                Result.Code = CodeKeys.INVALID_AGE_FORMAT;
            }
            
            return Result;
        }
    }
}
