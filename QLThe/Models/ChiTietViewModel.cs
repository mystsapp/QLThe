using QLThe.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Models
{
    public class ChiTietViewModel
    {
        public IEnumerable<NoiNhanViewModel> NoiNhanViewModels { get; set; }
        public IEnumerable<LoaiThe> LoaiThes { get; set; }
        public ChiTietCapThe ChiTietCapThe { get; set; }
        public IEnumerable<HTTT> HTTTs { get; set; }
        public ThongTinThe ThongTinThe { get; set; }

        [DisplayName("Nơi nhận")]
        public string NoiNhan { get; set; }
        [DisplayName("Loại thẻ")]
        public string LoaiThe { get; set; }
        public string CurrentYear { get; set; }
        public string StrUrl { get; set; }
    }
}
