using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username không được bỏ trống.")]
        [MaxLength(50, ErrorMessage = "Không vượt qua 50 ký tự.")]
        [Remote("UsersExists", "Users", ErrorMessage = "User đã tồn tại")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password không được bỏ trống.")]
        [MaxLength(50, ErrorMessage = "Không vượt qua 50 ký tự.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Họ tên không được bỏ trống.")]
        [MaxLength(50, ErrorMessage = "Không vượt qua 50 ký tự.")]
        [DisplayName("Họ Tên")]
        public string HoTen { get; set; }

        //public string Daily { get; set; }

        [MaxLength(10), Column(TypeName = "varchar(10)")]
        [DisplayName("Mã CN")]
        public string MaCN { get; set; }

        [DisplayName("Chi Nhánh")]
        public int ChiNhanhId { get; set; }

        [ForeignKey("ChiNhanhId")]
        public virtual ChiNhanh ChiNhanh { get; set; }


        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        [DisplayName("Phòng Ban")]
        public string PhongBan { get; set; }
        
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        [DisplayName("Văn Phòng")]
        public string VanPhong { get; set; }

        //[DisplayName("Văn Phòng")]
        //public int VanPhongId { get; set; }

        //[ForeignKey("VanPhongId")]
        //public virtual VanPhong VanPhong { get; set; }

        [DisplayName("Đổi MK")]
        public bool DoiMK { get; set; }
        public DateTime? NgayDoiMK { get; set; }

        [DisplayName("Trạng Thái")]
        public bool TrangThai { get; set; }

        [DisplayName("Khối")]
        [MaxLength(10), Column(TypeName = "nvarchar(10)")]
        public string Khoi { get; set; }

        [DisplayName("Người Tạo")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string NguoiTao { get; set; }
        public DateTime? Ngaytao { get; set; }

        [DisplayName("Người Cập Nhật")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string NguoiCapNhat { get; set; }
        public DateTime? Ngaycapnhat { get; set; }

        [DisplayName("Role")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
