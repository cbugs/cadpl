using Admin.Models;
using Cadbury.Inventor.Core.Domain.Entities;
using Cadbury.Inventor.Core.Domain.Manager;
using Cadbury.Inventor.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class IngredientController : BaseController
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
                var dbRecords = IngredientManager.Get(jtStartIndex, jtPageSize, CurrentContextType).ToList();
                var records = new List<object>();
                foreach (var dbRecord in dbRecords)
                {
                    records.Add(new
                    {
                        Id = dbRecord.Id,
                        Name = dbRecord.Name,
                        Colour = dbRecord.Colour,
                        Category = dbRecord.Category,
                        PackImagePath = dbRecord.PackImagePath,
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
                    TotalRecordCount = IngredientManager.Count(CurrentContextType)
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
        public JsonResult InsertFixtures()
        {
            try
            {
                InsertIngredients();
                InsertExistingBarCombinations();

                return Json(new JTableResult
                {
                    Result = "OK"
                });
            }
            catch(Exception ex)
            {
                return Json(new JTableResult
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }

        private void InsertIngredients()
        {
            if (IngredientManager.Get(CurrentContextType).Count() != 0)
            {
                throw new Exception("Ingredients already inserted !");
            }

            string filename = System.Configuration.ConfigurationManager.AppSettings["IngredientList"];
            string fullFilename = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/" + filename);

            try
            {
                string[] fileLines = System.IO.File.ReadAllLines(fullFilename);
                foreach (string s in fileLines)
                {
                    string[] ingredientData = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string groupName = ingredientData[0];
                    int groupCount = int.Parse(ingredientData[1]);
                    while (groupCount > 0)
                    {
                        Ingredient newIngredient = new Ingredient();
                        newIngredient.Id = System.Guid.NewGuid();
                        newIngredient.Name = ingredientData[groupCount + 1];
                        if (newIngredient.Name.EndsWith("[A]"))
                        {
                            newIngredient.Colour = ColorKeys.AMBER;
                            newIngredient.Name = newIngredient.Name.Replace("[A]", "");
                        }
                        else if (newIngredient.Name.EndsWith("[R]"))
                        {
                            newIngredient.Colour = ColorKeys.RED;
                            newIngredient.Name = newIngredient.Name.Replace("[R]", "");
                        }
                        else
                        {
                            newIngredient.Colour = ColorKeys.GREEN;
                        }
                        newIngredient.Category = groupName;
                        newIngredient.PackImagePath = "";
                        newIngredient.CreatedOn = DateTime.UtcNow;
                        groupCount--;
                        IngredientManager.Insert(newIngredient, CurrentContextType);
                    }
                }
            }
            catch (System.IO.FileNotFoundException ioExcp)
            {
                throw new Exception("Ingredients file could not be loaded.", ioExcp);
            }
            catch (System.IO.IOException ioGeneralExcp)
            {
                throw new System.IO.IOException("An IO Exception occurred while reading the ingredients file.", ioGeneralExcp);
            }
            catch (Exception excp)
            {
                throw new Exception("A general exception occurred while reading the ingredients file.", excp);
            }
        }

        private void InsertExistingBarCombinations()
        {
            if (ExistingBarCombinationManager.Get(CurrentContextType).Count() != 0)
            {
                throw new Exception("Combinations already inserted !");
            }

            string filename = System.Configuration.ConfigurationManager.AppSettings["DelistedList"];
            string fullFilename = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/" + filename);

            try
            {
                string[] fileLines = System.IO.File.ReadAllLines(fullFilename);
                foreach (string s in fileLines)
                {
                    string[] ingredientData = s.Split(new char[] { ',' });
                    ExistingBarCombination existingBar = new ExistingBarCombination();
                    existingBar.BarName = ingredientData[0];
                    existingBar.Ingredient1 = IngredientManager.GetIngredient(ingredientData[1], CurrentContextType);
                    existingBar.CreatedOn = DateTime.UtcNow;
                    if (!string.IsNullOrEmpty(ingredientData[2]))
                    {
                        existingBar.Ingredient2 = IngredientManager.GetIngredient(ingredientData[2], CurrentContextType);
                    }

                    if (!string.IsNullOrEmpty(ingredientData[3]))
                    {
                        existingBar.Ingredient3 = IngredientManager.GetIngredient(ingredientData[3], CurrentContextType);
                    }
                    ExistingBarCombinationManager.Insert(existingBar, CurrentContextType);
                }
            }
            catch (System.IO.FileNotFoundException ioExcp)
            {
                throw new Exception("Ingredients file could not be loaded.", ioExcp);
            }
            catch (System.IO.IOException ioGeneralExcp)
            {
                throw new System.IO.IOException("An IO Exception occurred while reading the ingredients file.", ioGeneralExcp);
            }
            catch (Exception excp)
            {
                throw new Exception("A general exception occurred while reading the ingredients file.", excp);
            }
        }
    }
}