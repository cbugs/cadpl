using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Cadbury.Inventor.Core.Domain.Entities
{

    public class ExistingBarCombination
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(200)]
        [Key]
        public string BarName { get; set; }

        public Guid? Ingredient1Id { get; set; }
        public virtual Ingredient Ingredient1 { get; set; }

        public Guid? Ingredient2Id { get; set; }
        public virtual Ingredient Ingredient2 { get; set; }

        public Guid? Ingredient3Id { get; set; }
        public virtual Ingredient Ingredient3 { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
