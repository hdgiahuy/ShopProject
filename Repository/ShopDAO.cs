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
    public class ShopDAO
    {
        AdminContext db = null;
        public ShopDAO()
        {
            db = new AdminContext();
        }
        public string Encrypt(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(str));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
        //Administrator
        //Administrator list page
        public IEnumerable<Administrator> ListAllPaging(string SearchString, int page, int pagesize)
        {
            IQueryable<Administrator> model = db.Administrators;
            if (!String.IsNullOrEmpty(SearchString))
            {
                model = model.Where(x => x.adAcc.Contains(SearchString));
            }
            return model.OrderByDescending(x => x.adNgaytao).ToPagedList(page, pagesize);
        }

        public bool thaydoitrangthaiTk(string id)
        {
            var nguoidung = db.Administrators.Find(id);
            nguoidung.adStatus = !nguoidung.adStatus;
            db.SaveChanges();
            return nguoidung.adStatus;
        }
        //Custummer list page
        public IEnumerable<Customer> ListAllPagingctm(string SearchString, int page, int pagesize)
        {
            IQueryable<Customer> modelctm = db.Customers;
            if (!String.IsNullOrEmpty(SearchString))
            {
                modelctm = modelctm.Where(x => x.cusPhone.Contains(SearchString));
            }
            return modelctm.OrderByDescending(x => x.cusPhone).ToPagedList(page, pagesize);
        }
        //Order
        public IEnumerable<Order> ListAllPagingoder(string SearchString, int page, int pagesize)
        {
            IQueryable<Order> modelctm = db.Orders;
            if (!String.IsNullOrEmpty(SearchString))
            {
                modelctm = modelctm.Where(x => x.orderDateTime.Contains(SearchString));
            }
            return modelctm.OrderByDescending(x => x.orderID).ToPagedList(page, pagesize);
        }
        //ProductType listpage
        public IEnumerable<ProductType> ListAllPagingptt( int page, int pagesize)
        {
            IQueryable<ProductType> modeptt = db.ProductTypes;

            return modeptt.OrderByDescending(x => x.typeID).ToPagedList(page, pagesize);
        }
        //Product list page
        public IEnumerable<Product> ListAllPagingpd(string SearchString, int page, int pagesize)
        {
            IQueryable<Product> modelpd = db.Products;
            if (!String.IsNullOrEmpty(SearchString))
            {
                modelpd = modelpd.Where(x => x.proName.Contains(SearchString));
            }
            return modelpd.OrderByDescending(x => x.proID).ToPagedList(page, pagesize);
        }
        //News list page\
        public IEnumerable<News> ListAllPagingnews(string SearchString, int page, int pagesize)
        {
            IQueryable<News> modelpd = db.News;
            if (!String.IsNullOrEmpty(SearchString))
            {
                modelpd = modelpd.Where(x => x.TieuDe.Contains(SearchString));
            }
            return modelpd.OrderByDescending(x => x.ID).ToPagedList(page, pagesize);
        }

        //Kiểm tra ml
        public string changepass(string adAcc)
        {
            var nguoidung = db.Administrators.Find(adAcc);
           
          
            return nguoidung.adPass;
        }

        public int quyen(string accname)
        {
            var quyen = db.Administrators.Find(accname);
            return quyen.adQuyen;
        }

        //List ds tim kiem
        public List<string> ListName(string keyword)
        {
            var i= db.Products.SqlQuery("SELECT * FROM dbo.Products where dbo.ufn_removeMark(proName) LIKE '%" + keyword + "%' or dbo.ufn_text(proName) LIKE N'%" + keyword + "%' or proName = N'" + keyword + "'");
            return i.Select(x => x.proName).ToList();
        }
        //thông tin tài khoản khi thanh toán
        public Administrator tkthanhtoan(string accname)
        {
             
            return db.Administrators.Find(accname);;
        }
        //thông tin đơn hàng
        public List<prandoeeder> LisByOder(int id)
        {
            var model = from a in db.OrderDetails join b in db.Products
                        on a.proID equals b.proID
                        where a.proID == b.proID && a.orderID == id
                        select new prandoeeder()
                        {
                            proPhoto = b.proPhoto,
                            proID = b.proID,
                            proName = b.proName,
                            proPrice = b.proPrice,
                            proDiscount =   b.proDiscount,
                            pdcordtsQuantityID  = a.ordtsQuantity,
                            ordtsThanhTien=  a.ordtsThanhTien
                        };

            return model.ToList();
        }

        public List<string> hduser(string adacc)
        {
            var i = db.Orders.SqlQuery("SELECT * FROM dbo.Orders where adAcc='"+adacc+"'");
            return i.Select(x => x.adAcc).ToList();
        }
    }
}