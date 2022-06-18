namespace ThucTapTaiTruong.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            Anhs = new HashSet<Anh>();
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            GioHangs = new HashSet<GioHang>();
            ChiTietDSYTs = new HashSet<ChiTietDSYT>();
            DanhGias = new HashSet<DanhGia>();
        }

        [Key]
        public int MaSanPham { get; set; }

        public int MaTheLoai { get; set; }

        [Required]
        [StringLength(50)]
        public string TenSP { get; set; }

        public int Gia { get; set; }

        public int? KhuyenMai { get; set; }

        public int SoLuong { get; set; }

        [StringLength(250)]
        public string MoTa { get; set; }

        public bool? Xoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Anh> Anhs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDSYT> ChiTietDSYTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGia> DanhGias { get; set; }

        public virtual TheLoai TheLoai { get; set; }
    }
}
