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
    public class ProductsController : Controller
    {

        // GET: Shopper/Products

        Models.UserContext db = new Models.UserContext();
        // GET: Shopper/Products
        //hiển thị sản phẩm theo id loại

        public ActionResult ProductsByProType(int id, int? page, string gia, string size, string hangsx)
        {
            if (gia == null && size == null && hangsx == null)
            {
                ViewBag.typeName = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeName;
                ViewBag.typeID = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeID;
                Session["id"] = ViewBag.typeID;
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                return View(db.Products.Where(t => t.typeID == id).OrderByDescending(x => x.typeID).ToPagedList(pageNumber, pageSize));
            }
            //lọc giá, size, hãng sản xuất
            else if (gia != "null" && size != "null" && hangsx != "null")
            {
                //dưới 100k
                if (gia == "1")
                {
                    ViewBag.typeID = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeID;
                    Session["id"] = ViewBag.typeID;
                    int pageSize = 8;
                    int pageNumber = (page ?? 1);
                    var i = db.Products.SqlQuery("SELECT * FROM dbo.Products  where proPrice < '100000' and typeID='" + Session["id"] + "' and pdcID='" + hangsx + "' and proSize='" + size + "'");

                    return View(i.OrderByDescending(x => x.pdcID).ToPagedList(pageNumber, pageSize));
                }
                //trên 500k
                else if (gia == "2")
                {

                    ViewBag.typeID = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeID;
                    Session["id"] = ViewBag.typeID;
                    int pageSize = 8;
                    int pageNumber = (page ?? 1);
                    var i = db.Products.SqlQuery("SELECT * FROM dbo.Products  where proPrice > '500000' and typeID='" + Session["id"] + "' and pdcID='" + hangsx + "' and proSize='" + size + "'");

                    return View(i.OrderByDescending(x => x.pdcID).ToPagedList(pageNumber, pageSize));

                }
                //từ 100k đến 500k
                else
                {
                    ViewBag.typeID = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeID;
                    Session["id"] = ViewBag.typeID;
                    int pageSize = 8;
                    int pageNumber = (page ?? 1);
                    var i = db.Products.SqlQuery("SELECT * FROM dbo.Products  where proPrice <= '500000' and proPrice >= '100000' and typeID='" + Session["id"] + "' and pdcID='" + hangsx + "' and proSize='" + size + "'  ");

                    return View(i.OrderByDescending(x => x.pdcID).ToPagedList(pageNumber, pageSize));

                }
            }
            //lọc all
            else if (gia == "null" && size == "null" && hangsx == "null")
            {
                ViewBag.typeName = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeName;
                ViewBag.typeID = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeID;
                Session["id"] = ViewBag.typeID;
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                return View(db.Products.Where(t => t.typeID == id).OrderByDescending(x => x.typeID).ToPagedList(pageNumber, pageSize));

            }
            //giá , size
            else if (gia != "null" && size != "null" && hangsx == "null")
            {
                //dưới 100k
                if (gia == "1")
                {
                    ViewBag.typeID = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeID;
                    Session["id"] = ViewBag.typeID;
                    int pageSize = 8;
                    int pageNumber = (page ?? 1);
                    var i = db.Products.SqlQuery("SELECT * FROM dbo.Products  where proPrice < '100000' and typeID='" + Session["id"] + "'  and proSize='" + size + "'");

                    return View(i.OrderByDescending(x => x.pdcID).ToPagedList(pageNumber, pageSize));
                }
                //trên 500k
                else if (gia == "2")
                {

                    ViewBag.typeID = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeID;
                    Session["id"] = ViewBag.typeID;
                    int pageSize = 8;
                    int pageNumber = (page ?? 1);
                    var i = db.Products.SqlQuery("SELECT * FROM dbo.Products  where proPrice > '500000' and typeID='" + Session["id"] + "'  and proSize='" + size + "'");

                    return View(i.OrderByDescending(x => x.pdcID).ToPagedList(pageNumber, pageSize));

                }
                //từ 100k đến 500k
                else
                {
                    ViewBag.typeID = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeID;
                    Session["id"] = ViewBag.typeID;
                    int pageSize = 8;
                    int pageNumber = (page ?? 1);
                    var i = db.Products.SqlQuery("SELECT * FROM dbo.Products  where proPrice <= '500000' and proPrice >= '100000' and typeID='" + Session["id"] + "'  and proSize='" + size + "'  ");

                    return View(i.OrderByDescending(x => x.pdcID).ToPagedList(pageNumber, pageSize));

                }
            }
            //giá, hãng
            else if (gia != "null" && size == "null" && hangsx != "null")
            {
                //dưới 100k
                if (gia == "1")
                {
                    ViewBag.typeID = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeID;
                    Session["id"] = ViewBag.typeID;
                    int pageSize = 8;
                    int pageNumber = (page ?? 1);
                    var i = db.Products.SqlQuery("SELECT * FROM dbo.Products  where proPrice < '100000' and typeID='" + Session["id"] + "' and pdcID='" + hangsx + "' ");

                    return View(i.OrderByDescending(x => x.pdcID).ToPagedList(pageNumber, pageSize));
                }
                //trên 500k
                else if (gia == "2")
                {

                    ViewBag.typeID = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeID;
                    Session["id"] = ViewBag.typeID;
                    int pageSize = 8;
                    int pageNumber = (page ?? 1);
                    var i = db.Products.SqlQuery("SELECT * FROM dbo.Products  where proPrice > '500000' and typeID='" + Session["id"] + "' and pdcID='" + hangsx + "' ");

                    return View(i.OrderByDescending(x => x.pdcID).ToPagedList(pageNumber, pageSize));

                }
                //từ 100k đến 500k
                else
                {
                    ViewBag.typeID = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeID;
                    Session["id"] = ViewBag.typeID;
                    int pageSize = 8;
                    int pageNumber = (page ?? 1);
                    var i = db.Products.SqlQuery("SELECT * FROM dbo.Products  where proPrice <= '500000' and proPrice >= '100000' and typeID='" + Session["id"] + "' and pdcID='" + hangsx + "'  ");

                    return View(i.OrderByDescending(x => x.pdcID).ToPagedList(pageNumber, pageSize));

                }
            }
            //hãng, size
            else if (gia == "null" && size != "null" && hangsx != "null")
            {

                ViewBag.typeID = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeID;
                Session["id"] = ViewBag.typeID;
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                var i = db.Products.SqlQuery("SELECT * FROM dbo.Products  where  typeID='" + Session["id"] + "' and pdcID='" + hangsx + "' and proSize='" + size + "'");

                return View(i.OrderByDescending(x => x.pdcID).ToPagedList(pageNumber, pageSize));

            }
            //size
            else if (gia == "null" && size != "null" && hangsx == "null")
            {
                ViewBag.typeID = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeID;
                Session["id"] = ViewBag.typeID;
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                var i = db.Products.SqlQuery("SELECT * FROM dbo.Products  where  typeID='" + Session["id"] + "'  and proSize='" + size + "'");

                return View(i.OrderByDescending(x => x.pdcID).ToPagedList(pageNumber, pageSize));

            }
            //hãng
            else if (gia == "null" && size == "null" && hangsx != "null")
            {
                ViewBag.typeID = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeID;
                Session["id"] = ViewBag.typeID;
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                var i = db.Products.SqlQuery("SELECT * FROM dbo.Products  where  typeID='" + Session["id"] + "' and pdcID='" + hangsx + "' ");

                return View(i.OrderByDescending(x => x.pdcID).ToPagedList(pageNumber, pageSize));

            }
            //giá
            else if (gia != "null" && size == "null" && hangsx == "null")
            {
                //dưới 100k
                if (gia == "1")
                {
                    ViewBag.typeID = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeID;
                    Session["id"] = ViewBag.typeID;
                    int pageSize = 8;
                    int pageNumber = (page ?? 1);
                    var i = db.Products.SqlQuery("SELECT * FROM dbo.Products  where proPrice < '100000' and typeID='" + Session["id"] + "' ");

                    return View(i.OrderByDescending(x => x.pdcID).ToPagedList(pageNumber, pageSize));
                }
                //trên 500k
                else if (gia == "2")
                {

                    ViewBag.typeID = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeID;
                    Session["id"] = ViewBag.typeID;
                    int pageSize = 8;
                    int pageNumber = (page ?? 1);
                    var i = db.Products.SqlQuery("SELECT * FROM dbo.Products  where proPrice > '500000' and typeID='" + Session["id"] + "' ");

                    return View(i.OrderByDescending(x => x.pdcID).ToPagedList(pageNumber, pageSize));

                }
                //từ 100k đến 500k
                else
                {
                    ViewBag.typeID = db.ProductTypes.SingleOrDefault(t => t.typeID == id).typeID;
                    Session["id"] = ViewBag.typeID;
                    int pageSize = 8;
                    int pageNumber = (page ?? 1);
                    var i = db.Products.SqlQuery("SELECT * FROM dbo.Products  where proPrice <= '500000' and proPrice >= '100000' and typeID='" + Session["id"] + "'  ");

                    return View(i.OrderByDescending(x => x.pdcID).ToPagedList(pageNumber, pageSize));

                }
            }

            return View();
        }
        //hiển thị sản phẩm theo id nhà sản xuất
        public ActionResult ProductsByPdc(int id, int? page)
        {
            ViewBag.pdcName = db.Producers.SingleOrDefault(c => c.pdcID == id).pdcName;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(db.Products.Where(p => p.pdcID == id).OrderByDescending(x => x.pdcID).ToPagedList(pageNumber, pageSize));

        }
        //hiển thị sản phẩm theo tên sp
        public ActionResult SearchByName(string keyword, int? page)
        {

            ViewBag.search = keyword;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var i = db.Products.SqlQuery("SELECT * FROM dbo.Products where dbo.ufn_removeMark(proName) LIKE '%" + keyword + "%' or dbo.ufn_text(proName) LIKE N'%" + keyword + "%' or proName = N'" + keyword + "'");

            return View(i.OrderByDescending(x => x.proName).ToPagedList(pageNumber, pageSize));
        }
        //hiển thị sản phẩm theo tên loại
        public ActionResult ProductsBytypeName(string name, int? page)
        {
            ViewBag.tName = name;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(db.Products.Where(p => p.typeID == db.ProductTypes.FirstOrDefault(t => t.typeName.Equals(name)).typeID).OrderByDescending(x => x.proUpdateDate).ToPagedList(pageNumber, pageSize));
        }
        //Hiển thị sản phẩm mới nhất
        public ActionResult ProductsBestNew(string title, int? page)
        {
            ViewBag.titleDisplay = title;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(db.Products.OrderByDescending(x => x.proUpdateDate).ToPagedList(pageNumber, pageSize));
        }
        //Hiển thị sản phẩm giá thấp nhất
        public ActionResult ProductsBestDiscount(string title, int? page)
        {
            ViewBag.titleDisplayOfDis = title;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var i = db.Products.SqlQuery("SELECT * FROM Products ORDER BY proDiscount DESC");
          
            return View(i.ToPagedList(pageNumber, pageSize));
        }
        //Hiển thị chi tiết sản phẩm
        public ActionResult ProductDetail(string id)
        {
            return View(db.Products.SingleOrDefault(p => p.proID.Equals(id)));
        }
        public JsonResult ListName(string keyword)
        {
            var data = new ShopDAO().ListName(keyword);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

    }
}