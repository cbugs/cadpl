using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Cadbury.Inventor.Core.Domain.DTO;

namespace Cadbury.Inventor.Core.Domain.Entities
{
    public class Participant
    {
        /// <summary>
        /// GUID representing the user's id in the database
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Hash of email address - used to retrieve information from Consultix
        /// </summary>
        [MaxLength(64)]
        [Required]
        public string EmailHash { get; set; }

        /// <summary>
        /// Consultix consumer id
        /// </summary>
        [MaxLength(50)]
        public string ConsumerId { get; set; }

        /// <summary>
        /// Date this user was created.
        /// Required for auditing purposes.
        /// </summary>
        [Required]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Date this user was updated.
        /// Required for auditing purposes.
        /// </summary>
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// List of entries associated to the participant
        /// </summary>
        public virtual IList<Entry> Entries { get; set; }

        /// <summary>
        /// List of data caches associated to the participant
        /// </summary>
        public virtual IList<ParticipantDataCache> ParticipantDataCaches { get; set; }

        /// <summary>
        /// Constuctor
        /// </summary>
        public Participant()
        {
            Entries = new List<Entry>();
            ParticipantDataCaches = new List<ParticipantDataCache>();
        }
    }
}
