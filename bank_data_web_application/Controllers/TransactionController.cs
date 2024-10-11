using bank_data_web_application.Helpers;
using bank_data_web_models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace bank_data_web_application.Controllers
{
	public class TransactionController : Controller
	{
        private int _userId;

        [HttpGet]
		public async Task<IActionResult> TransaactionHistory()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> LoadTransactionData(DateTime fromDate, DateTime toDate)
		{
            _userId = (int)HttpContext.Session.GetInt32("UserId");

            var client = new RestClient(GlobalStaticHelper.baseApiUrl);
			var request = new RestRequest("/api/TransactionManagement/GetALLTransactions", Method.Get);
			request.AddQueryParameter("userId", _userId);
			request.AddQueryParameter("fromDate", fromDate);
			request.AddQueryParameter("toDate", toDate);

			try
			{
				var response = await client.ExecuteAsync(request);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var transactionResult = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(response.Content);

					if (transactionResult.Count() == 0)
					{
						return Json(new List<Transaction>());
					}

					return Json(transactionResult);
				}
			}
			catch (Exception)
			{
				return Json(new List<Transaction>());
			}

			return Json(new List<Transaction>());
		}

		[HttpGet]
		public async Task<IActionResult> TransferMoney()
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

					if (accountResult.Count() == 0)
					{
						return View(new List<Account>());
					}

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
		public async Task<IActionResult> InitiateTransfer(int accountId)
		{
			_userId = (int)HttpContext.Session.GetInt32("UserId");

			var client = new RestClient(GlobalStaticHelper.baseApiUrl);
			var request = new RestRequest("/api/AccountManagement/GetAccountsByAccountId", Method.Get);
			request.AddQueryParameter("accountId", accountId);

			try
			{
				var response = await client.ExecuteAsync(request);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var accountResult = JsonConvert.DeserializeObject<Account>(response.Content);

					if (accountResult is null)
					{
						return View(new Account());
					}

					return View(accountResult);
				}
			}
			catch (Exception)
			{
				return View(new Account());
			}

			return View(new Account());
		}

        [HttpPost]
        public async Task<IActionResult> InitiateTransfer(int fromAccountId, string toAccountNumber, double amount, string description)
        {
            _userId = (int)HttpContext.Session.GetInt32("UserId");

            var client = new RestClient(GlobalStaticHelper.baseApiUrl);
            var request = new RestRequest("/api/TransactionManagement/CreateTransfer", Method.Post);
            request.AddQueryParameter("fromAccountId", fromAccountId);
            request.AddQueryParameter("toAccountNumber", toAccountNumber);
            request.AddQueryParameter("amount", amount);
            request.AddQueryParameter("description", description);

            try
            {
                var response = await client.ExecuteAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return Json(new { data = true });
                }
            }
            catch (Exception)
            {
                return Json(new { data = false });
            }

            return Json(new { data = false });
        }
    }
}
