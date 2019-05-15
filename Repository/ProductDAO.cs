using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopProject.Repository
{
    public class ProductDAO
    {
        public string formatNumber(int strNum)
        {
              string a=  Convert.ToInt32(strNum).ToString("#,###");
            return a;
        }
    }
}