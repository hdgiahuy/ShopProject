using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using ShopProject.Areas.Shopper.Models;
using ShopProject.Repository;
using System.Web.Routing;
using System.Text;
using System.Configuration;

namespace ShopProject.Areas.Shopper.Controllers
{
    public class UserReGController : Controller
    {
        Models.UserContext dbLog = new Models.UserContext();
        Repository.ShopDAO dao = new Repository.ShopDAO();
        // GET: Shopper/UserReGa
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string mk, string mk1, Models.Administrator nd, string accname, string hoten)
        {
            var pro = dbLog.Administrators.SingleOrDefault(c => c.adAcc.Equals(nd.adAcc));
            try
            {
                if (pro != null)
                {
                    ViewBag.CreateUs = "Tài khoản đã tồn tại";
                    return View();
                }
                else
                {
                    if (mk == mk1)
                    {

                        dbLog.Administrators.Add(nd);
                        var mahoa = dao.Encrypt(mk);
                        nd.adAcc = accname;
                        nd.adHoten = hoten;
                        nd.adPass = mk;
                        nd.adNgaytao = DateTime.Now;
                        nd.adStatus = true;
                        nd.adPass = mahoa;
                        nd.adQuyen = 2;
                        dbLog.SaveChanges();
                        ViewBag.Success = "Thêm tài khoản thành công";
                        return View();
                    }
                    else
                    {
                        ViewBag.faild = "Mật khẩu xác nhận không đúng!";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = "Không thể đăng ký tài khoản.";
                return View();
            }
            return View();
        }

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
                    if (model.adStatus == false)
                    {
                        ViewBag.LoginError = "Tài khoản bị khóa vào phản hồi để liên hệ admin.";
                    }
                    else
                    {
                        if (model.adPass.Equals(dao.Encrypt(adLogin.adPass)))
                        {
                            Session["accname"] = model.adAcc;

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            Session["accname"] = null;
                            ViewBag.LoginError = "Sai tài khoản hoặc mật khẩu.";
                        }
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
                ViewBag.LoginError = "Hệ thống lỗi tạm thời chưa đăng nhập được!";
            }
            return View();
        }
        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session["accname"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult changepass()
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                string adAcc = Session["accname"].ToString();
                ViewBag.pdcListEdit = new SelectList(dbLog.Positions, "ID", "Name");
                var update = dbLog.Administrators.SingleOrDefault(p => p.adAcc.Equals(adAcc));
                return View(update);
            }
        }
        [HttpPost]
        public ActionResult changepass(Models.Administrator editAdm, string mk, string mknews, string mkcomplie)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "UserReG");
            }
            else
            {
                ViewBag.pdcListEdit = new SelectList(dbLog.Positions, "ID", "Name");

                try
                {
                    string adAcc = Session["accname"].ToString();
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
                            ViewBag.Success = "Cập nhật tài khoản thành công.";
                            return View();

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

        public ActionResult LoginUser()
        {
            return View();
        }
        [HttpGet]
        public ActionResult updatepass()
        {
            string email = Session["email"].ToString();
            var update = dbLog.Administrators.SingleOrDefault(p => p.adEmail.Equals(email));
            return View(update);

        }

        [HttpPost]
        public ActionResult updatepass(Models.Administrator editAdm, string mk, string mk1, string codemail)
        {

            if (codemail == Session["code"].ToString())
            {
                if (mk == mk1)
                {

                    editAdm.adPass = mk;
                    dbLog.Entry(editAdm).State = System.Data.Entity.EntityState.Modified;
                    var mahoa = dao.Encrypt(editAdm.adPass);
                    editAdm.adPass = mahoa;
                    dbLog.SaveChanges();

                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Mật khẩu xác nhận chưa chính xác";
                }

            }
            else
            {
                ViewBag.error = "Mã khôi phục không đúng!!!";
            }
            return View();
        }
        [HttpPost]
        public ActionResult LoginUser(string email)
        {
            var setmail = dbLog.Administrators.SingleOrDefault(p => p.adEmail.Equals(email));
            if (setmail != null)
            {
                Session["code"] = 123;
                string code = Session["code"].ToString();
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Shopper/Content/template/neworder.html"));
                content = content.Replace("{{code}}", code);
                Session["email"] = email;

                new sendmail().SendMail(email, "Khôi Phục Tài Khoản", content);
                return RedirectToAction("updatepass");
            }
            else
            {
                ViewBag.error = "Email không tồn tại!!!";
                return View();
            }

        }
        [HttpGet]
        public ActionResult Update(string adAcc)
        {
            adAcc = Session["accname"].ToString();
            ViewBag.pdcListEdit = new SelectList(dbLog.Positions, "ID", "Name");
            var update = dbLog.Administrators.SingleOrDefault(p => p.adAcc.Equals(adAcc));
            return View(update);

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
                    ViewBag.succ = "Cập nhật thông tin người dùng thành công!";
                    return View();

                }
                catch (Exception)
                {
                    ViewBag.error = "Không thể cập nhật sản phẩm.";
                }
                return View();
            }
        }

    }
}