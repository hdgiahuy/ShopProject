using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ShopProject.Repository;
using Rotativa;


namespace ShopProject.Areas.Administrator.Controllers
{
    public class OderController : Controller
    {
        Models.AdminContext dbOder = new Models.AdminContext();
        // GET: Administrator/Oder
        public ActionResult Index()
        {
            string a = Session["accname"].ToString();
            var model = dbOder.Administrators.SingleOrDefault(x => x.adAcc.Equals(a));
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

                    var modelpro = dbOder.Orders.ToList();
                    return View(modelpro);
                }
            }

        }
        public ActionResult oderdetail(int id)
        {

            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var dao = new ShopDAO();
                Session["oderid"] = id;
                var model = dao.LisByOder(id);
                return View(model);
            }
        }

        public ActionResult hd(int id)
        {

            var dao = new ShopDAO();
            var model = dao.LisByOder(id);
            return View(model);

        }
        //in hóa đơn
        public ActionResult PrintViewToPdf(int id)
        {
            var dao = new ShopDAO();
            var model = dao.LisByOder(id);
            return new ViewAsPdf("hd", model)
            {
                FileName = Server.MapPath("HD_" + id + ".pdf"),
                PageOrientation = Rotativa.Options.Orientation.Landscape,
               
                PageSize = Rotativa.Options.Size.A4
            };


        }
        [HttpPost]
        public JsonResult thaydoitrangthaiTK(int id)
        {
            var kq = new EX().thaydoitrangthaiTk(id);
            return Json(new
            {
                status = kq

            }
                  );

        }



    }
}