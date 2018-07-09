using System;
using System.ComponentModel.DataAnnotations;


namespace Cadbury.Inventor.Core.Domain.Entities
{
    public abstract class Base
    {
        /// <summary>
        /// Date the object was created
        /// </summary>
        [Required]
        public abstract DateTime CreatedOn { get; set; }



        /// <summary>
        /// Date the object was last updated
        /// </summary>
        public abstract DateTime? UpdatedOn { get; set; }
    }
}
