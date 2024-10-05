using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_data_web_models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionTypeEnum TransactionTypeEnum { get; set; }
        public string Description { get; set; } = string.Empty;

        #region NAVIGATIONAL PROPERTIES
        public Account Account { get; set; } = new Account();
        #endregion
    }
}
