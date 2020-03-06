using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLThe.Data.Models
{
    public class TrangThai
    {
        [Key]
        [MaxLength(5), Column(TypeName = "varchar(5)")]
        public string MaTt { get; set; }

        [DisplayName("Tên Trạng Thái")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string TenTt { get; set; }

        [DisplayName("Ghi Chú")]
        [MaxLength(150), Column(TypeName = "nvarchar(150)")]
        public string GhiChu { get; set; }
    }
}