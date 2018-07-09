using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultix.ProCampaignAccount.Models
{
    public class RequestModelBase
    {
        public string ApiKey { get; set; }
        public string Source { get; set; }
        public List<Attribute> Attributes { get; set; }
        public List<Transaction> Transactions { get; set; }

        public RequestModelBase()
        {
            Attributes = new List<Attribute>();
            Transactions = new List<Transaction>();
        }

        public void AddAttribute(string name,string value)
        {
            Attributes.Add(new Attribute(){Name = name,Value = value});
        }

        public void AddTransaction(string name, string source, List<Parameter> parameters)
        {
            Transactions.Add(new Transaction()
            {
                Name = name, 
                Source = source,
                Parameters = parameters,
                Date_Created = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")
            });
        }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }

        public class Attribute
        {
            public string Name { get; set; }
            public Object Value { get; set; }
        }

        public class Parameter
        {
            public string Name { get; set; }
            public Object Value { get; set; }
        }

        public class Transaction
        {
            public string Name { get; set; }
            public string Source { get; set; }
            public string Date_Created { get; set; }
            public List<Parameter> Parameters { get; set; }

        }
    }
}
