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
    }
}
