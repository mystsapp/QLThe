using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLThe.Data.Models
{
    public class CapThe
    {
        [Key]
        [MaxLength(20), Column(TypeName = "varchar(20)")]
        [Display(Name = "Mã Số")]
        public string MaCapThe { get; set; }

        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Người Cấp")]
        public string NguoiCap { get; set; }

        [Display(Name = "Ngày Cấp")]
        public DateTime? NgayCap { get; set; }

        [Display(Name = "Chi Nhánh")]
        [MaxLength(10), Column(TypeName = "varchar(10)")]
        public string MaCN { get; set; }

        [Required]
        [DisplayName("Văn Phòng")]
        public int VanPhongId { get; set; }

        [ForeignKey("VanPhongId")]
        public virtual VanPhong VanPhong { get; set; }

        [Display(Name = "Người Nhận")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string NguoiNhan { get; set; }

        [Display(Name = "T.Số Lượng")]
        public int? TongSoLuong { get; set; }

        [Display(Name = "HTTT")]
        public string Httt { get; set; }

        [Display(Name = "Ghi Chú")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string GhiChu { get; set; }

        [Display(Name = "Hóa Đơn")]
        public bool? HoaDon { get; set; }

        [Display(Name = "Máy Tính")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string MayTinh { get; set; }
    }
}