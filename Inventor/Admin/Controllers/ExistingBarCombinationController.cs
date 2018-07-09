using Admin.Models;
using Cadbury.Inventor.Core.Domain.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class ExistingBarCombinationController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var dbRecords = ExistingBarCombinationManager.Get(jtStartIndex, jtPageSize, CurrentContextType).ToList();
                var records = new List<object>();
                foreach (var dbRecord in dbRecords)
                {
                    records.Add(new {
                        BarName = dbRecord.BarName,
                        Ingredient1 = dbRecord.Ingredient1,
                        Ingredient2 = dbRecord.Ingredient2,
                        Ingredient3 = dbRecord.Ingredient3,
                        CreatedOn = String.Format(
                            "{0:yyyy-MM-dd hh:mm}",
                            dbRecord.CreatedOn
                        )
                    });
                }

                return Json(new JTableResult
                {
                    Result = "OK",
                    Records = records,
                    TotalRecordCount = ExistingBarCombinationManager.Count(CurrentContextType)
                });
            }
            catch (Exception ex)
            {
                return Json(new JTableResult
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }
    }
}