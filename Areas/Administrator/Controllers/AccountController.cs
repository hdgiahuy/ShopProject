using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopProject.Areas.Administrator.Models;
using ShopProject.Repository;
using PagedList;
using System.IO;
using System.Web.Routing;


namespace ShopProject.Areas.Administrator.Controllers
{
    public class AccountController : Controller
    {
        Models.AdminContext dbLog = new Models.AdminContext();
        Repository.ShopDAO dao = new Repository.ShopDAO();

        public ActionResult Index()
        {
            string a = Session["accname"].ToString();
            var b = dbLog.Administrators.SingleOrDefault(x => x.adAcc.Equals(a));
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
                    var modelpro = dbLog.Administrators.ToList();
                    return View(modelpro);
                }
            }

        }

        // GET: Administrator/Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Login(Models.Administrator adLogin)
        {
            try
            {
                var model = dbLog.Administrators.SingleOrDefault(a => a.adAcc.Equals(adLogin.adAcc));
                if (model != null)
                {
                    if (model.adPass.Equals(dao.Encrypt(adLogin.adPass)))
                    {
                        Session["accname"] = model.adAcc;
                        if (model.adQuyen == 1)
                        {
                            return RedirectToAction("Index", "Home");

                        }
                        else
                        {
                            return RedirectToRoute("Home");
                        }


                    }
                    else
                    {
                        Session["accname"] = null;
                        ViewBag.LoginError = "Sai tài khoản hoặc mật khẩu.";
                    }
                }
                else
                {
                    Session["accname"] = null;
                    ViewBag.LoginError = "Sai tài khoản hoặc mật khẩu.";
                }
            }
            catch (Exception)
            {
                Session["accname"] = null;
                ViewBag.LoginError = "Sai tài khoản hoặc mật khẩu.";
            }
            return View();
        }

        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session["accname"] = null;
            return RedirectToAction("Login", "Account");
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
                ViewBag.pdcListEdit = new SelectList(dbLog.Positions, "ID", "Name");
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create(Models.Administrator nd, string name, string mk, string diachi, string mail, string sdt, string hoten)
        {

            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.pdcListEdit = new SelectList(dbLog.Positions, "ID", "Name");
                var pro = dbLog.Administrators.SingleOrDefault(c => c.adAcc.Equals(name));
                try
                {
                    if (pro != null)
                    {
                        ViewBag.CreatePdcError = "Tài khoản đã tồn tại";
                        return View();
                    }
                    else
                    {
                        nd.adAcc = name;
                        nd.adPass = mk;
                        nd.adHoten = hoten;
                        nd.adEmail = mail;
                        nd.adDiaChi = diachi;
                        nd.adSDT = sdt;
                        dbLog.Administrators.Add(nd);
                        var mahoa = dao.Encrypt(nd.adPass);
                        nd.adNgaytao = DateTime.Now;
                        nd.adStatus = true;
                        nd.adPass = mahoa;
                        dbLog.SaveChanges();
                        SetAlert("Thêm mới danh mục thành công!", "success");
                        return RedirectToAction("Index", "Account");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.CreateProError = ".";
                    return View();
                }

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
                var delete = dbLog.Administrators.SingleOrDefault(h => h.adAcc.Equals(id));
                try
                {
                    if (delete != null)
                    {
                        dbLog.Administrators.Remove(delete);
                        dbLog.SaveChanges();
                        return RedirectToAction("Index", "Account", new { error = "Xoá sản phẩm thành công." });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Acount", new { error = "Sản phẩm không tồn tại." });
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Account", new { error = "Không thể xoá sản phẩm." });
                }
            }
        }

        [HttpGet]
        public ActionResult Update(string adAcc)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.pdcListEdit = new SelectList(dbLog.Positions, "ID", "Name");
                var update = dbLog.Administrators.SingleOrDefault(p => p.adAcc.Equals(adAcc));
                return View(update);
            }
        }

        [HttpPost]
        public ActionResult Update(Models.Administrator editAdm)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.pdcListEdit = new SelectList(dbLog.Positions, "ID", "Name");

                try
                {

                    dbLog.Entry(editAdm).State = System.Data.Entity.EntityState.Modified;

                    dbLog.SaveChanges();
                    SetAlert("Cập nhật thông tin người dùng thành công!", "success");
                    return RedirectToAction("Index", "Account");

                }
                catch (Exception)
                {
                    ViewBag.EditProError = "Không thể cập nhật sản phẩm.";
                }
                return View();
            }
        }

        [HttpPost]
        public JsonResult thaydoitrangthaiTK(string id)
        {
            var kq = new ShopDAO().thaydoitrangthaiTk(id);
            return Json(new
            {
                status = kq

            }
                  );

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



        ////thay đổi pass cho tài khoản admin
        [HttpGet]
        public ActionResult Changepass(string adAcc)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.pdcListEdit = new SelectList(dbLog.Positions, "ID", "Name");
                var update = dbLog.Administrators.SingleOrDefault(p => p.adAcc.Equals(adAcc));
                return View(update);
            }
        }
        [HttpPost]
        public ActionResult Changepass(Models.Administrator editAdm, string mk, string mknews, string mkcomplie, string adAcc)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.pdcListEdit = new SelectList(dbLog.Positions, "ID", "Name");

                try
                {
                    var adpass = dao.changepass(adAcc);

                    if (dao.Encrypt(mk) == adpass)
                    {
                        if (mknews == mkcomplie)
                        {
                            editAdm.adPass = mknews;
                            dbLog.Entry(editAdm).State = System.Data.Entity.EntityState.Modified;
                            var mahoa = dao.Encrypt(editAdm.adPass);
                            editAdm.adPass = mahoa;
                            dbLog.SaveChanges();
                            SetAlert("Cập nhật mật khẩu thành công!", "success");
                            return RedirectToAction("Index", "Home");

                        }
                        else
                        {
                            ViewBag.EditProError = "Mật khẩu xác nhận không đúng.";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.EditProError = "Sai mật khẩu.";
                        return View();
                    }


                }
                catch (Exception)
                {
                    ViewBag.EditProError = "Không thể cập nhật mật khẩu.";
                }
                return View();
            }
        }
    }
}