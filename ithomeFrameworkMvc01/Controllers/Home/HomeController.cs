using ithomeFrameworkMvc01.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace ithomeFrameworkMvc01.Controllers.Home
{
    public partial class HomeController : Controller
    {
        string connString = "server=127.0.0.1;port=3306;user id=root;password=a55663699;database=mvctest;charset=utf8;";

        MySqlConnection conn = new MySqlConnection();
    }
}