using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Cadbury.Inventor.Core;
using System;

namespace Cadbury.Inventor.Core.Domain.Entities
{
    public class Entry
    {
        /// <summary>
        /// GUID representing the bar's id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Id of Participant
        /// </summary>
        [ForeignKey("Participant")]
        public Guid ParticipantId { get; set; }

        /// <summary>
        /// Entity representing the Participant associated
        /// </summary>
        public virtual Participant Participant { get; set; }

        /// <summary>
        /// Id of ParticipantDataCache
        /// </summary>
        [ForeignKey("ParticipantDataCache")]
        public Guid ParticipantDataCacheId { get; set; }

        /// <summary>
        /// Entity representing the data cache associated
        /// </summary>
        public virtual ParticipantDataCache ParticipantDataCache { get; set; }

        /// <summary>
        /// Name given to the bar
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string BarName { get; set; }

        /// <summary>
        /// Name given to the bar
        /// </summary>
        [MaxLength(500)]
        public string BarDescription { get; set; }

        /// <summary>
        /// Bar text background color.
        /// Could either be an HTML Color code or a color name (not exceeding 8 chars)
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string BarColour { get; set; }

        /// <summary>
        /// First ingredient in list of ingredients
        /// </summary>
        public Guid? Ingredient1Id { get; set; }
        public virtual Ingredient Ingredient1 { get; set; }

        /// <summary>
        /// Second ingredient in list of ingredients. Optional
        /// </summary>
        public Guid? Ingredient2Id { get; set; }
        public virtual Ingredient Ingredient2 { get; set; }

        /// <summary>
        /// Third ingredient in list of ingredients. Optional.
        /// </summary>
        public Guid? Ingredient3Id { get; set; }
        public virtual Ingredient Ingredient3 { get; set; }


        /// <summary>
        /// List of rejected ingredients
        /// </summary>
        public string RejectedIngredients { get; set; }

        /// <summary>
        /// Name of the composited image
        /// </summary>
        [MaxLength(500)]
        public string CompositedImage { get; set; }

        /// <summary>
        /// Name of the composited image share
        /// </summary>
        [MaxLength(500)]
        public string CompositedImageShare { get; set; }

        /// <summary>
        /// Color of the entry
        /// </summary>
        public string Colour { get; set; }

        /// <summary>
        /// Date this entry was created.
        /// Required for auditing purposes.
        /// </summary>
        [Required]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Date this entry was updated.
        /// Required for auditing purposes.
        /// </summary>
        public DateTime? UpdatedOn { get; set; }
    }
}
