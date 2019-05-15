namespace ShopProject.Areas.Shopper.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Administrator")]
    public partial class Administrator
    {
        [Key]
        [StringLength(500)]
        public string adAcc { get; set; }

        [Required]
        [StringLength(500)]
        public string adPass { get; set; }

        public bool adStatus { get; set; }

        public int adQuyen { get; set; }

        [StringLength(100)]
        public string adHoten { get; set; }

        [StringLength(50)]
        public string adEmail { get; set; }

        [StringLength(250)]
        public string adDiaChi { get; set; }

        [StringLength(20)]
        public string adSDT { get; set; }

        public DateTime? adNgaytao { get; set; }

        public virtual Position Position { get; set; }
    }
}
