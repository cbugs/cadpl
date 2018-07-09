using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cadbury.Inventor.Core.Domain.Entities
{
    public class IngredientGroup
    {
        /// <summary>
        /// Id of the group - simply returns the name of the ingredient group, e.g. Herbs & Spices
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(24)]
        public string Id { get; set; }


        /// <summary>
        /// The list of ingredients that fall under this group
        /// </summary>
        public virtual ICollection<Ingredient> Ingredients { get; set; }

    }
}
