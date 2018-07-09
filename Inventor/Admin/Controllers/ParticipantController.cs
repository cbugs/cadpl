using Admin.Models;
using Cadbury.Inventor.Core.Domain.Entities;
using Cadbury.Inventor.Core.Domain.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class ParticipantController : BaseController
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
                var dbRecords = ParticipantManager.Get(jtStartIndex, jtPageSize, CurrentContextType).ToList();
                var records = new List<object>();
                foreach (var dbRecord in dbRecords)
                {
                    records.Add(new {
                        Id = dbRecord.Id,
                        EmailHash = dbRecord.EmailHash,
                        ConsumerId = dbRecord.ConsumerId,
                        Entries = dbRecord.Entries.Count(),
                        ParticipantDataCaches = dbRecord.ParticipantDataCaches.Count(),
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
                    TotalRecordCount = ParticipantManager.Count(CurrentContextType)
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