using bank_data_web_business_layer.Interfaces;
using bank_data_web_models;
using bank_data_web_models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bank_data_web_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountManagementController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountManagementController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("CreateNewBankAccount")]
        public async Task<IActionResult> CreateNewBankAccount(AccountDTO accountDTO)
        {
            try
            {
                Account account = new Account()
                {
                    UserId = accountDTO.UserId,
                    AccountNumber = accountDTO.AccountNumber,
                    AccountHolderName = accountDTO.AccountHolderName,
                    Balance = accountDTO.Balance,
                };

                var status = await _accountService.CreateNewBankAccount(account);

                if (!status)
                {
                    return Ok(status);
                }

                return StatusCode(201, status);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetAllBankAccounts")]
        public async Task<IActionResult> GetAllBankAccounts()
        {
            try
            {
                var accounts = await _accountService.GetAllBankAccounts();

                if(accounts == null)
                {
                    return NotFound("Accounts not found");
                }

                return Ok(accounts);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetAccountByAccountNumber")]
        public async Task<IActionResult> GetAccountByAccountNumber(string accountNumber)
        {
            try
            {
                var account = await _accountService.GetBankAccountByAccountNumber(accountNumber);

                if (account == null)
                {
                    return NotFound("Accounts not found");
                }

                return Ok(account);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetAccountsByHolderName")]
        public async Task<IActionResult> GetAccountsByHolderName(string holderName)
        {
            try
            {
                var accounts = await _accountService.GetBankAccountByAccountHolderName(holderName);

                if (accounts == null)
                {
                    return NotFound("Accounts not found");
                }

                return Ok(accounts);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("UpdateBankAccount")]
        public async Task<IActionResult> UpdateBankAccount(AccountDTO accountDTO)
        {
            try
            {
                Account account = new Account()
                {
                    AccountId = accountDTO.AccountId,
                    UserId = accountDTO.UserId,
                    AccountNumber = accountDTO.AccountNumber,
                    AccountHolderName = accountDTO.AccountHolderName,
                    Balance = accountDTO.Balance,
                };

                var status = await _accountService.UpdateBankAccount(account);

                return Ok(status);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeleteBankAccount")]
        public async Task<IActionResult> DeleteBankAccount(string accountNumber)
        {
            try
            {
                var status = await _accountService.DeleteBankAccount(accountNumber);

                return Ok(status);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

		[HttpGet("GetAccountsByUser")]
		public async Task<IActionResult> GetAccountsByUser(int userId)
		{
			try
			{
				var accounts = await _accountService.GetBankAccountByUser(userId);

				if (accounts == null)
				{
					return NotFound("Accounts not found");
				}

				return Ok(accounts);
			}
			catch (Exception)
			{
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpGet("GetAccountsByAccountId")]
		public async Task<IActionResult> GetAccountsByAccountId(int accountId)
		{
			try
			{
				var account = await _accountService.GetBankAccountByAccountId(accountId);

				if (account == null)
				{
					return NotFound("Account not found");
				}

				return Ok(account);
			}
			catch (Exception)
			{
				return StatusCode(500, "Internal server error");
			}
		}
	}
}
