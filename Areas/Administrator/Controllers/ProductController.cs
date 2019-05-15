using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

using System.IO;
namespace ShopProject.Areas.Administrator.Controllers
{
    public class ProductController : Controller
    {
        Models.AdminContext dbPro = new Models.AdminContext();
        ShopProject.Repository.ShopDAO shopDAO = new Repository.ShopDAO();
        //
        // GET: /Administrator/Product/
        [HandleError]
        public ActionResult Index()
        {
            string a = Session["accname"].ToString();
            var b = dbPro.Administrators.SingleOrDefault(x => x.adAcc.Equals(a));
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

                    var modelpro = dbPro.Products.ToList();
                    return View(modelpro);

                }
            }

        }

        [HandleError]
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
                ViewBag.pdcListCreate = new SelectList(dbPro.Producers, "pdcID", "pdcName");
                ViewBag.typeListCreate = new SelectList(dbPro.ProductTypes, "typeID", "typeName");
                return View();
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Create(Models.Product createPro, HttpPostedFileBase file, string masp, string namepro, string size, int gia, int giam)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.pdcListCreate = new SelectList(dbPro.Producers, "pdcID", "pdcName");
                ViewBag.typeListCreate = new SelectList(dbPro.ProductTypes, "typeID", "typeName");
                var pro = dbPro.Products.SingleOrDefault(c => c.proID.Equals(masp));
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        try
                        {
                            string nameFile = Path.GetFileName(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath("/Image"), nameFile));
                            createPro.proPhoto = "/Image/" + nameFile;
                        }
                        catch (Exception)
                        {
                            ViewBag.CreateProError = "Không thể chọn ảnh.";
                        }
                    }
                    createPro.proUpdateDate = DateTime.Now.ToString();
                    try
                    {
                        if (pro != null)
                        {
                            ViewBag.CreateProError = "Mã sản phẩm đã tồn tại.";
                        }
                        else
                        {
                            createPro.proID = masp;
                            createPro.proName = namepro;
                            createPro.proSize = size;
                            createPro.proPrice = gia;
                            createPro.proDiscount = giam;
                            dbPro.Products.Add(createPro);
                            dbPro.SaveChanges();
                            SetAlert("Thêm mới sản phẩm thành công!", "success");
                            return RedirectToAction("Index", "Product");
                        }
                    }
                    catch (Exception)
                    {
                        ViewBag.CreateProError = "Không thể thêm sản phẩm.";
                    }
                }
                else
                {
                    ViewBag.HinhAnh = "Vui lòng chọn hình ảnh.";
                }
                return View();
            }
        }

        [HandleError]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.pdcListEdit = new SelectList(dbPro.Producers, "pdcID", "pdcName");
                ViewBag.typeListEdit = new SelectList(dbPro.ProductTypes, "typeID", "typeName");
                var model = dbPro.Products.SingleOrDefault(p => p.proID.Equals(id));
                return View(model);
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Edit(Models.Product editPro, HttpPostedFileBase file)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.pdcListEdit = new SelectList(dbPro.Producers, "pdcID", "pdcName");
                ViewBag.typeListEdit = new SelectList(dbPro.ProductTypes, "typeID", "typeName");
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        try
                        {
                            string nameFile = Path.GetFileName(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath("/Image"), nameFile));
                            editPro.proPhoto = "/Image/" + nameFile;
                        }
                        catch (Exception)
                        {
                            ViewBag.EditProError = "Không thể chọn ảnh.";
                        }
                    }
                }
                editPro.proUpdateDate = DateTime.Now.ToString();
                try
                {
                    dbPro.Entry(editPro).State = System.Data.Entity.EntityState.Modified;
                    dbPro.SaveChanges();
                    SetAlert("Cập nhật sản phẩm thành công!", "success");
                    return RedirectToAction("Index", "Product");
                }
                catch (Exception)
                {
                    ViewBag.EditProError = "Không thể cập nhật sản phẩm.";
                }
                return View();
            }
        }


        public ActionResult Delete(string id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var model = dbPro.Products.SingleOrDefault(h => h.proID.Equals(id));
                try
                {
                    if (model != null)
                    {
                        dbPro.Products.Remove(model);
                        dbPro.SaveChanges();
                        return RedirectToAction("Index", "Product", new { error = "Xoá sản phẩm thành công." });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Product", new { error = "Sản phẩm không tồn tại." });
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Product", new { error = "Không thể xoá sản phẩm." });
                }
            }
        }

        public ActionResult Details(string id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var model = dbPro.Products.SingleOrDefault(p => p.proID.Equals(id));
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