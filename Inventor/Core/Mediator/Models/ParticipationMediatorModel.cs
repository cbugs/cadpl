using System.Collections.Generic;

namespace Cadbury.Inventor.Core.Mediator.Models
{
    public class ParticipationMediatorModel : IMediatorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string BirthDate { get; set; }
        public string Ingredient1 { get; set; }
        public string Ingredient2 { get; set; }
        public string Ingredient3 { get; set; }
        public string RejectedIngredients { get; set; }
        public string BarName { get; set; }
        public string BarNameBackground { get; set; }
        public string BarDescription { get; set; }
        public bool TCAgreement { get; set; }
        public bool EmailSubscription { get; set; }
        public string PrivacyPolicyVersion { get; set; }
        public string ReCaptchaResponse { get; set; }
    }
}
