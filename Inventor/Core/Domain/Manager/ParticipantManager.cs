using Cadbury.Inventor.Core.Domain.DAL;
using Cadbury.Inventor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Cadbury.Inventor.Core.Domain.Manager
{
    public class ParticipantManager
    {
        public static int Count(ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                return context.Participants.Count();
            }
        }

        public static void Insert(Participant participant, ParticipantDataCache participantDataCache, Entry entry, ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                participant.ParticipantDataCaches.Add(participantDataCache);

                if (entry.Ingredient1 != null)
                {
                    context.Ingredients.Attach(entry.Ingredient1);
                }
                if (entry.Ingredient2 != null)
                {
                    context.Ingredients.Attach(entry.Ingredient2);
                }
                if (entry.Ingredient3 != null)
                {
                    context.Ingredients.Attach(entry.Ingredient3);
                }
                entry.ParticipantDataCacheId = participantDataCache.Id;
                participant.Entries.Add(entry);

                context.Participants.Add(participant);

                context.SaveChanges();
            }
        }

        public static void InsertFromExistingParticipant(Participant participant, ParticipantDataCache participantDataCache, Entry entry, ContextType contextType = ContextType.Default)
        {
            try
            {
                using (var context = ContextSwitcher.CreateContext(contextType))
                {
                    participantDataCache.ParticipantId = participant.Id;
                    context.ParticipantDataCaches.Add(participantDataCache);

                    if (entry.Ingredient1 != null)
                    {
                        context.Ingredients.Attach(entry.Ingredient1);
                    }
                    if (entry.Ingredient2 != null)
                    {
                        context.Ingredients.Attach(entry.Ingredient2);
                    }
                    if (entry.Ingredient3 != null)
                    {
                        context.Ingredients.Attach(entry.Ingredient3);
                    }
                    entry.ParticipantId = participant.Id;
                    entry.ParticipantDataCacheId = participantDataCache.Id;
                    context.Entries.Add(entry);

                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static void Update(Participant entity, ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static IList<Participant> Get(ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                return context.Participants
                    .Include(nameof(InventorContext.ParticipantDataCaches))
                    .Include(nameof(InventorContext.Entries))
                    .Include(p => p.Entries.Select(e => e.Ingredient1))
                    .Include(p => p.Entries.Select(e => e.Ingredient2))
                    .Include(p => p.Entries.Select(e => e.Ingredient3))
                    .OrderByDescending(e => e.CreatedOn)
                    .ToList();
            }
        }

        public static List<Participant> Get(int startIndex, int pageSize, ContextType contextType = ContextType.Default)
        {
            return Get(contextType)
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();
        }

        public static Participant GetByEmailHash(string emailHash, ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                return context.Participants
                    .Include(nameof(InventorContext.ParticipantDataCaches))
                    .Include(nameof(InventorContext.Entries))
                    .Include(p => p.Entries.Select(e => e.Ingredient1))
                    .Include(p => p.Entries.Select(e => e.Ingredient2))
                    .Include(p => p.Entries.Select(e => e.Ingredient3))
                    .Where(p => p.EmailHash == emailHash)
                    .OrderByDescending(e => e.CreatedOn)
                    .FirstOrDefault();
            }
        }
    }
}
