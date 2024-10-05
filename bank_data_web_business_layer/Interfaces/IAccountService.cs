using bank_data_web_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_data_web_business_layer.Interfaces
{
    public interface IAccountService
    {
        Task<bool> CreateNewBankAccount(Account account);
        Task<Account> GetBankAccountByAccountNumber(string accountNumber);
        Task<IEnumerable<Account>> GetBankAccountByAccountHolderName(string holderName);
        Task<IEnumerable<Account>> GetAllBankAccounts();
        Task<bool> UpdateBankAccount(Account account);
        Task<bool> DeleteBankAccount(string accountNumber);

    }
}
