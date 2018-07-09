using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadbury.Inventor.Core.Domain.DAL
{
    public static class ContextSwitcher
    {
        ///// <summary>
        ///// Dictionary of Context types vs connection strings
        ///// </summary>
        //private static Dictionary<ContextType, string> contextConnections;


        ///// <summary>
        ///// Initialize the context connection string names
        ///// </summary>
        ///// <param name="connections"></param>
        //public static void InitializeContexts(Dictionary<ContextType, string> connections)
        //{
        //    contextConnections = connections;
        //}


        /// <summary>
        /// Factory method that needs to be called whenever a new context is needed.
        /// If no parameters are specified default context is created as Development.
        /// </summary>
        /// <param name="contextToUse">The type of context to use, each context has its own connection string with a different db specified</param>
        /// <returns>A new InventorContext with the context specified</returns>
        public static InventorContext CreateContext(ContextType contextToUse = ContextType.Default)
        {
            string contextString = contextToUse.ToString();
            return new InventorContext(contextString);
        }

        /// <summary>
        /// Retrieves the current context that the user is subscribed to.
        /// </summary>
        /// <returns>Current context stored in session, or default context if none is specified</returns>
        public static ContextType GetUserContext(string currentContext)
        {
            if (string.IsNullOrEmpty(currentContext))
            {
                return ContextType.Default;
            }
            else
            {
                if (Enum.IsDefined(typeof(ContextType), currentContext))
                {
                    return (ContextType)Enum.Parse(typeof(ContextType), currentContext);
                }
                else
                {
                    return ContextType.Default;
                }
            }
        }
    }
}
