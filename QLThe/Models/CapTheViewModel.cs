using QLThe.Data.Models;
using QLThe.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace QLThe.Models
{
    public class CapTheViewModel
    {
        public IPagedList<CapTheDto> CapTheDtos { get; set; }
        public IEnumerable<ChiTietCapThe> ChiTietCapThes { get; set; }
        public IEnumerable<ThongTinThe> ThongTinThes { get; set; }
        public string StrUrl { get; set; }

        // for create
        public CapThe CapThe { get; set; }
        public ChiTietCapThe ChiTietCapThe { get; set; }
        public ThongTinThe ThongTinThe { get; set; }
        public IEnumerable<ChiNhanh> ChiNhanhs { get; set; }
        public IEnumerable<VanPhong> VanPhongs { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<HTTT> HTTTs { get; set; }
        public IEnumerable<LoaiThe> LoaiThes { get; set; }
        public IEnumerable<NoiNhanViewModel> NoiNhanViewModels { get; set; }
        
        [DisplayName("Nơi nhận")]
        public string NoiNhan { get; set; }
        [DisplayName("Loại thẻ")]
        public string LoaiThe { get; set; }
        public string CurrentYear { get; set; }

    }
}
