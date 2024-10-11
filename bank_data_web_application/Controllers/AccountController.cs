using bank_data_web_application.Helpers;
using bank_data_web_models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace bank_data_web_application.Controllers
{
    public class AccountController : Controller
    {

        private int _userId;
        public AccountController() { }

        [HttpGet]
        public async Task<IActionResult> Summary()
        {
			_userId = (int)HttpContext.Session.GetInt32("UserId");

			var client = new RestClient(GlobalStaticHelper.baseApiUrl);
			var request = new RestRequest("/api/AccountManagement/GetAccountsByUser", Method.Get);
			request.AddQueryParameter("userId", _userId);

            try
            {
				var response = await client.ExecuteAsync(request);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
					var accountResult = JsonConvert.DeserializeObject<IEnumerable<Account>>(response.Content);
                    return View(accountResult);
				}
			}
            catch (Exception)
            {
				return View(new List<Account>());
			}

			return View(new List<Account>());
        }

        [HttpGet]
        public async Task<IActionResult> ViewTransactions(int accountId)
        {
			_userId = (int)HttpContext.Session.GetInt32("UserId");

			var client = new RestClient(GlobalStaticHelper.baseApiUrl);
			var request = new RestRequest("/api/TransactionManagement/GetTransactionDetailsByAccountId", Method.Get);
			request.AddQueryParameter("accountId", accountId);

			try
			{
				var response = await client.ExecuteAsync(request);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var transactionResult = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(response.Content);
					return View(transactionResult);
				}
			}
			catch (Exception)
			{
				return View(new List<Transaction>());
			}

			return View(new List<Transaction>());
		}
    }
}
