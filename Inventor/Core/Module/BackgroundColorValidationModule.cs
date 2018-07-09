using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Cadbury.Inventor.Core.DTO;

namespace Cadbury.Inventor.Core.Module
{
    public class BackgroundColorValidationModule : ModuleBase, IModule
    {
        public string BackgroundColor { get; set; }

        public ResultDTO Process()
        {
            IList<string> backgroundColors = Enum.GetNames(typeof(BackgroundColorKeys)).ToList();
            if (!backgroundColors.Contains(BackgroundColor))
            {
                Result.HttpStatusCode = HttpStatusCode.BadRequest;
                Result.Code = CodeKeys.INVALID_BACKGROUND_COLOR;

                return Result;
            }

            Result.HttpStatusCode = HttpStatusCode.OK;

            return Result;
        }
    }
}
