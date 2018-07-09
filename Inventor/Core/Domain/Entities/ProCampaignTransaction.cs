using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadbury.Inventor.Core.Domain.Entities
{
    public class ProCampaignTransaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("ProCampaignStatus")]
        public Guid ProCampaignStatusId { get; set; }
        public ProCampaignStatus ProCampaignStatus { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        [MaxLength(100)]
        [Required]
        public string Database { get; set; }

        [Column(TypeName = "text")]
        public string TransactionObject { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
