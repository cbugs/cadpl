using Cadbury.Inventor.Core.Domain.DAL;
using Cadbury.Inventor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cadbury.Inventor.Core.Domain.Manager
{
    public class EntryManager
    {
        public static int Count(ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                return context.Entries.Count();
            }
        }

        public static IList<Entry> Get(ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                return context.Entries
                    .Include(nameof(Entry.Ingredient1))
                    .Include(nameof(Entry.Ingredient2))
                    .Include(nameof(Entry.Ingredient3))
                    .Include(nameof(Entry.Participant))
                    .Include(nameof(Entry.ParticipantDataCache))
                    .OrderByDescending(e => e.CreatedOn)
                    .ToList();
            }
        }

        public static List<Entry> Get(int startIndex, int pageSize, ContextType contextType = ContextType.Default)
        {
            return Get(contextType)
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();
        }

        public static IList<Entry> GetByAdminFilter(string input, ContextType contextType = ContextType.Default)
        {
            Guid id = Guid.Empty;
            Guid.TryParse(input, out id);

            return Get(contextType)
                .Where(
                    e => e.ParticipantId == id 
                    || e.ParticipantDataCache.Email == input 
                    || e.Participant.EmailHash == input
                    || e.BarName == input)
                .ToList();
        }
    }
}
