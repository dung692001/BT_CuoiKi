namespace ThucTapTaiTruong.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        [Key]
        public int MaHoaDon { get; set; }

        public int MaThanhToan { get; set; }

        public int MaND { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayDat { get; set; }

        [Column(TypeName = "money")]
        public decimal? TongTien { get; set; }

        public bool? TrangThaiHD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        public virtual ThanhToan ThanhToan { get; set; }
    }
}
