using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Data.Models
{
    public class LoaiThe
    {
        [Key]
        [DisplayName("Mã Loại")]
        [MaxLength(5), Column(TypeName = "varchar(5)")]
        public string MaLoai { get; set; }

        [DisplayName("Tên Loại")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string TenLoai { get; set; }

        [DisplayName("Ghi chú")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string Ghichu { get; set; }
    }
}
