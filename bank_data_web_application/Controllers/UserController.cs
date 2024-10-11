using bank_data_web_application.Helpers;
using bank_data_web_models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RestSharp;
using Newtonsoft.Json;
using SimpleCrypto;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace bank_data_web_application.Controllers
{
    public class UserController : Controller
    {
		private readonly PBKDF2 _crypto;
		public UserController()
		{
			_crypto = new PBKDF2();
		}

		[HttpGet]
        public async Task<IActionResult> Login()
        {
			if (User.Identity.IsAuthenticated)
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

						HttpContext.Session.SetInt32("UserId", userResult.UserId);
						HttpContext.Session.SetString("UserName", userResult.UserName);
						HttpContext.Session.SetString("Name", userResult.UserInformation.Name);
						HttpContext.Session.SetString("Email", userResult.UserInformation.Email);
						HttpContext.Session.SetString("pictureUrl", userResult.UserInformation.PictureUrl);

						var claims = new List<Claim>
						{
							new Claim(ClaimTypes.Name, userResult.UserName),
							new Claim(ClaimTypes.Email, userResult.UserInformation.Email),
							new Claim("UserId", userResult.UserId.ToString())
						};

						var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

						await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
						{
							IsPersistent = true,
							ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
						});

						return RedirectToAction("Index", "BankApplicationHome");
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
					{
						return View();
					}
					else
					{
						return View();
					}
				}
				catch (Exception ex)
				{
					return View();
				}
			}

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
			var client = new RestClient(GlobalStaticHelper.baseApiUrl);
			var request = new RestRequest("/api/UserManagment/GetUserByEmail", Method.Get);
			request.AddQueryParameter("email", user.UserName);
			

			try
			{
				var response = await client.ExecuteAsync(request);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var userResult = JsonConvert.DeserializeObject<User>(response.Content);

					if (!_crypto.Compare(userResult.Password, _crypto.Compute(user.Password, userResult.PasswordSalt)))
					{
						return Unauthorized("Invalid email or password.");
					}

                    HttpContext.Session.SetInt32("UserId", userResult.UserId);
                    HttpContext.Session.SetString("UserName", userResult.UserName);
                    HttpContext.Session.SetString("Name", userResult.UserInformation.Name);
                    HttpContext.Session.SetString("Email", userResult.UserInformation.Email);
                    HttpContext.Session.SetString("pictureUrl", userResult.UserInformation.PictureUrl);

                    var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, userResult.UserName),
						new Claim(ClaimTypes.Email, userResult.UserInformation.Email),
						new Claim("UserId", userResult.UserId.ToString())
					};

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
					{
						IsPersistent = true,
						ExpiresUtc = DateTime.UtcNow.AddMinutes(30) 
					});

					return RedirectToAction("Index", "BankApplicationHome");
				}
				else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					return View();
				}
				else
				{
					return View();
				}
			}
			catch (Exception ex)
			{
				return View();
			}
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			HttpContext.Session.Clear();

			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return RedirectToAction("Login", "User");
		}
	}
}
