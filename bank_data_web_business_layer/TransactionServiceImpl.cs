using bank_data_web_business_layer.Interfaces;
using bank_data_web_data_access_layer;
using bank_data_web_models;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_data_web_business_layer
{
    public class TransactionServiceImpl : ITransactionService
    {
        private readonly IDatabaseService _databaseService;
        private readonly TransactionRepository<Transaction> _transactionRepository;

        public TransactionServiceImpl(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
            _transactionRepository = new TransactionRepository<Transaction>(_databaseService.GetConnectionString());
        }

        public async Task<bool> CreateDeposit(Transaction transaction)
        {
            string transactionJsonString = JsonConvert.SerializeObject(transaction);
            bool status = _transactionRepository.InsertData("CreateDeposit", transactionJsonString);
            return status;
        }
        public async Task<bool> CreateWithdrawal(Transaction transaction)
        {
            string transactionJsonString = JsonConvert.SerializeObject(transaction);
            bool status = _transactionRepository.InsertData("CreateWithdrawal", transactionJsonString);
            return status;
        }

        public async Task<Transaction> GetTransactionDetails(int transactionId)
        {
            var transaction = _transactionRepository.RetrieveData("GetTransactionDetails", new SqlParameter[]
            {
                new SqlParameter("@transactionId", transactionId)
            }).FirstOrDefault();

            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionDetailsByAccountNumber(string accountNumber)
        {
            var transaction = _transactionRepository.RetrieveData("GetTransactionDetailsByAccountNumber", new SqlParameter[]
            {
                new SqlParameter("@accountNumber", accountNumber)
            });

            return transaction;
        }

        public class TransferModel
        {
            public int FromAccountId { get; set; }
            public string ToAccountNumber { get; set; }
            public double Amount { get; set; }
            public string Description { get; set; }
        }

        public async Task<bool> CreateTransfer(int fromAccountId, string toAccountNumber, double amount, string description)
        {

            var transfer = new TransferModel()
            {
                FromAccountId = fromAccountId,
                ToAccountNumber = toAccountNumber,
                Amount = amount,
                Description = description
            };

            string transferJsonString = JsonConvert.SerializeObject(transfer);
            bool status = _transactionRepository.InsertData("CreateTransfer", transferJsonString);
            return status;
        }

		public async Task<IEnumerable<Transaction>> GetTransactionDetailsByAccountId(int accountId)
		{
			var transaction = _transactionRepository.RetrieveData("GetTransactionDetailsByAccountId", new SqlParameter[]
			{
				new SqlParameter("@accountId", accountId)
			});

			return transaction;
		}

        public async Task<IEnumerable<Transaction>> GetAllTransactions(int userId, DateTime? fromDate, DateTime? toDate)
        {
			var transactions = _transactionRepository.RetrieveData("GetAllTransactions", new SqlParameter[]
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate", toDate)
            });
            return transactions;
		}

	}
}
