using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadbury.Inventor.Core.Domain.DTO
{
    public class Entry
    {
        /// <summary>
        /// Bar's id. GUID.
        /// </summary>
        public string Id { get; set; }



        /// <summary>
        /// Participant who created the bar. This is a GUID.
        /// </summary>
        public string Owner { get; set; }



        /// <summary>
        /// Name of the Bar
        /// </summary>
        public string Name { get; set; }



        /// <summary>
        /// Bar text background color.
        /// Could either be an HTML Color code or a color name (not exceeding 8 chars)
        /// </summary>
        public string TextBackgroundColor { get; set; }



        /// <summary>
        /// First ingredient for a bar. Required field.
        /// </summary>
        public string Ingredient1 { get; set; }



        /// <summary>
        /// Second ingredient for a bar.
        /// </summary>
        public string Ingredient2 { get; set; }



        /// <summary>
        /// Third ingredient for a bar. 
        /// </summary>
        public string Ingredient3 { get; set; }



        /// <summary>
        /// Participant's input for "Please explain why your bar is a winner bar"
        /// </summary>
        public string WinnerBarDescription { get; set; }


        /// <summary>
        /// Flag is set to true if this bar has been flagged for whatever reason
        /// </summary>
        public bool AdminFlag { get; set; }
    }
}
