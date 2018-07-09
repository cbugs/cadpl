using Cadbury.Inventor.Core.Domain.DAL;
using Cadbury.Inventor.Core.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Cadbury.Inventor.Core.Domain.Manager
{
    public class ProCampaignStatusManager
    {
        public static int Count(ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                return context.ProCampaignStatuses.Count();
            }
        }

        public static void Insert(ProCampaignStatus entity, ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                context.ProCampaignStatuses.Add(entity);
                context.SaveChanges();
            }
        }

        public static void Update(ProCampaignStatus entity, ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static IList<ProCampaignStatus> Get(ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                return context.ProCampaignStatuses
                    .OrderByDescending(e => e.CreatedOn)
                    .ToList();
            }
        }

        public static List<ProCampaignStatus> Get(int startIndex, int pageSize, ContextType contextType = ContextType.Default)
        {
            return Get(contextType)
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();
        }
    }
}
