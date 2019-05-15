using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopProject.Areas.Administrator.Models;
using ShopProject.Repository;
using PagedList;
namespace ShopProject.Areas.Administrator.Controllers
{


    public class NewsController : Controller
    {
        Models.AdminContext news = new Models.AdminContext();
        // GET: Administrator/News
        public ActionResult Index(string SearchString, int page = 1, int pagesize = 5)
        {
            string a = Session["accname"].ToString();
            var b = news.Administrators.SingleOrDefault(x => x.adAcc.Equals(a));
            if (b.adQuyen == 2)
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
                    ViewBag.pdcListCreate = new SelectList(news.Themes, "ID", "TenChuDe");
                    var dao = new ShopDAO();
                    var model = dao.ListAllPagingnews(SearchString, page, pagesize);
                    ViewBag.SearchString = SearchString;
                    return View(model);

                }
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.pdcListCreate = new SelectList(news.Themes, "ID", "TenChuDe");

                return View();
            }
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Models.News createNew)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.pdcListCreate = new SelectList(news.Themes, "ID", "TenChuDe");

                var pro = news.News.SingleOrDefault(c => c.TieuDe.Equals(createNew.TieuDe));

                createNew.NgayDang = DateTime.Now;
                try
                {
                    if (pro != null)
                    {
                        ViewBag.CreateProError = "Tên tiêu đề trùng.";
                        return View();
                    }
                    else
                    {
                        news.News.Add(createNew);
                        news.SaveChanges();
                        SetAlert("Thêm tin tức thành công","success");
                        return RedirectToAction("Index", "News");
                    }
                }
                catch (Exception)
                {
                    ViewBag.CreateProError = "Không thể thêm sản phẩm.";
                }
            }

            return View();
        }
        public ActionResult Delete(int id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var model = news.News.SingleOrDefault(h => h.ID.Equals(id));
                try
                {
                    if (model != null)
                    {
                        news.News.Remove(model);
                        news.SaveChanges();
                        return RedirectToAction("Index", "News", new { error = "Xoá sản phẩm thành công." });
                    }
                    else
                    {
                        return RedirectToAction("Index", "News", new { error = "Sản phẩm không tồn tại." });
                    }
                }
                catch (Exception)
                {

                    return RedirectToAction("Index", "News", new { error = "Không thể xoá sản phẩm." });
                }
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.pdcListEdit = new SelectList(news.Themes, "ID", "TenChuDe");

                var model = news.News.SingleOrDefault(p => p.ID.Equals(id));
                return View(model);
            }
        }

        [HandleError]
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Models.News editPro)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.pdcListEdit = new SelectList(news.Themes, "ID", "TenChuDe");
               
                
                
                try
                {
                    news.Entry(editPro).State = System.Data.Entity.EntityState.Modified;
                    news.SaveChanges();
                    SetAlert("Cập nhật tin tuc thành công!", "success");
                    return RedirectToAction("Index", "News");
                }
                catch (Exception)
                {
                    ViewBag.EditProError = "Không thể cập nhật tin tức.";
                }
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var model = news.News.SingleOrDefault(p => p.ID.Equals(id));
                return View(model);
            }
        }
        private void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}
