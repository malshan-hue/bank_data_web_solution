using bank_data_web_business_layer.Interfaces;
using bank_data_web_data_access_layer;
using bank_data_web_models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_data_web_business_layer
{
    public class UserServiceImpl : IUserService
    {
        private readonly IDatabaseService _databaseService;
        private readonly UserRepository<User> _userRepository;

        public UserServiceImpl(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
            _userRepository = new UserRepository<User>(_databaseService.GetConnectionString());
        }

        public async Task<bool> CreateUser(User user)
        {
            string userJsonString = JsonConvert.SerializeObject(user);
            bool status =  _userRepository.InsertData("CreateUser", userJsonString);
            return status;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = _userRepository.RetrieveData("GetUserByEmail", new SqlParameter[]{
                new SqlParameter("@email", email)
            }).FirstOrDefault();

            return user ?? new User();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = _userRepository.RetrieveData("GetAllUsers", new SqlParameter[]{
            });

            return users;
        }

        public async Task<bool> UpdateUser(User user)
        {
            string userJsonString = JsonConvert.SerializeObject(user);
            bool status = _userRepository.UpdateData("UpdateUser", userJsonString);
            return status;
        }

        public async Task<bool> DeleteUserByEmail(string email)
        {
            var status = _userRepository.DeleteData("DeleteUserByEmail", new SqlParameter[]{
                new SqlParameter("@email", email)
            });

            return status;
        }

    }
}
