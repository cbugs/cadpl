using Cadbury.Inventor.Core.Domain.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cadbury.Inventor.Core.Domain.Manager
{
    public class IngredientManager
    {
        public static int Count(ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                return context.Ingredients.Count();
            }
        }

        public static void Insert(Entities.Ingredient entity, ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                context.Ingredients.Add(entity);
                context.SaveChanges();
            }
        }

        public static List<Entities.Ingredient> Get(ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                return (
                    from ingredient 
                    in context.Ingredients
                    orderby ingredient.CreatedOn descending
                    select ingredient
                ).ToList();
            }
        }

        public static List<Entities.Ingredient> Get(int startIndex, int pageSize, ContextType contextType = ContextType.Default)
        {
            return Get(contextType)
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();
        }

        public static Entities.Ingredient GetIngredient(string name, ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                return (
                    from ingredientToSearch 
                    in context.Ingredients
                    where ingredientToSearch.Name == name
                    select ingredientToSearch
                ).FirstOrDefault();
            }
        }

        public static Entities.Ingredient GetIngredient(Guid id, ContextType contextType = ContextType.Default)
        {
            using (var context = ContextSwitcher.CreateContext(contextType))
            {
                return (
                    from ingredientToSearch 
                    in context.Ingredients
                    where ingredientToSearch.Id == id
                    select ingredientToSearch
                ).FirstOrDefault();
            }
        }

        public static Entities.Ingredient GetIngredientFromName(string name, ContextType contextType = ContextType.Default)
        {
            using (var ctx = ContextSwitcher.CreateContext(contextType))
            {
                return (from ingredientToSearch in ctx.Ingredients where ingredientToSearch.Name.ToLower() == name.ToLower() select ingredientToSearch).FirstOrDefault();
            }
        }
    }
}
