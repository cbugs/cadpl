using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadbury.Inventor.Core.Domain.DTO
{
    public class ParticipationEntry
    {
        /// <summary>
        /// The participant's first name(s)
        /// </summary>
        public string FirstName { get; set; }


        /// <summary>
        /// The participant's last name(s)
        /// </summary>
        public string LastName { get; set; }



        /// <summary>
        /// User's email address.
        /// </summary>
        public string Email { get; set; }



        /// <summary>
        /// The participant's mobile phone no
        /// </summary>
        public string MobileNumber { get; set; }



        /// <summary>
        /// Specifies user's country: UK or IE only. Use uppercase values
        /// </summary>
        public string CountryCode { get; set; }



        /// <summary>
        /// User's city
        /// </summary>
        public string City { get; set; }



        /// <summary>
        /// User's date of birth
        /// </summary>
        public DateTime BirthDate { get; set; }



        /// <summary>
        /// First chosen ingredient
        /// </summary>
        public string Ingredient1 { get; set; }



        /// <summary>
        /// Second chosen ingredient
        /// </summary>

        public string Ingredient2 { get; set; }



        /// <summary>
        /// Third chosen ingredient
        /// </summary>
        public string Ingredient3 { get; set; }



        /// <summary>
        /// Name of the bar
        /// </summary>
        public string BarName { get; set; }



        /// <summary>
        /// Colour (HTML Colour Code) of the background where the name of the bar is written
        /// </summary>
        public string BarNameBackground { get; set; }



        /// <summary>
        /// User's description of why his/her bar is a winner
        /// </summary>

        public string BarDescription { get; set; }



        /// <summary>
        /// True if the participant wants to receive email updates from
        /// Cadbury
        /// </summary>
        public bool EmailSubscription { get; set; }



        /// <summary>
        /// Whether the user has agreed to T&C
        /// </summary>
        public bool TCAgreement { get; set; }



        /// <summary>
        /// Response from ReCaptcha
        /// </summary>
        public string ReCaptchaResponse { get; set; }



        /// <summary>
        /// Privacy policy version to which user agreed to.
        /// Read more in GDPR document.
        /// </summary>
        public string PrivacyPolicyVersion { get; set; }


    }
}
