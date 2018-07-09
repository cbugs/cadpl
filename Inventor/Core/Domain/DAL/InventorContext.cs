using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Cadbury.Inventor.Core.Domain.Entities;

namespace Cadbury.Inventor.Core.Domain.DAL
{
    /// <summary>
    /// Context class that allows access to db info.
    /// Use factory class ContextSwitcher to get the right context.
    /// </summary>
    public class InventorContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        /// <summary>
        /// Constructor - creates a new context class.
        /// Use factory class ContextSwitcher to get the right context.
        /// </summary>
        /// <param name="contextConnectionString"></param>
        public InventorContext(string contextConnectionString) : base(contextConnectionString)
        {
            Database.SetInitializer<InventorContext>(new InventorInitializer());
        }

        /// <summary>
        /// List of Bar entities.
        /// </summary>
        public DbSet<Entry> Entries { get; set; }

        /// <summary>
        /// List of Ingredient entities.
        /// </summary>
        public DbSet<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// List of users. If data is not present, query Consultix database
        /// </summary>
        public DbSet<Participant> Participants { get; set; }

        /// <summary>
        /// List of existing bar combinations
        /// </summary>
        public DbSet<ExistingBarCombination> ExistingBarCombinations { get; set; }

        /// <summary>
        /// List of participants with their PII still in cache
        /// </summary>
        public DbSet<ParticipantDataCache> ParticipantDataCaches { get; set; }

        /// <summary>
        /// List of statuses from Consultix after a transaction
        /// </summary>
        public DbSet<ProCampaignStatus> ProCampaignStatuses { get; set; }

        /// <summary>
        /// List of transactions sent to Consultix
        /// </summary>
        public DbSet<ProCampaignTransaction> ProCampaignTransactions { get; set; }

    }
}
