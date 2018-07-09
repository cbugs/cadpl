using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Cadbury.Inventor.Core.Domain.Entities
{
    public class Ingredient
    {
        /// <summary>
        /// GUID representing the ingredient's id in the database
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the ingredient. E.g. Peach
        /// </summary>
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Type of ingredient (Uppercase only). Used to classify ingredients by colour for filtering.
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string Colour { get; set; }

        /// <summary>
        /// Ingredient's category
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// Path to image
        /// </summary>
        [MaxLength(400)]
        public string PackImagePath { get; set; }

        /// <summary>
        /// Date this ingredient was created.
        /// Required for auditing purposes.
        /// </summary>
        [Required]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Date this ingredient was updated.
        /// Required for auditing purposes.
        /// </summary>
        public DateTime? UpdatedOn { get; set; }
    }
}
