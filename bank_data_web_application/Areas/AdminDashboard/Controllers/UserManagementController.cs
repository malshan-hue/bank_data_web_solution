using bank_data_web_application.Helpers;
using bank_data_web_models;
using bank_data_web_models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Claims;

namespace bank_data_web_application.Areas.AdminDashboard.Controllers
{

	[Area("AdminDashboard")]
	public class UserManagementController : Controller
	{

		private int _userId;

		public async Task<IActionResult> Index()
		{
			_userId = (int)HttpContext.Session.GetInt32("UserId");

			var client = new RestClient(GlobalStaticHelper.baseApiUrl);
			var request = new RestRequest("/api/UserManagment/GetAllUsers", Method.Get);
			try
			{
				var response = await client.ExecuteAsync(request);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var userResult = JsonConvert.DeserializeObject<IEnumerable<User>>(response.Content);

					if (userResult.Count() == 0)
					{
						return View(new List<User>());
					}

					return View(userResult);
				}
			}
			catch (Exception)
			{
				return View(new List<User>());
			}

			return View(new List<User>());
		}

		[HttpGet]
		public async Task<IActionResult> UserProfile(string userName)
		{
			var client = new RestClient(GlobalStaticHelper.baseApiUrl);
			var request = new RestRequest("/api/UserManagment/GetUserByEmail", Method.Get);
			request.AddQueryParameter("email", userName);

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
		public async Task<IActionResult> UserProfile([FromForm] UserDTO userDTO)
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

		[HttpPost]
		public async Task<IActionResult> DeactivateUser(string userName)
		{
			var client = new RestClient(GlobalStaticHelper.baseApiUrl);
			var request = new RestRequest("/api/UserManagment/DeleteUser", Method.Delete);
			request.AddParameter("email", userName);

			try
			{
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

		[HttpGet]
		public async Task<IActionResult> CreateNewUser()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateNewUser([FromForm] UserDTO userDTO)
		{
			try
			{
				var client = new RestClient(GlobalStaticHelper.baseApiUrl);
				var request = new RestRequest("/api/UserManagment/CreateUser", Method.Post);
				request.AddParameter("UserId", 0);
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

				if (response.StatusCode == System.Net.HttpStatusCode.Created)
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
