using bank_data_web_application.Helpers;
using bank_data_web_models.DTO;
using bank_data_web_models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Claims;

namespace bank_data_web_application.Areas.AdminDashboard.Controllers
{
    [Area("AdminDashboard")]
    public class DashboardController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

		[HttpGet]
		public async Task<IActionResult> Profile()
		{
			var client = new RestClient(GlobalStaticHelper.baseApiUrl);
			var request = new RestRequest("/api/UserManagment/GetUserByEmail", Method.Get);
			request.AddQueryParameter("email", User.FindFirst(ClaimTypes.Email)?.Value);

			try
			{
				var response = await client.ExecuteAsync(request);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var userResult = JsonConvert.DeserializeObject<User>(response.Content);
					return View(userResult);
				}
				else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					return RedirectToAction("Index", "BankApplicationHome");
				}
				else
				{
					return RedirectToAction("Index", "BankApplicationHome");
				}
			}
			catch (Exception ex)
			{
				return RedirectToAction("Index", "BankApplicationHome");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Profile([FromForm] UserDTO userDTO)
		{
			try
			{
				var client = new RestClient(GlobalStaticHelper.baseApiUrl);
				var request = new RestRequest("/api/UserManagment/UpdateUser", Method.Put);
				request.AddParameter("UserId", userDTO.UserId);
				request.AddParameter("UserName", userDTO.UserName);

				if (!string.IsNullOrEmpty(userDTO.Password))
				{
					request.AddParameter("Password", userDTO.Password);
				}

				request.AddParameter("Name", userDTO.Name);
				request.AddParameter("Email", userDTO.Email);
				request.AddParameter("Phone", userDTO.Phone);

				if (userDTO.Picture != null && userDTO.Picture.Length > 0)
				{
					using (var stream = new MemoryStream())
					{
						await userDTO.Picture.CopyToAsync(stream);

						stream.Position = 0;

						request.AddFile("Picture", stream.ToArray(), userDTO.Picture.FileName, userDTO.Picture.ContentType);
					}
				}

				var response = await client.ExecuteAsync(request);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					return Json(new { data = true });
				}

				return Json(new { data = false });
			}
			catch (Exception)
			{
				return Json(new { data = false });
			}
		}
	}
}
