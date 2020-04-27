﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Data.Models
{
    public class VanPhong
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10), Column(TypeName = "varchar(10)")]
        [DisplayName("Mã VP")]
        public string MaVP { get; set; }
        [Required]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        [DisplayName("Văn Phòng")]
        public string Name { get; set; }

        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [MaxLength(15)]
        [DisplayName("Điện Thoại")]
        public string DienThoai { get; set; }

        [DisplayName("Chi Nhánh")]
        public int ChiNhanhId { get; set; }

        [ForeignKey("ChiNhanhId")]
        public virtual ChiNhanh ChiNhanh { get; set; }

        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string Role { get; set; }

    }
}
