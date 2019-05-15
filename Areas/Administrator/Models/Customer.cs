namespace ShopProject.Areas.Administrator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        [Key]
        [StringLength(20)]
        public string cusPhone { get; set; }

        [StringLength(200)]
        public string cusFullName { get; set; }

        [StringLength(100)]
        public string cusEmail { get; set; }

        [StringLength(500)]
        public string cusAddress { get; set; }
    }
}
