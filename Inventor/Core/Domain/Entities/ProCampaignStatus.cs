using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadbury.Inventor.Core.Domain.Entities
{
    public class ProCampaignStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Entry")]
        public Guid EntryId { get; set; }
        public Entry Entry { get; set; }

        [Required]
        public bool IsSuccessful { get; set; }

        public int ResponseCode { get; set; }

        [Column(TypeName = "text")]
        public string ResponseText { get; set; }

        public int HttpStatusCode { get; set; }

        [Column(TypeName = "text")]
        public string HttpStatusMessage { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
