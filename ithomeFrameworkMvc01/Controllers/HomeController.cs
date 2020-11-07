using ithomeFrameworkMvc01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.Script.Serialization;

namespace ithomeFrameworkMvc01.Controllers
{
    public class HomeController : Controller
    {
        string connString = "server=127.0.0.1;port=3306;user id=root;password=a55663699;database=mvctest;charset=utf8;";

        MySqlConnection conn = new MySqlConnection();

        public ActionResult Index()
        {
            List<City> list = GetCityList();

            ViewBag.CityList = list;

            return View();
        }

        public List<City> GetCityList()
        {
            try
            {
                conn.ConnectionString = connString;

                string sql = @"SELECT id, city FROM City";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                List<City> list = new List<City>();

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        City city = new City();
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


                //DateTime date = DateTime.Now;
                //ViewBag.Date = date;
                //Student student = new Student("1", "王大明", 80);

                //return View(student);
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

        public void Disconnect()
        {
            conn.Close();
        }

        [HttpPost]
        public ActionResult GetVillageList(string id)
        {
            try
            {
                conn.ConnectionString = connString;

                string sql = @"SELECT villageid, village FROM Village WHERE cityid = "+ id;

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                List<Village> list = new List<Village>();

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Village village = new Village();
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

        public ActionResult Insert()
        {
            conn.ConnectionString = connString;

            if (conn.State != ConnectionState.Open)
                conn.Open();

            string sql = @"INSERT INTO `City` (`Id`, `City`) VALUES
                           ('0', '基隆市'),
                           ('1', '臺北市'),
                           ('2', '新北市'),
                           ('3', '桃園市'),
                           ('4', '新竹市'),
                           ('5', '新竹縣'),
                           ('6', '宜蘭縣'),
                           ('7', '苗栗縣'),
                           ('8', '臺中市'),
                           ('9', '彰化縣'),
                           ('A', '南投縣'),
                           ('B', '雲林縣'),
                           ('C', '嘉義市'),
                           ('D', '嘉義縣'),
                           ('E', '臺南市'),
                           ('F', '高雄市'),
                           ('G', '屏東縣'),
                           ('H', '澎湖縣'),
                           ('I', '花蓮縣'),
                           ('J', '臺東縣'),
                           ('K', '金門縣'),
                           ('L', '連江縣');
";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            int index = cmd.ExecuteNonQuery();

            bool success = false;

            success = (index > 0) ? true : false;

            ViewBag.Success = success;

            conn.Close();

            return View();
        }
        public ActionResult Transport(string id, string name, int score)
        {
            ViewBag.id = id;
            ViewBag.name = name;
            ViewBag.score = score;

            return View();
        }

        [HttpPost]
        public ActionResult Transport(FormCollection form)
        {
            ViewBag.id = form["id"];
            ViewBag.name = form["name"];
            ViewBag.score = int.Parse(form["score"]);

            return View();
        }
    }
}