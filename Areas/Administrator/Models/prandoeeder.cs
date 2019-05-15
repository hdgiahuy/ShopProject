using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopProject.Areas.Administrator.Models
{
    public class prandoeeder
    {

        public string proPhoto {  set;get; }
        public string proID {  set;get; }
        public string proName {  set;get; }
        public int proPrice {  set;get; }
        public int? proDiscount {  set;get; }
        public int? pdcordtsQuantityID {  set;get; }
        public string ordtsThanhTien {  set;get; }

    }
}