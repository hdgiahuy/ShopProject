using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ShopProject.Repository;
namespace ShopProject.Areas.Administrator.Controllers
{
    public class ProductTypeController : Controller
    {
        Models.AdminContext dbType = new Models.AdminContext();
        //
        // GET: /Administrator/ProductType/
        [HandleError]
        public ActionResult Index(int page = 1, int pagesize = 5)
        {
            string a = Session["accname"].ToString();
            var b = dbType.Administrators.SingleOrDefault(x => x.adAcc.Equals(a));
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
                var dao = new ShopDAO();
                var model = dao.ListAllPagingptt(page, pagesize);
                return View(model);
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
                ViewBag.cateListCreate = new SelectList(dbType.Categories, "cateID", "cateName");
                return View();
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Create(Models.ProductType createType, string prtname)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.cateListCreate = new SelectList(dbType.Categories, "cateID", "cateName");
                try
                {
                    EX ex = new EX();
                    var a = ex.prtname(prtname);
                    int b = a.Count();

                    if (b==0)
                        {
                            createType.typeName = prtname;
                            dbType.ProductTypes.Add(createType);

                            dbType.SaveChanges();
                            SetAlert("Thêm mới loại sản phẩm thành công!", "success");
                            return RedirectToAction("Index", "ProductType");
                        }
                    else
                    {
                        ViewBag.CreateTypeError = "Trùng tên loại sản phẩm.";
                    
                }
                   
                }
                catch (Exception)
                {
                    ViewBag.CreateTypeError = "Không thể thêm loại sản phẩm.";
                }
                return View();
            }
        }

        [HandleError]
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
                ViewBag.cateListEdit = new SelectList(dbType.Categories, "cateID", "cateName");
                return View(dbType.ProductTypes.SingleOrDefault(e => e.typeID.Equals(id)));
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Edit(Models.ProductType editType, string prtname)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                EX ex = new EX();
                var a = ex.prtname(prtname);
                 int b= a.Count();
                ViewBag.cateListEdit = new SelectList(dbType.Categories, "cateID", "cateName");
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (b!=0)
                        {
                            ViewBag.EditTypeError = "Trùng tên loại sản phẩm.";
                        }
                        else
                        {
                            editType.typeName = prtname;
                            dbType.Entry(editType).State = System.Data.Entity.EntityState.Modified;
                            dbType.SaveChanges();
                            SetAlert("Cập nhật loại sản phẩm thành công!", "success");
                            return RedirectToAction("Index", "ProductType");
                        }

                    }
                }
                catch (Exception)
                {
                    ViewBag.EditTypeError = "Không thể cập nhật loại sản phẩm.";
                }
                return View();
            }
        }

        [HandleError]
        public ActionResult Delete(int id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var model = dbType.ProductTypes.SingleOrDefault(h => h.typeID.Equals(id));
                try
                {
                    if (model != null)
                    {
                        dbType.ProductTypes.Remove(model);
                        dbType.SaveChanges();
                        return RedirectToAction("Index", "ProductType", new { error = "Xoá loại sản phẩm thành công." });
                    }
                    else
                    {
                      
                        return RedirectToAction("Index", "ProductType", new { error = "Loại sản phẩm không tồn tại." });
                    }
                }
                catch (Exception)
                {
                    TempData["testmsg"] = "<script>alert('Bạn cần xóa tất cả sản phẩm trong loại sản phẩm này!! ');</script>";
                    return RedirectToAction("Index");
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