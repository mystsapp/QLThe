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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal STT { get; set; }
        //[Key]
        //public string MaCapThe { get; set; }

        [DisplayName("Mã cấp thẻ")]
        public string MaCapThe { get; set; }

        [ForeignKey("MaCapThe")]
        public virtual CapThe CapThe { get; set; }

        [DisplayName("Seri từ")]
        [MaxLength(15), Column(TypeName = "varchar(15)")]
        public string SoSeriTu { get; set; }

        [DisplayName("Seri đến")]
        [MaxLength(15), Column(TypeName = "varchar(15)")]
        public string SoSeriDen { get; set; }

        [DisplayName("Mệnh giá")]
        public decimal? MenhGia { get; set; }
        [DisplayName("Số lượng")]
        public int? SoLuong { get; set; }
    }
}
