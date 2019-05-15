using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopProject.Areas.Administrator.Models;
using ShopProject.Repository;
namespace ShopProject.Areas.Administrator.Controllers
{
    public class ThemeController : Controller
    {
        Models.AdminContext dbCate = new Models.AdminContext();
        // GET: Administrator/Theme
        public ActionResult Index()
        {
            string a = Session["accname"].ToString();
            var b = dbCate.Administrators.SingleOrDefault(x => x.adAcc.Equals(a));
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

                var modelCate = dbCate.Themes.ToList();
                return View(modelCate);
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
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create(Models.Theme createCate)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {

                try
                {

                    EX ex = new EX();
                    var a = ex.theme(createCate.TenChuDe);
                    int b = a.Count();
                    if (b == 0)
                    {
                        dbCate.Themes.Add(createCate);
                        dbCate.SaveChanges();
                        SetAlert("Thêm mới chủ đề thành công!", "success");
                        return RedirectToAction("Index", "Theme");


                    }
                    else
                    {
                        ViewBag.Createtheme = "Tên danh mục đã tồn tại.";
                    }


                }
                catch (Exception)
                {
                    ViewBag.CreateCategory = "Không thể thêm danh mục.";
                }

                return View();
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
                var model = dbCate.Themes.SingleOrDefault(c => c.ID.Equals(id));
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit(Models.Theme editCate)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {

                try
                {
                    EX ex = new EX();
                    var a = ex.prtname(editCate.TenChuDe);
                    int b = a.Count();
                    if (b == 0)
                    {
                        dbCate.Entry(editCate).State = System.Data.Entity.EntityState.Modified;
                        dbCate.SaveChanges();
                        SetAlert("Cập nhật chủ đề thành công!", "success");
                        return RedirectToAction("Index", "Theme");
                    }
                    else
                    {
                        ViewBag.CreateCategory = "Tên danh mục đã tồn tại.";
                    }


                }
                catch (Exception)
                {
                    ViewBag.EditCategory = "Không thể cập nhật danh mục.";
                }
                return View();
            }
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
                var model = dbCate.Themes.SingleOrDefault(h => h.ID.Equals(id));
                try
                {
                    if (model != null)
                    {
                        dbCate.Themes.Remove(model);
                        dbCate.SaveChanges();
                        return RedirectToAction("Index", "Theme", new { error = "Xoá danh mục thành công." });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Theme", new { error = "Danh mục không tồn tại." });
                    }
                }
                catch (Exception)
                {
                    TempData["testmsg"] = "<script>alert('Bạn cần xóa tin tức của chủ đề này trước!! ');</script>";

                    return RedirectToAction("Index", "Theme");
                }
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