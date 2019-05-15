using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopProject.Areas.Administrator.Controllers
{
    public class HomeController : Controller
    {
        Models.AdminContext db = new Models.AdminContext();
        Repository.ShopDAO dao = new Repository.ShopDAO();
        [HandleError]
        public ActionResult Index()
        {
            string a = Session["accname"].ToString();
            var model = db.Administrators.SingleOrDefault(x => x.adAcc.Equals(a));
            if (model.adQuyen == 2)
            {
                return RedirectToRoute("Home");
            }
            else
            {
                if (Session["accname"] == null)
                {
                    Session["accname"] = null;
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    return View();
                }
            }
        }

	}
}