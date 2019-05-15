namespace ShopProject.Areas.Shopper.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public int orderID { get; set; }

        public string orderMessage { get; set; }

        [StringLength(50)]
        public string orderDateTime { get; set; }

        [StringLength(50)]
        public string orderStatus { get; set; }


        [StringLength(100)]
        public string orderName { get; set; }

        public string orderDiachi { get; set; }
        
        [StringLength(50)]
        public string orderSDT { get; set; }
        [StringLength(500)]
        public string adAcc { get; set; }
    }
}
