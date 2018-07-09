using Cadbury.Inventor.Core.Domain.DAL;
using Cadbury.Inventor.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Cadbury.Inventor.Core.Domain.Manager
{
    public class ExistingBarCombinationManager
    {
        public static int Count(ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                return context.ExistingBarCombinations.Count();
            }
        }

        public static void Insert(ExistingBarCombination existingBarCombination, ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                if (existingBarCombination.Ingredient1 != null)
                {
                    context.Ingredients.Attach(existingBarCombination.Ingredient1);
                }
                if (existingBarCombination.Ingredient2 != null)
                {
                    context.Ingredients.Attach(existingBarCombination.Ingredient2);
                }
                if (existingBarCombination.Ingredient3 != null)
                {
                    context.Ingredients.Attach(existingBarCombination.Ingredient3);
                }

                context.ExistingBarCombinations.Add(existingBarCombination);

                context.SaveChanges();
            }
        }

        public static IList<ExistingBarCombination> Get(ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                return context.ExistingBarCombinations
                    .Include(nameof(ExistingBarCombination.Ingredient1))
                    .Include(nameof(ExistingBarCombination.Ingredient2))
                    .Include(nameof(ExistingBarCombination.Ingredient3))
                    .OrderByDescending(e => e.CreatedOn)
                    .ToList();
            }
        }

        public static List<ExistingBarCombination> Get(int startIndex, int pageSize, ContextType contextType = ContextType.Default)
        {
            return Get(contextType)
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();
        }
    }
}
