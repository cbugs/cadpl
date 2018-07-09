using System;
using Cadbury.Inventor.Core.Domain.DTO;
using Cadbury.Inventor.Core.Domain.Entities;
using Cadbury.Inventor.Core.Domain.DAL;


namespace Cadbury.Inventor.Core.Domain
{
    public class Mediator
    {
        /// <summary>
        /// Generate a PII cache object from this object
        /// </summary>
        /// <returns></returns>
        public static Entities.ParticipantDataCache GeneratePIIDataCacheFromParticipationEntry(DTO.ParticipationEntry entry)
        {
            Entities.ParticipantDataCache returnCacheObject = new Entities.ParticipantDataCache();
            returnCacheObject.Id = System.Guid.NewGuid();
            returnCacheObject.BirthDate = entry.BirthDate;
            returnCacheObject.City = entry.City;
            returnCacheObject.CountryCode = entry.CountryCode;
            returnCacheObject.CreatedOn = DateTime.Now;
            returnCacheObject.UpdatedOn = DateTime.Now;
            returnCacheObject.Email = entry.Email;
            returnCacheObject.FirstName = entry.FirstName;
            returnCacheObject.LastName = entry.LastName;
            returnCacheObject.MobileNumber = entry.MobileNumber;

            return returnCacheObject;
        }


        
    }
}
