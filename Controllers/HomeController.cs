using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopProject.Areas.Administrator.Models;
using ShopProject.Repository;
namespace ShopProject.Controllers
{
    public class HomeController : Controller
    {
        AdminContext dbLog = new AdminContext();
        Repository.ShopDAO dao = new Repository.ShopDAO();
        // GET: Home

        public ActionResult Index()
        {
            return View();
        }
       
    }
}