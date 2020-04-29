using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrystalMVCReport.Models
{
    public class CapTheViewModel
    {
        public string MaCapThe { get; set; }

        public string NguoiCap { get; set; }
        public DateTime? NgayCap { get; set; }
        public string MaCN { get; set; }
        public int VanPhongId { get; set; }
        public string NguoiNhan { get; set; }
        public int? TongSoLuong { get; set; }
        public string Httt { get; set; }
        public string GhiChu { get; set; }
        public bool? HoaDon { get; set; }
        public string MayTinh { get; set; }
    }
}