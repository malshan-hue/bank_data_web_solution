﻿using bank_data_web_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_data_web_business_layer.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> CreateDeposit(Transaction transaction);
        Task<bool> CreateWithdrawal(Transaction transaction);
        Task<Transaction> GetTransactionDetails(int transactionId);
        Task<IEnumerable<Transaction>> GetTransactionDetailsByAccountNumber(string accountNumber);
    }
}
