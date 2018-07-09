using Cadbury.Inventor.Core.Domain.Entities;
using System;

namespace Cadbury.Inventor.Core.Domain.DTO
{
    public class Ingredient
    {
        /// <summary>
        /// Id (and name) of the ingredient. E.g. Peach
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// Blank constructor
        /// </summary>
        public Ingredient() { }


        /// <summary>
        /// Initializes this object using an Ingredient entity object
        /// </summary>
        public Ingredient(Entities.Ingredient ingredient)
        {
            this.Id = ingredient.Id;
            
        }


        /// <summary>
        /// Type of ingredient (Uppercase only). Used to classify ingredients for filtering in back-end.
        /// Use an uppercase value for this value.
        /// Current requirements list three colours:
        /// "G" - Green (safe)
        /// "O" - Orange (possible later use)
        /// "R" - Red (will require admin filtering)
        /// </summary>
        public char Color { get; set; }
    }
}
