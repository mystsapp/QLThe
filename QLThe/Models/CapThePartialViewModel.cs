using QLThe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Models
{
    public class CapThePartialViewModel
    {
        public IEnumerable<ChiTietCapThe> ChiTietCapThes { get; set; }
        public IEnumerable<ThongTinThe> ThongTinThes { get; set; }
        
    }
}
