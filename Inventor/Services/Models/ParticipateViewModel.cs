using Cadbury.Inventor.Core.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class ParticipateViewModel
    {
        [Required(ErrorMessage = CodeKeys.MISSING_FIRSTNAME)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = CodeKeys.MISSING_LASTNAME)]
        public string LastName { get; set; }

        [Required(ErrorMessage = CodeKeys.MISSING_EMAIL)]
        [EmailAddress(ErrorMessage = CodeKeys.INVALID_EMAIL)]
        public string Email { get; set; }

        [Required(ErrorMessage = CodeKeys.MISSING_MOBILE_NUMBER)]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = CodeKeys.MISSING_CITY)]
        public string City { get; set; }

        [Required(ErrorMessage = CodeKeys.MISSING_COUNTRY_CODE)]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = CodeKeys.MISSING_BIRTH_DATE)]
        public string BirthDate { get; set; }

        public string Ingredient1 { get; set; }

        public string Ingredient2 { get; set; }

        public string Ingredient3 { get; set; }

        public List<string> RejectedIngredients { get; set; }

        public string BarName { get; set; }

        public string BarNameBackground { get; set; }

        public string BarDescription { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = CodeKeys.TC_NOT_ACCEPTED)]
        public bool TCAgreement { get; set; }

        public bool EmailSubscription { get; set; }

        public string PrivacyPolicyVersion { get; set; }

        public string ReCaptchaResponse { get; set; }
    }
}