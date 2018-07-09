using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cadbury.Inventor.Core.Domain.Entities
{
    public class ParticipantDataCache : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Participant")]
        public Guid ParticipantId { get; set; }

        public virtual Participant Participant { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string MobileNumber { get; set; }

        [Required]
        [MaxLength(10)]
        public string CountryCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public override DateTime CreatedOn { get; set; }

        public override DateTime? UpdatedOn { get; set; }
    }
}
