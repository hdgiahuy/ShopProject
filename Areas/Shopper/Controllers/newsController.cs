using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopProject.Areas.Shopper.Models;
using ShopProject.Repository;
using PagedList;
namespace ShopProject.Areas.Shopper.Controllers
{
    public class newsController : Controller
    {
        Models.UserContext db = new UserContext();
        // GET: Shopper/news
        public ActionResult Index(string SearchString, int page = 1, int pagesize = 5)
        {
            ViewBag.pdcListCreate = new SelectList(db.Themes, "ID", "TenChuDe");
            var dao = new ShopDAO();
            var model = dao.ListAllPagingnews(SearchString, page, pagesize);
            ViewBag.SearchString = SearchString;
            return View(model);
        }
        public ActionResult View(int id)
        {
            var model = db.News.SingleOrDefault(p => p.ID.Equals(id));
            return View(model);
        }

    }
}