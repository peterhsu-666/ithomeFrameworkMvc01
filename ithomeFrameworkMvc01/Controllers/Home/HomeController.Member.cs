using ithomeFrameworkMvc01.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.Script.Serialization;
using ithomeFrameworkMvc01.Models.Home;
using static ithomeFrameworkMvc01.Models.Home.MemberModel;

namespace ithomeFrameworkMvc01.Controllers.Home
{
    public partial class HomeController 
    {
        /// <summary>
        /// 會員 - GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Member()
        {
            MemberModel model = new MemberModel();

            List<ViewCity> list = GetCityList();

            ViewBag.CityList = list;

            return View(model);
        }
        
        /// <summary>
        /// 會員 - POST
        /// </summary>
        /// <param name="model">畫面參數</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Member(MemberModel model)
        {
            if (ModelState.IsValid)
            {
                bool success = InsertMember(model);
                return View("Index");
            }
            else
            {
                List<ViewCity> list = GetCityList();

                ViewBag.CityList = list;

                return View(model);
            }
        }

        /// <summary>
        /// 取得城市清單
        /// </summary>
        /// <returns></returns>
        public List<ViewCity> GetCityList()
        {
            try
            {
                conn.ConnectionString = connString;

                string sql = @"SELECT id, city FROM City";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                List<ViewCity> list = new List<ViewCity>();

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ViewCity city = new ViewCity();
                        city.CityId = dr["id"].ToString();
                        city.CityName = dr["city"].ToString();
                        list.Add(city);
                    }
                }

                //DataTable dt = new DataTable();
                //MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                //adapter.Fill(dt);

                //ViewBag.DT = dt;

                return list;
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
            finally
            {
                Disconnect();
            }
        }

        /// <summary>
        /// 取得鄉鎮清單
        /// </summary>
        /// <param name="id">城市代碼</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetVillageList(string id)
        {
            try
            {
                conn.ConnectionString = connString;

                string sql = @"SELECT villageid, village FROM Village WHERE cityid = @id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                List<ViewVillage> list = new List<ViewVillage>();

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ViewVillage village = new ViewVillage();
                        village.VillageId = dr["villageid"].ToString();
                        village.VillageName = dr["village"].ToString();
                        list.Add(village);
                    }
                }

                JavaScriptSerializer JsonConvert = new JavaScriptSerializer();

                string result = JsonConvert.Serialize(list);

                return Json(result);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
            finally
            {
                Disconnect();
            }
        }

        /// <summary>
        /// 新增會員
        /// </summary>
        /// <param name="model">畫面資料</param>
        /// <returns></returns>
        public bool InsertMember(MemberModel model)
        {
            conn.ConnectionString = connString;

            if (conn.State != ConnectionState.Open)
                conn.Open();

            string sql = @"INSERT INTO member (
                                       account
                                     , password
                                     , city
                                     , village
                                     , address
                           ) VALUES (
                                       @account
                                     , @password
                                     , @city
                                     , @village
                                     , @address)";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@account", MySqlDbType.VarChar).Value = model.Account;
            cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = model.Password;
            cmd.Parameters.Add("@city", MySqlDbType.VarChar).Value = model.City;
            cmd.Parameters.Add("@village", MySqlDbType.VarChar).Value = model.Village;
            cmd.Parameters.Add("@address", MySqlDbType.VarChar).Value = model.Address;

            int index = cmd.ExecuteNonQuery();

            bool success = false;

            success = (index > 0) ? true : false;

            conn.Close();

            return success;
        }

        /// <summary>
        /// 關閉連線
        /// </summary>
        public void Disconnect()
        {
            conn.Close();
        }
    }
}