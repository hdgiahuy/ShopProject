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
    public class EX
    {
        AdminContext db = new AdminContext();
        public List<string> prtname(string prtname)
        {
            var i = db.ProductTypes.SqlQuery("SELECT * FROM ProductTypes where dbo.ufn_removeMark(typeName) LIKE '%"+prtname+"%' or dbo.ufn_text(typeName) LIKE N'%"+prtname+"%' or typeName = N'"+prtname+"'");
            return i.Select(x => x.typeName).ToList();
        }
        //
        public List<string> theme(string name)
        {
            var i = db.Themes.SqlQuery("SELECT * FROM Theme where dbo.ufn_removeMark(TenChuDe) LIKE '%" + name + "%' or dbo.ufn_text(TenChuDe) LIKE N'%" + name + "%' or TenChuDe = N'" + name + "'");
            return i.Select(x => x.TenChuDe).ToList();
        }

        public string thaydoitrangthaiTk(int id)
        {
            string huy = "Đã hủy";
            string xetduyet = "Chờ phê duyệt";
            string vanchuyen = "Chuyển hàng";
            string thanhtoan = "Đă thanh toán";
            var hd = db.Orders.Find(id);
            if (hd.orderStatus == "Chờ phê duyệt")
            {
                hd.orderStatus = vanchuyen;
            }
            else if (hd.orderStatus == "Chuyển hàng")
            {
                hd.orderStatus = thanhtoan;
            } else if (hd.orderStatus == "Đă thanh toán")
            {
                hd.orderStatus = huy;
            }
            else 
            {
                hd.orderStatus = xetduyet;
            }
                db.SaveChanges();
            return hd.orderStatus;
        }
    }
}