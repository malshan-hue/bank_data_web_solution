﻿using bank_data_web_business_layer.Interfaces;
using bank_data_web_models;
using bank_data_web_models.DTO;
using SimpleCrypto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bank_data_web_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagmentController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly PBKDF2 _crypto;
        private readonly Random _random;

		public UserManagmentController(IUserService userService)
        {
            _userService = userService;
            _crypto = new PBKDF2();
            _random = new Random();
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromForm] UserDTO userDTO)
        {
            //validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //create new guid for user
            var userGlobalIdentity = Guid.NewGuid();
            string userProfileImageFileUri = string.Empty;

			//store profile image
			if (userDTO.Picture != null && userDTO.Picture.Length > 0)
			{
				var userProfileImagesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/UserProfileImages");

				var userProfileImageFileName = userGlobalIdentity.ToString() + "-" + userDTO.Picture.FileName;
				var userProfileImageFilePath = Path.Combine(userProfileImagesFolderPath, userProfileImageFileName);

				using (var stream = new FileStream(userProfileImageFilePath, FileMode.Create))
				{
					await userDTO.Picture.CopyToAsync(stream);
				}

				userProfileImageFileUri = Path.Combine("/Uploads/UserProfileImages", userProfileImageFileName).Replace("\\", "/");
			}

            //create user object
			User user = new User()
            {
                UserName = userDTO.UserName,
                Password = _crypto.Compute(userDTO.Password),
                PasswordSalt = _crypto.Salt,
                ActivationCode = _random.Next(100000, 1000000),
                UserGlobalIdentity = userGlobalIdentity,
                UserInformation = new UserInformation()
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    Phone = userDTO.Phone,
                    PictureUrl = userProfileImageFileUri
				}
            };

            try
            {
                bool status = await _userService.CreateUser(user);

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

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();

                if(users == null)
                {
                    return NotFound("users Not Found");
                }

                return Ok(users);
            }
            catch(Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var user = await _userService.GetUserByEmail(email);

                if (user.UserId == 0)
                {
                    return NotFound($"A User with {email} was Not Found");
                }

                return Ok(user);

            } 
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromForm] UserDTO userDTO)
        {
            try
            {
                //validate the model
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //create new guid for user
                var userGlobalIdentity = Guid.NewGuid();
                string userProfileImageFileUri = string.Empty;

                //store profile image
                if (userDTO.Picture != null && userDTO.Picture.Length > 0)
                {
                    var userProfileImagesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/UserProfileImages");

                    var userProfileImageFileName = userGlobalIdentity.ToString() + "-" + userDTO.Picture.FileName;
                    var userProfileImageFilePath = Path.Combine(userProfileImagesFolderPath, userProfileImageFileName);

                    using (var stream = new FileStream(userProfileImageFilePath, FileMode.Create))
                    {
                        await userDTO.Picture.CopyToAsync(stream);
                    }

                    userProfileImageFileUri = Path.Combine("/Uploads/UserProfileImages", userProfileImageFileName).Replace("\\", "/");
                }

                //create user object
                User user = new User()
                {
                    UserId = userDTO.UserId,
                    UserName = userDTO.UserName,
                    Password = _crypto.Compute(userDTO.Password),
                    PasswordSalt = _crypto.Salt,
                    ActivationCode = _random.Next(100000, 1000000),
                    UserGlobalIdentity = userGlobalIdentity,
                    UserInformation = new UserInformation()
                    {
                        UserId = userDTO.UserId,
                        Name = userDTO.Name,
                        Email = userDTO.Email,
                        Phone = userDTO.Phone,
                        PictureUrl = userProfileImageFileUri
                    }
                };

            
                bool status = await _userService.UpdateUser(user);
                return Ok(status);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            try
            {
                var status = await _userService.DeleteUserByEmail(email);

                if (!status)
                {
                    return NotFound($"User with {email} wa Not Found");
                }

                return Ok(status);
            }
            catch(Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
