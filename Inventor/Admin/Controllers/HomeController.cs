using Admin.Models;
using Cadbury.Inventor.Core.Domain.Manager;
using Cadbury.Inventor.Core.DTO;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View(new HomeStatsViewModel()
            {
                Participants = ParticipantManager.Count(CurrentContextType),
                Entries = EntryManager.Count(CurrentContextType),
                Ingredients = IngredientManager.Count(CurrentContextType),
                ExistingBarCombinations = ExistingBarCombinationManager.Count(CurrentContextType),
                NewProCampaignTransactions = ProCampaignTransactionManager.CountByStatus(ProCampaignTransactionStatusKeys.NEW, CurrentContextType),
                SentProCampaignTransactions = ProCampaignTransactionManager.CountByStatus(ProCampaignTransactionStatusKeys.SENT, CurrentContextType)
            });
        }
    }
}