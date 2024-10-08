﻿using bank_data_web_models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_data_web_data_access_layer
{
    public class AccountRepository<Account>
    {
        private string _connectionString;

        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool InsertData(string procedureName, string jsonString)
        {
            bool status = false;

            try
            {

                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    using (var sqlCommand = new SqlCommand(procedureName, sqlConnection))
                    {

                        try
                        {

                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            sqlCommand.Parameters.AddWithValue("@jsonString", jsonString);

                            var executionStatusParam = new SqlParameter
                            {
                                ParameterName = "@executionStatus",
                                SqlDbType = SqlDbType.Bit,
                                Direction = ParameterDirection.Output,
                            };

                            sqlCommand.Parameters.Add(executionStatusParam);

                            sqlConnection.Open();
                            sqlCommand.ExecuteNonQuery();

                            status = (bool)sqlCommand.Parameters["@executionStatus"].Value;

                            return status;
                        }
                        catch (Exception)
                        {
                            return status;
                        }
                        finally
                        {
                            sqlConnection.Close();
                        }

                    }
                }

            }
            catch (Exception)
            {
                return status;
            }
        }

        public bool UpdateData(string procedureName, string jsonString)
        {
            bool status = false;
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    using (var sqlCommand = new SqlCommand(procedureName, sqlConnection))
                    {
                        try
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;

                            sqlCommand.Parameters.AddWithValue("@jsonString", jsonString);

                            var executionStatusParam = new SqlParameter
                            {
                                ParameterName = "@executionStatus",
                                SqlDbType = SqlDbType.Bit,
                                Direction = ParameterDirection.Output
                            };
                            sqlCommand.Parameters.Add(executionStatusParam);

                            sqlConnection.Open();
                            sqlCommand.ExecuteNonQuery();

                            status = (bool)sqlCommand.Parameters["@executionStatus"].Value;

                            return status;
                        }
                        catch (Exception)
                        {
                            return status;
                        }
                        finally
                        {
                            sqlConnection.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {
                return status;
            }
        }

        public ICollection<Account> RetrieveData(string procedureName, SqlParameter[] parameters = null)
        {
            ICollection<Account> data = new List<Account>();

            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    using (var sqlCommand = new SqlCommand(procedureName, sqlConnection))
                    {
                        try
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;

                            if (parameters != null)
                            {
                                sqlCommand.Parameters.AddRange(parameters);
                            }

                            sqlConnection.Open();

                            var jsonResult = new StringBuilder();
                            var reader = sqlCommand.ExecuteReader();

                            if (!reader.HasRows)
                            {
                                jsonResult.Append("[]");
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    jsonResult.Append(reader.GetValue(0).ToString());
                                }
                            }

                            data = JsonConvert.DeserializeObject<ICollection<Account>>(jsonResult.ToString()) ?? new List<Account>();
                        }
                        catch (Exception)
                        {
                            return new List<Account>();
                        }
                        finally
                        {
                            sqlConnection.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return data;
        }



        public bool DeleteData(string procedureName, SqlParameter[] parameters = null)
        {
            bool status = false;

            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    using (var sqlCommand = new SqlCommand(procedureName, sqlConnection))
                    {
                        try
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;

                            if (parameters != null)
                            {
                                sqlCommand.Parameters.AddRange(parameters);
                            }

                            var executionStatusParam = new SqlParameter
                            {
                                ParameterName = "@executionStatus",
                                SqlDbType = SqlDbType.Bit,
                                Direction = ParameterDirection.Output
                            };

                            sqlCommand.Parameters.Add(executionStatusParam);

                            sqlConnection.Open();
                            sqlCommand.ExecuteNonQuery();

                            status = (bool)sqlCommand.Parameters["@executionStatus"].Value;

                            return status;

                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {

                            sqlConnection.Close();
                        }
                    }
                }

            }
            catch (Exception)
            {
                return status;
            }



        }
    }
}
