using QLThe.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Dtos
{
    public class CapTheDto
    {
        [Display(Name = "Mã số")]
        public string MaCapThe { get; set; }
        [Display(Name = "Người cấp")]
        public string NguoiCap { get; set; }

        [Display(Name = "Ngày cấp")]
        public DateTime? NgayCap { get; set; }

        [Display(Name = "Chi nhánh")]
        public string ChiNhanh { get; set; }

        [Display(Name = "Văn phòng")]
        public string VanPhong { get; set; }

        [Display(Name = "Người nhận")]
        public string NguoiNhan { get; set; }

        [Display(Name = "T.Số lượng")]
        public int? TongSoLuong { get; set; }

        [Display(Name = "HTTT")]
        public string Httt { get; set; }
        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        [Display(Name = "Hóa đơn")]
        public bool? HoaDon { get; set; }
        public string MayTinh { get; set; }

        public ICollection<ChiTietCapThe> ChiTietCapThe { get; set; }
        public ICollection<ThongTinThe> ThongTinThe { get; set; }

        public string TienBangChu { get; set; }
    }
}
