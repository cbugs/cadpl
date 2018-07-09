using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Cadbury.Inventor.Core.Domain.Entities;
using Cadbury.Inventor.Core.Domain.Manager;
using Cadbury.Inventor.Core.DTO;

namespace Cadbury.Inventor.Core.Module
{
    public class BarColourCheckModule : ModuleBase, IModule
    {
        public Ingredient Ingredient1 { get; set; }
        public Ingredient Ingredient2 { get; set; }
        public Ingredient Ingredient3 { get; set; }

        public ResultDTO Process()
        {
            string colour = String.Empty;

            IList<Ingredient> ingredients = GenerateIngredientsList(Ingredient1, Ingredient2, Ingredient3);
            IList<Guid> ingredientGuids = GenerateIngredientGuidsList(Ingredient1, Ingredient2, Ingredient3);

            // Colour is red if all 3 ingredients are red
            IList<string> colours = ingredients.Select(i => i.Colour).Distinct().ToList();
            if (ingredients.Count == 3 && colours.Count == 1 && Ingredient1.Colour == ColorKeys.RED)
            {
                colour = ColorKeys.RED;
            }

            // Colour is red if an entry corresponds from ExistingBarCombination
            IList<ExistingBarCombination> existingBarCombinations = ExistingBarCombinationManager.Get();
            foreach(ExistingBarCombination existingBarCombination in existingBarCombinations)
            {
                IList<Guid> ingredientGuidsFromExistingBarCombination = GenerateIngredientGuidsList(existingBarCombination.Ingredient1, existingBarCombination.Ingredient2, existingBarCombination.Ingredient3);
                if (ingredientGuids.Count() == ingredientGuidsFromExistingBarCombination.Count())
                {
                    if (ingredientGuids.All(ingredientGuidsFromExistingBarCombination.Contains))
                    {
                        colour = ColorKeys.RED;
                        break;
                    }
                }
            }

            Result.HttpStatusCode = HttpStatusCode.OK;
            Result.Meta = new
            {
                Colour = colour
            };

            return Result;
        }

        private IList<Ingredient> GenerateIngredientsList(Ingredient ingredient1, Ingredient ingredient2, Ingredient ingredient3)
        {
            IList<Ingredient> ingredients = new List<Ingredient>();
            if (ingredient1 != null)
            {
                ingredients.Add(ingredient1);
            }
            if (ingredient2 != null)
            {
                ingredients.Add(ingredient2);
            }
            if (ingredient3 != null)
            {
                ingredients.Add(ingredient3);
            }

            return ingredients;
        }

        private IList<Guid> GenerateIngredientGuidsList(Ingredient ingredient1, Ingredient ingredient2, Ingredient ingredient3)
        {
            IList<Guid> ingredientGuids = new List<Guid>();
            if (ingredient1 != null)
            {
                ingredientGuids.Add(ingredient1.Id);
            }
            if (ingredient2 != null)
            {
                ingredientGuids.Add(ingredient2.Id);
            }
            if (ingredient3 != null)
            {
                ingredientGuids.Add(ingredient3.Id);
            }

            return ingredientGuids;
        }
    }
}
