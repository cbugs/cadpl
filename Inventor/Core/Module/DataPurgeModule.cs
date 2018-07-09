using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using Cadbury.Inventor.Core.Domain.DAL;
using Cadbury.Inventor.Core.Domain.Entities;
using Cadbury.Inventor.Core.DTO;
using Cadbury.Inventor.Core.Utils;

namespace Cadbury.Inventor.Core.Module
{
    public class DataPurgeModule : ModuleBase, IModule
    {
        public int DaysPeriodFromNow { get; set; }

        public ResultDTO Process()
        {
            var dateTimeFromNow = DateTime.UtcNow.AddDays(-DaysPeriodFromNow);

            using (var context = ContextSwitcher.CreateContext())
            {
                IList<ParticipantDataCache> participantDataCaches = context.ParticipantDataCaches
                    .Where(
                        e => e.Email.Contains("@")
                        && e.CreatedOn < dateTimeFromNow
                    ).ToList();
                foreach(ParticipantDataCache participantDataCache in participantDataCaches)
                {
                    participantDataCache.Email = StringUtils.HashSHA256(participantDataCache.Email.ToLower());
                    participantDataCache.FirstName = StringUtils.HashSHA256(participantDataCache.FirstName.ToLower());
                    participantDataCache.LastName = StringUtils.HashSHA256(participantDataCache.LastName.ToLower());
                    participantDataCache.MobileNumber = StringUtils.HashSHA256(participantDataCache.MobileNumber.ToLower());
                    participantDataCache.City = StringUtils.HashSHA256(participantDataCache.City.ToLower());
                    participantDataCache.UpdatedOn = DateTime.UtcNow;
                    context.Entry(participantDataCache).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }

            Result.HttpStatusCode = HttpStatusCode.OK;

            return Result;
        }
    }
}
