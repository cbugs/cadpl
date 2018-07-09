using System;
using System.IO;
using System.Net;
using System.Web.Hosting;
using Cadbury.Inventor.Core.DTO;

namespace Cadbury.Inventor.Core.Module
{
    public class FilePurgeModule : ModuleBase, IModule
    {
        public int DaysPeriodFromNow { get; set; }

        public ResultDTO Process()
        {
            var applicationPath = HostingEnvironment.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath);
            var path = Path.Combine(applicationPath, "App_Data", "ugi_temp");
            string[] files = Directory.GetFiles(path);
            var counter = 0;
            foreach(var file in files)
            {
                if (File.Exists(file) && !file.Contains(".gitignore"))
                {
                    DateTime fileCreatedDate = File.GetCreationTime(file);
                    if (fileCreatedDate.AddDays(DaysPeriodFromNow) < DateTime.UtcNow)
                    {
                        File.Delete(file);
                        counter++;
                    }
                }
            }

            Result.HttpStatusCode = HttpStatusCode.OK;
            Result.Meta = new
            {
                TotalCount = files.Length,
                TotalDeletion = counter
            };

            return Result;
        }
    }
}
