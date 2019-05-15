using ShopProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopProject.Areas.Administrator.Controllers
{
    public class CustomerController : Controller
    {
        Models.AdminContext dbCus = new Models.AdminContext();
        //
        // GET: /Administrator/Category/
       
        public ActionResult Index()
        {
            string a = Session["accname"].ToString();
            var model = dbCus.Administrators.SingleOrDefault(x => x.adAcc.Equals(a));
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
                    var modelpro = dbCus.Customers.ToList();
                    return View(modelpro);
                }
            }
        }
    }
}