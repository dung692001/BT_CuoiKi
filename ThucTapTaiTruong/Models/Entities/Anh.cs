namespace ThucTapTaiTruong.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Anh")]
    public partial class Anh
    {
        [Key]
        public int MaAnh { get; set; }

        public int MaSanPham { get; set; }

        [Required]
        [StringLength(50)]
        public string TenAnh { get; set; }

        [StringLength(250)]
        public string URL { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
