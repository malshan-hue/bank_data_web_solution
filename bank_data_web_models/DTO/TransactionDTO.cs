using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_data_web_models.DTO
{
    public class TransactionDTO
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionTypeEnum TransactionTypeEnum { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
