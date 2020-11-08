using System.Web.Mvc;
using ithomeFrameworkMvc01.Models.Home;

namespace ithomeFrameworkMvc01.Controllers.Home
{
    public partial class HomeController
    {

        /// <summary>
        /// 首頁 - GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Session["Account"] == null || string.IsNullOrWhiteSpace(Session["Account"].ToString()))
            {
                IndexModel model = new IndexModel();

                return View(model);
            }
            else
            {
                Response.Redirect("~/Home/Detail");
                return new EmptyResult();
            }
        }

        /// <summary>
        /// 登入 - POST
        /// </summary>
        /// <param name="model">畫面資料</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(IndexModel model)
        {
            bool isLogin = model.CheckMember();

            if (isLogin)
            {
                Session["Account"] = model.Account;

                Response.Redirect("~/Home/Detail");
                return new EmptyResult();
            }
            else
            {
                ViewBag.Msg = "Login Failure!!";
                return View("Index",model);
            }
        }
    }
}