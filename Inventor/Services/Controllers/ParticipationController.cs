using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using Cadbury.Inventor.Core.DTO;
using Services.Models;
using Cadbury.Inventor.Core.Mediator;
using Cadbury.Inventor.Core.Mediator.Models;
using Services.Filters;
using Cadbury.Inventor.Core.Domain.Entities;
using System.Web;
using Cadbury.Inventor.Core.WordTree;
using Cadbury.Inventor.Core.Domain.Manager;
using Cadbury.Inventor.Core.Module;
using System.Configuration;
using Cadbury.Inventor.Core.Elmah;

namespace Services.Controllers
{
    [RoutePrefix("")]
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    public class ParticipationController : ApiController
    {
        /// <summary>
        /// Method to check if campaign has started or ended
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Ping")]
        [ValidateSiteStatus]
        public IHttpActionResult Ping()
        {
            // Returns OK by default. In case the campaign has not yet started or has ended, the 
            // ValidateSiteStatus filter will catch that and return the correct responses accordingly
            return StatusCode(HttpStatusCode.OK);
        }

        /// <summary>
        /// Method to check if a word is valid
        /// </summary>
        /// <param name="wordViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Verify")]
        [ValidateSiteStatus]
        public IHttpActionResult Verify(VerifyViewModel input)
        {
            try
            {
                var mediator = new TextCheckMediator(new TextCheckMediatorModel()
                {
                    Text = input.BarName,
                    WordTree = (WordTree)HttpContext.Current.Application["WordTree"]
                });
                var result = mediator.Process();
                if(result.HttpStatusCode != HttpStatusCode.NoContent)
                {
                    return Content(result.HttpStatusCode, result);
                }

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);

                return Content(HttpStatusCode.InternalServerError, new ResultDTO()
                {
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("Participate")]
        [ValidateModel]
        [ValidateSiteStatus]
        public IHttpActionResult Participate(ParticipateViewModel input)
        {
            try
            {
                var textCheckMediator = new TextCheckMediator(new TextCheckMediatorModel()
                {
                    Text = input.BarName,
                    WordTree = (WordTree)HttpContext.Current.Application["WordTree"]
                });
                var textCheckResult = textCheckMediator.Process();
                if (textCheckResult.HttpStatusCode != HttpStatusCode.NoContent)
                {
                    return Content(textCheckResult.HttpStatusCode, textCheckResult);
                }

                var participationMediator = new ParticipationMediator(new ParticipationMediatorModel()
                {
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    Email = input.Email,
                    MobileNumber = input.MobileNumber,
                    City = input.City,
                    CountryCode = input.CountryCode,
                    BirthDate = input.BirthDate,
                    Ingredient1 = input.Ingredient1,
                    Ingredient2 = input.Ingredient2,
                    Ingredient3 = input.Ingredient3,
                    RejectedIngredients = input.RejectedIngredients == null ? "" : String.Join(", ", input.RejectedIngredients),
                    BarName = input.BarName,
                    BarNameBackground = input.BarNameBackground,
                    BarDescription = input.BarDescription,
                    TCAgreement = input.TCAgreement,
                    EmailSubscription = input.EmailSubscription,
                    PrivacyPolicyVersion = input.PrivacyPolicyVersion,
                    ReCaptchaResponse = input.ReCaptchaResponse
                });

                var participationResult = participationMediator.Process();
                if (participationResult.HttpStatusCode != HttpStatusCode.OK)
                {
                    return Content(participationResult.HttpStatusCode, participationResult);
                }

                return Content(participationResult.HttpStatusCode, participationResult.Meta);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);

                return Content(HttpStatusCode.InternalServerError, new ResultDTO()
                {
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Message = String.Format(
                        "{0} {1}. {2}",
                        ex.Message,
                        ex.Source,
                        ex.StackTrace
                    )
                });
            }
        }

        [HttpGet]
        [Route("LegalTexts")]
        public IHttpActionResult LegalTexts(string locale = "")
        {
            try
            {
                var consultixTermsAndConditionsModule = new ConsultixTermsAndConditionsModule()
                {
                    TermsAndConditionsName = "CBUK180501_TermsAndConditions",
                    Locale = (locale == String.Empty) ? CountryKeys.UK : locale,
                    Environment = ConfigurationManager.AppSettings["Environment"]
                };
                ResultDTO consultixTermsAndConditionsResult = consultixTermsAndConditionsModule.Process();
                if (consultixTermsAndConditionsResult.HttpStatusCode != HttpStatusCode.OK)
                {
                    return Content(consultixTermsAndConditionsResult.HttpStatusCode, consultixTermsAndConditionsResult);
                }

                return Content(consultixTermsAndConditionsResult.HttpStatusCode, consultixTermsAndConditionsResult.Meta);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);

                return Content(HttpStatusCode.InternalServerError, new ResultDTO()
                {
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                });
            }
        }

        /*
        [HttpPost]
        [Route("TestFilePurge")]
        public object TestFilePurge()
        {
            var filePurgeModule = new FilePurgeModule()
            {
                DaysPeriodFromNow = 3
            };
            return filePurgeModule.Process();
        }

        /*
        [HttpPost]
        [Route("TestHangfireConsultix")]
        public object TestHangfireConsultix()
        {
            var consultixRetryModule = new ConsultixRetryModule();
            consultixRetryModule.Process();

            return new
            {
                Message = "OK"
            };
        }

        [HttpPost]
        [Route("TestHangfirePurge")]
        public object TestHangfirePurge()
        {
            var dataPurgeModule = new DataPurgeModule()
            {
                DaysPeriodFromNow = Convert.ToInt32(ConfigurationManager.AppSettings["PurgePeriod"])
            };
            dataPurgeModule.Process();

            return new
            {
                Message = "OK"
            };
        }
        */
    }
}