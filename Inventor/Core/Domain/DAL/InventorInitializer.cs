using System.Collections.Generic;
using System.Data.Entity;
using Cadbury.Inventor.Core.Domain.Entities;
using Cadbury.Inventor.Core;

namespace Cadbury.Inventor.Core.Domain.DAL
{
    public class InventorInitializer : CreateDatabaseIfNotExists<InventorContext>
    {
        //adds the ingredients to the database
        protected override void Seed(InventorContext context)
        {
            // Load data from CSV from here
            // new IngredientCSVReader(context);
            // new DelistedCSVReader(context);

            // Save all changes
            context.SaveChanges();
        }
    }
}