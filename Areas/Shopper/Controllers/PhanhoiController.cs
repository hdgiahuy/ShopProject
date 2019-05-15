using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopProject.Areas.Shopper.Models;
using ShopProject.Repository;
namespace ShopProject.Areas.Shopper.Controllers
{
    public class PhanhoiController : Controller
    {
        Models.UserContext db = new Models.UserContext();
        // GET: Shopper/Phanhoi
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index( string fullname, string email, string phone, string mess)
        {
          
            
               

            Customer newCus = new Customer();
            var cus = db.Customers.FirstOrDefault(p => p.cusPhone.Equals(phone));
            if (cus != null)
            {
                //nếu có số điện thoại trong db rồi
                //cập nhật thông tin và lưu
                cus.cusFullName = fullname;
                cus.cusEmail = email;
                cus.cusAddress = cus.cusAddress + "<p>[" + DateTime.Now + "]" +"::"+ mess+"</p>";
                db.Entry(cus).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                //nếu chưa có sđt trong db
                //thêm thông tin và lưu
                newCus.cusPhone = phone;
                newCus.cusFullName = fullname;
                newCus.cusEmail = email;
                newCus.cusAddress ="["+DateTime.Now+"]"+ "::" + mess ;
                db.Customers.Add(newCus);
                db.SaveChanges();
            }
            return View();

        }
    

    }
}