using System;
using System.Collections.Generic;
using System.Net;
using Cadbury.Inventor.Core.Domain.Entities;
using Cadbury.Inventor.Core.Domain.Manager;
using Cadbury.Inventor.Core.DTO;

namespace Cadbury.Inventor.Core.Module
{
    /// <summary>
    /// Check if ingredients exist and returns the ingredients
    /// </summary>
    public class IngredientsValidationModule : ModuleBase, IModule
    {
        public string Ingredient1Raw { get; set; }
        public string Ingredient2Raw { get; set; }
        public string Ingredient3Raw { get; set; }

        public ResultDTO Process()
        {
            if (String.IsNullOrEmpty(Ingredient1Raw))
            {
                Result.Code = CodeKeys.EMPTY_INGREDIENTS;
                return Result;
            }

            Ingredient ingredient1 = null;
            Ingredient ingredient2 = null;
            Ingredient ingredient3 = null;

            ingredient1 = IngredientManager.GetIngredientFromName(Ingredient1Raw);
            if (ingredient1 == null)
            {
                Result.Code = CodeKeys.INVALID_INGREDIENT_1;
                return Result;
            }

            if (!String.IsNullOrEmpty(Ingredient2Raw))
            {
                ingredient2 = IngredientManager.GetIngredientFromName(Ingredient2Raw);
                if (ingredient2 == null)
                {
                    Result.Code = CodeKeys.INVALID_INGREDIENT_2;
                    return Result;
                }
            }

            if (!String.IsNullOrEmpty(Ingredient3Raw))
            {
                ingredient3 = IngredientManager.GetIngredientFromName(Ingredient3Raw);
                if (ingredient3 == null)
                {
                    Result.Code = CodeKeys.INVALID_INGREDIENT_3;
                    return Result;
                }
            }

            Result.HttpStatusCode = HttpStatusCode.OK;

            Result.Meta = new
            {
                Ingredient1 = ingredient1,
                Ingredient2 = ingredient2,
                Ingredient3 = ingredient3
            };

            return Result;
        }
    }
}
