using bank_data_web_business_layer.Interfaces;
using bank_data_web_data_access_layer;
using bank_data_web_models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_data_web_business_layer
{
    public class AccountServiceImpl : IAccountService
    {

        private readonly IDatabaseService _databaseService;
        private readonly AccountRepository<Account> _accountRepository;

        public AccountServiceImpl(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
            _accountRepository = new AccountRepository<Account>(_databaseService.GetConnectionString());
        }

        public async Task<bool> CreateNewBankAccount(Account account)
        {
            string accounhtJsonString = JsonConvert.SerializeObject(account);
            bool status = _accountRepository.InsertData("CreateNewBankAccount", accounhtJsonString);
            return status;
        }

        public async Task<Account> GetBankAccountByAccountNumber(string accountNumber)
        {
            var account = _accountRepository.RetrieveData("GetBankAccountByAccountNumber", new SqlParameter[]{
                new SqlParameter("@accountNumber", accountNumber)
            }).FirstOrDefault();

            return account ?? new Account();
        }

        public async Task<IEnumerable<Account>> GetBankAccountByAccountHolderName(string holderName)
        {
            var accounts = _accountRepository.RetrieveData("GetBankAccountByAccountHolderName", new SqlParameter[]{
                new SqlParameter("@holderName", holderName)
            });

            return accounts;
        }

        public async Task<IEnumerable<Account>> GetAllBankAccounts()
        {
            var users = _accountRepository.RetrieveData("GetAllBankAccount", new SqlParameter[]{
            });

            return users;
        }

        public async Task<bool> UpdateBankAccount(Account account)
        {
            string accounhtJsonString = JsonConvert.SerializeObject(account);
            bool status = _accountRepository.UpdateData("UpdateBankAccount", accounhtJsonString);
            return status;
        }

        public async Task<bool> DeleteBankAccount(string accountNumber)
        {
            var status = _accountRepository.DeleteData("DeleteBankAccount", new SqlParameter[]{
                new SqlParameter("@accountNumber", accountNumber)
            });

            return status;
        }

		public async Task<IEnumerable<Account>> GetBankAccountByUser(int userId)
		{
			var accounts = _accountRepository.RetrieveData("GetBankAccountByUser", new SqlParameter[]{
				new SqlParameter("@userId", userId)
			});

			return accounts;
		}

		public async Task<Account> GetBankAccountByAccountId(int accountId)
		{
			var account = _accountRepository.RetrieveData("GetBankAccountByAccountId", new SqlParameter[]{
				new SqlParameter("@accountId", accountId)
			}).FirstOrDefault();

			return account;
		}
	}
}
