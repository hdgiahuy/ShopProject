using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ShopProject.Areas.Shopper.Models;
using ShopProject.Repository;
namespace ShopProject.Areas.Shopper.Controllers
{
    public class DonhangUserController : Controller
    {
        ShopDAO dao = new ShopDAO();
        Models.UserContext dbOder = new Models.UserContext();
        // GET: Shopper/DonhangUser

        public ActionResult Index()
        {
            string adacc = Session["accname"].ToString();
            var a = dao.hduser(adacc);
            int b = a.Count();
           
            if (b == 0)
            {
                ViewBag.text = "Hiện tại bạn không có đơn hàng nào!!!";
            }
            else
            {              
                int pageSize = 1000000;
                int pageNumber = 1;
                var i = dbOder.Orders.SqlQuery("SELECT * FROM Orders where adAcc='"+adacc+"'");
               
                return View(i.ToPagedList(pageNumber, pageSize));
            }
            return View();
        }
        public ActionResult oderdetail(int id)
        {

          
                var dao = new ShopDAO();
                Session["oderid"] = id;
                var model = dao.LisByOder(id);
                return View(model);
                
        }
    }
}
