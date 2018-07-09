using Cadbury.Inventor.Core.Domain.DAL;
using Cadbury.Inventor.Core.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Cadbury.Inventor.Core.Domain.Manager
{
    public class ProCampaignTransactionManager
    {
        public static int Count(ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                return context.ProCampaignTransactions.Count();
            }
        }

        public static int CountByStatus(string status, ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                return context.ProCampaignTransactions
                    .Count(e => e.Status == status);
            }
        }

        public static void Insert(ProCampaignTransaction entity, ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                context.ProCampaignTransactions.Add(entity);
                context.SaveChanges();
            }
        }

        public static void Update(ProCampaignTransaction entity, ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static IList<ProCampaignTransaction> Get(ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                return context.ProCampaignTransactions
                    .Include(nameof(ProCampaignTransaction.ProCampaignStatus))
                    .Include(e => e.ProCampaignStatus.Entry)
                    .Include(e => e.ProCampaignStatus.Entry.Participant)
                    .OrderByDescending(e => e.CreatedOn)
                    .ToList();
            }
        }

        public static List<ProCampaignTransaction> Get(int startIndex, int pageSize, ContextType contextType = ContextType.Default)
        {
            return Get(contextType)
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();
        }

        public static IList<ProCampaignTransaction> GetByStatus(string status, ContextType contextType = ContextType.Default)
        {
            return Get(contextType)
                .Where(e => e.Status == status)
                .ToList();
        }
    }
}
