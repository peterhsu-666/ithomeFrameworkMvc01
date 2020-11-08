using System.Web.Mvc;

namespace ithomeFrameworkMvc01.Controllers.Home
{
    public partial class HomeController 
    {
        /// <summary>
        /// 明細 - GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail()
        {
            if (Session["Account"] == null || string.IsNullOrWhiteSpace(Session["Account"].ToString()))
            {
                return View("Index");
            }
            else
            {
                return View();
            }
        }
    }
}