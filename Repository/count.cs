using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using PagedList;
using ShopProject.Areas.Administrator.Models;
namespace ShopProject.Repository
{
    public class count
    {
       AdminContext db = new AdminContext();
        public int ad()
        {
            return db.Administrators.ToList().Count();
        }
        public int sp()
        {
            return db.Products.ToList().Count();
        }
        public int od()
        {
            return db.Orders.ToList().Count();
        }
    }
}