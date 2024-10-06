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
    }
}
