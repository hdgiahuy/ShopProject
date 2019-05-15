namespace ShopProject.Areas.Administrator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        public int ID { get; set; }

        public int IDChuDe { get; set; }

        [Required]
        [StringLength(255)]
        public string TieuDe { get; set; }

        [Column(TypeName = "ntext")]
        public string TomTat { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        public DateTime NgayDang { get; set; }

        public virtual Theme Theme { get; set; }
    }
}
