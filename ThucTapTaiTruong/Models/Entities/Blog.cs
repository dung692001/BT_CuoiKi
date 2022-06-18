namespace ThucTapTaiTruong.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Blog")]
    public partial class Blog
    {
        public int BlogID { get; set; }

        [Required]
        [StringLength(50)]
        public string TieuDe { get; set; }

        [Required]
        [StringLength(255)]
        public string NoiDung { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayViet { get; set; }
    }
}
