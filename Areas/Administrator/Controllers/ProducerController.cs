using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopProject.Areas.Administrator.Controllers
{
    public class ProducerController : Controller
    {
        Models.AdminContext dbPdc = new Models.AdminContext();
        //
        // GET: /Administrator/Producer/
        [HandleError]
        public ActionResult Index(string error)
        {
            string a = Session["accname"].ToString();
            var b = dbPdc.Administrators.SingleOrDefault(x => x.adAcc.Equals(a));
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
                ViewBag.PdcError = error;
                return View(dbPdc.Producers.ToList());
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
                return View();
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Create(Models.Producer createPdc, HttpPostedFileBase file)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        try
                        {
                            string nameFile = Path.GetFileName(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath("/Image"), nameFile));
                            createPdc.pdcPhoto = "/Image/" + nameFile;
                        }
                        catch (Exception)
                        {
                            ViewBag.CreatePdcError = "Không thể chọn ảnh.";
                        }
                    }
                    var pdc = dbPdc.Producers.SingleOrDefault(c => c.pdcName.Equals(createPdc.pdcName));
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            if (pdc != null)
                            {
                                ViewBag.CreatePdcError = "Hãng sản xuất đã tồn tại.";
                            }
                            else if(createPdc.pdcName!=null)
                            {
 dbPdc.Producers.Add(createPdc);
                                dbPdc.SaveChanges();
                                SetAlert("Thêm mới sản phẩm thành công!", "success");
                                return RedirectToAction("Index", "Producer");
                            }
                            else
                            {
                                ViewBag.CreatePdcError = "Tên hãng sản xuất không được bỏ trống.";
                            }
                                
                               
                            
                        }
                    }
                    catch (Exception)
                    {
                        ViewBag.CreatePdcError = "Không thể thêm hãng sản xuất.";
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
        public ActionResult Edit(int id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View(dbPdc.Producers.SingleOrDefault(e => e.pdcID.Equals(id)));
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Edit(Models.Producer editPdc, HttpPostedFileBase file)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        try
                        {
                            string nameFile = Path.GetFileName(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath("/Image"), nameFile));
                            editPdc.pdcPhoto = "/Image/" + nameFile;
                        }
                        catch (Exception)
                        {
                            ViewBag.EditPdcError = "Không thể chọn ảnh.";
                        }
                    }
                }
                try
                {
                    if (ModelState.IsValid)
                    {
                        dbPdc.Entry(editPdc).State = System.Data.Entity.EntityState.Modified;
                        dbPdc.SaveChanges();
                        SetAlert("Cập nhật nhà sản xuất thành công!", "success");
                        return RedirectToAction("Index", "Producer");
                    }
                }
                catch (Exception)
                {
                    ViewBag.EditPdcError = "Không thể cập nhật hãng sản xuất.";
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
                var model = dbPdc.Producers.SingleOrDefault(h => h.pdcID.Equals(id));
                try
                {
                    if (model != null)
                    {
                        dbPdc.Producers.Remove(model);
                        dbPdc.SaveChanges();
                        return RedirectToAction("Index", "Producer");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Producer", new { error = "Hãng sản xuất không tồn tại." });
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Producer", new { error = "Không thể xoá hãng sản xuất." });
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