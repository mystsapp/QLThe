using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Data.Models
{
    public class ChiTietCapThe
    {
        public decimal STT { get; set; }
        //[Key]
        //public string MaCapThe { get; set; }

        [DisplayName("Cấp Thẻ")]
        public string MaCapThe { get; set; }

        [ForeignKey("MaCapThe")]
        public virtual CapThe CapThe { get; set; }

        [DisplayName("Seri Từ")]
        [MaxLength(15), Column(TypeName = "varchar(15)")]
        public string SoSeriTu { get; set; }

        [DisplayName("Seri Đến")]
        [MaxLength(15), Column(TypeName = "varchar(15)")]
        public string SoSeriDen { get; set; }

        [DisplayName("Mệnh Giá")]
        public decimal? MenhGia { get; set; }
        [DisplayName("Số Lượng")]
        public int? SoLuong { get; set; }
    }
}
