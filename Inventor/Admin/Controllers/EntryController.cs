using Admin.Models;
using Cadbury.Inventor.Core.Domain.Entities;
using Cadbury.Inventor.Core.Domain.Manager;
using Cadbury.Inventor.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class EntryController : BaseController
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
                var dbRecords = new List<Entry>();
                var totalRecordCount = 0;

                if (!String.IsNullOrEmpty(Filter))
                {
                    dbRecords = EntryManager.GetByAdminFilter(StringUtils.HashSHA256(Filter.ToLower()), CurrentContextType).ToList();
                    totalRecordCount = dbRecords.Count();
                }
                else
                {
                    dbRecords = EntryManager.Get(jtStartIndex, jtPageSize, CurrentContextType).ToList();
                    totalRecordCount = EntryManager.Count(CurrentContextType);
                }

                var records = new List<object>();
                foreach (var dbRecord in dbRecords)
                {
                    records.Add(new {
                        Id = dbRecord.Id,
                        ParticipantId = dbRecord.Participant.Id,
                        FirstName = dbRecord.ParticipantDataCache.FirstName,
                        LastName = dbRecord.ParticipantDataCache.LastName,
                        Email = dbRecord.ParticipantDataCache.Email,
                        ConsumerId = dbRecord.Participant.ConsumerId,
                        BarName = dbRecord.BarName,
                        BarDescription = dbRecord.BarDescription,
                        BarColour = dbRecord.BarColour,
                        Ingredient1 = dbRecord.Ingredient1,
                        Ingredient2 = dbRecord.Ingredient2,
                        Ingredient3 = dbRecord.Ingredient3,
                        Colour = dbRecord.Colour,
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
                    TotalRecordCount = totalRecordCount
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

        [HttpPost]
        public void UpdateFilter(string filter)
        {
            Filter = filter;
        }

        [HttpGet]
        public void CSVExport(string filter)
        {
            var filename = String.Format(
                "Mdlz.CadburyInventor.Export.{0:yyyy-MM-dd-hh-mm-ss}.csv",
                DateTime.UtcNow
            );

            StringWriter sw = new StringWriter();
            sw.WriteLine("\"Date\",\"ParticipantId\",\"ConsumerId\",\"BarName\",\"BarDescription\",\"BarColour\",\"RejectedIngredients\",\"Ing. 1\",\"Ing. 2\",\"Ing. 3\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            Response.ContentType = "text/csv";

            IList<Entry> entries = EntryManager.Get(CurrentContextType);
            foreach (var entry in entries)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\"",
                    entry.CreatedOn,
                    entry.ParticipantId,
                    entry.Participant.ConsumerId,
                    entry.BarName,
                    entry.BarDescription,
                    entry.BarColour,
                    entry.RejectedIngredients,
                    (entry.Ingredient1 == null) ? "" : entry.Ingredient1.Name,
                    (entry.Ingredient2 == null) ? "" : entry.Ingredient2.Name,
                    (entry.Ingredient3 == null) ? "" : entry.Ingredient3.Name
                ));
            }

            Response.Write(sw.ToString());

            Response.End();
        }
    }
}