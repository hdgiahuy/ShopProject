using ShopProject.Areas.Shopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopProject.Areas.Shopper.Controllers
{
    public class ThanhToanController : Controller
    {
        UserContext db = new UserContext();
        // GET: Shopper/ThanhToan
        public ActionResult Index()
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "UserReG");
            }
            else
            {
                List<CartItem> giohang = Session["giohang"] as List<CartItem>;
                return View(giohang);
            }

        }

        [HttpPost]
        public ActionResult StepEnd()
        {
            ////Nhận reqest từ trang index
            string phone = Request.Form["phone"];
            string fullname = Request.Form["fullname"];
            string email = Request.Form["email"];
            string address = Request.Form["address"];
            string note = Request.Form["note"];
            ////kiểm tra xem có customer chưa và cập nhật lại
           
            //Thêm thông tin vào order và orderdetail
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            //thêm order mới
            Order newOrder = new Order();
            newOrder.orderDiachi = address;
            newOrder.orderName = fullname;
            newOrder.orderMessage = note;
            newOrder.orderSDT = phone;
            newOrder.orderDateTime = DateTime.Now.ToString();
            newOrder.orderStatus = "Chờ phê duyệt";
            newOrder.adAcc = Session["accname"].ToString();
            db.Orders.Add(newOrder);
            db.SaveChanges();
            //thêm details
            for (int i = 0; i < giohang.Count; i++)
            {
                OrderDetail newOrdts = new OrderDetail();
                newOrdts.orderID = newOrder.orderID;
                newOrdts.proID = giohang.ElementAtOrDefault(i).SanPhamID;
                newOrdts.ordtsQuantity = giohang.ElementAtOrDefault(i).SoLuong;
                newOrdts.ordtsThanhTien = giohang.ElementAtOrDefault(i).ThanhTien.ToString();
                db.OrderDetails.Add(newOrdts);
                db.SaveChanges();
            }
            Session["MHD"] = newOrder.orderID ;
            Session["Phone"] = phone;
            Session["fullname"] = fullname;
            Session["Email"] = email;
            Session["Diachi"] = address;
            //xoá sạch giỏ hàng
            giohang.Clear();
            return RedirectToAction("HoaDon", "ThanhToan");
        }

        public ActionResult HoaDon()
        {
            return View();
        }
    }
}