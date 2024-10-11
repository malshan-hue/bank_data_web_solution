using bank_data_web_application.Helpers;
using bank_data_web_models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace bank_data_web_application.Areas.AdminDashboard.Controllers
{
	[Area("AdminDashboard")]
	public class TransactionController : Controller
    {
        
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadTransactionData(DateTime fromDate, DateTime toDate)
        {

            var client = new RestClient(GlobalStaticHelper.baseApiUrl);
            var request = new RestRequest("/api/TransactionManagement/GetALLTransactionsForAdmin", Method.Get);
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
    }
}
