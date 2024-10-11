using bank_data_web_business_layer.Interfaces;
using bank_data_web_models;
using bank_data_web_models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bank_data_web_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionManagementController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionManagementController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("CreateDeposit")]
        public async Task<IActionResult> CreateDeposit(TransactionDTO transactionDTO)
        {
            try
            {
                Transaction transaction = new Transaction()
                {
                    AccountId = transactionDTO.AccountId,
                    Amount = transactionDTO.Amount,
                    TransactionTypeEnum = transactionDTO.TransactionTypeEnum,
                    Description = transactionDTO.Description,
                };

                var status = await _transactionService.CreateDeposit(transaction);

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

        [HttpPost("CreateWithdrawal")]
        public async Task<IActionResult> CreateWithdrawal(TransactionDTO transactionDTO)
        {
            try
            {
                Transaction transaction = new Transaction()
                {
                    AccountId = transactionDTO.AccountId,
                    Amount = transactionDTO.Amount,
                    TransactionTypeEnum = transactionDTO.TransactionTypeEnum,
                    Description = transactionDTO.Description,
                };

                var status = await _transactionService.CreateWithdrawal(transaction);

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

        [HttpGet("GetTransactionDetailsByAccountNumber")]
        public async Task<IActionResult> GetTransactionDetailsByAccountNumber(string accountNumber)
        {
            try
            {
                var account = await _transactionService.GetTransactionDetailsByAccountNumber(accountNumber);

                if (account == null)
                {
                    return NotFound("transaction not found");
                }

                return Ok(account);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetTransactionDetails")]
        public async Task<IActionResult> GetTransactionDetails(int transactionNumber)
        {
            try
            {
                var transaction = await _transactionService.GetTransactionDetails(transactionNumber);

                if (transaction == null)
                {
                    return NotFound("transaction not found");
                }

                return Ok(transaction);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("CreateTransfer")]
        public async Task<IActionResult> CreateTransfer(int fromAccountId, string toAccountNumber, double amount)
        {
			try
			{

				var status = await _transactionService.CreateTransfer(fromAccountId, toAccountNumber, amount);

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

		[HttpGet("GetTransactionDetailsByAccountId")]
		public async Task<IActionResult> GetTransactionDetailsByAccountId(int accountId)
		{
			try
			{
				var account = await _transactionService.GetTransactionDetailsByAccountId(accountId);

				if (account == null)
				{
					return NotFound("transaction not found");
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
