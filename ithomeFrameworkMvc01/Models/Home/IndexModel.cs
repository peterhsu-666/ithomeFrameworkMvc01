using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ithomeFrameworkMvc01.Models.Home
{
    public class IndexModel : IValidatableObject
    {
        public IndexModel()
        {
            connString = "server=127.0.0.1;port=3306;user id=root;password=a55663699;database=mvctest;charset=utf8;";

            conn = new MySqlConnection();
        }
        public string connString { get; set; }
        public MySqlConnection conn { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// 驗證
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var regex = new Regex("^[a-zA-Z0-9]+$");

            if (!string.IsNullOrWhiteSpace(Account))
            {
                if (!regex.IsMatch(Account))
                {
                    yield return new ValidationResult("English Or Number", new string[] { "Account" });
                }
            }

            if (!string.IsNullOrWhiteSpace(Password))
            {
                if (!regex.IsMatch(Password))
                {
                    yield return new ValidationResult("English Or Number", new string[] { "Password" });
                }
            }
        }

        /// <summary>
        /// 檢查是否有此會員
        /// </summary>
        /// <returns></returns>
        public bool CheckMember()
        {
            try
            {
                conn.ConnectionString = connString;

                string sql = @"SELECT account FROM member WHERE account = @account AND password = @password";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("@account", MySqlDbType.VarChar).Value = Account;
                cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = Password;

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                MySqlDataReader dr = cmd.ExecuteReader();

                return dr.Read();
               
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}