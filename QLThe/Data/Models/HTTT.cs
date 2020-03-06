using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Data.Models
{
    public class HTTT
    {
        [Key]
        [DisplayName("Mã HT")]
        [MaxLength(5), Column(TypeName = "varchar(5)")]
        public string MaHT { get; set; }

        [DisplayName("Tên HT")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string TenHT { get; set; }
    }
}
