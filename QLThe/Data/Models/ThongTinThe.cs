﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLThe.Data.Models
{
    public class ThongTinThe
    {
        [Key]
        [DisplayName("Seri Từ")]
        [MaxLength(15), Column(TypeName = "varchar(15)")]
        public string SoSeri { get; set; }

        //public string MaCapThe { get; set; }

        [DisplayName("Cấp Thẻ")]
        public string MaCapThe { get; set; }

        [ForeignKey("MaCapThe")]
        public virtual CapThe CapThe { get; set; }

        [DisplayName("Giá")]
        public decimal? Gia { get; set; }

        [DisplayName("Người Phát Hành")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string NguoiPhatHanh { get; set; }

        [DisplayName("Ngày Phát Hành")]
        public DateTime? NgayPhatHanh { get; set; }

        [DisplayName("Văn Phòng")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string NoiNhan { get; set; }

        [Display(Name = "Người Nhận")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string NguoiNhan { get; set; }

        [Display(Name = "Trạng Thái")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string Trangthai { get; set; }

        [Display(Name = "Hình 1")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string Hinh1 { get; set; }

        [Display(Name = "Hình 2")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string Hinh2 { get; set; }

        [Display(Name = "Ghi Chú")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string GhiChu { get; set; }
    }
}