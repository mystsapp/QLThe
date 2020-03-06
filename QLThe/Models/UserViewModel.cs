using Microsoft.AspNetCore.Mvc;
using QLThe.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Models
{
    public class UserViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public User User { get; set; }
        public IEnumerable<ChiNhanh> ChiNhanhs { get; set; }
        public IEnumerable<VanPhong> VanPhongs { get; set; }
        public IEnumerable<KhoiViewModel> KhoiViewModels { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public string OldPass { get; set; }
        [DataType(DataType.Password)]
        public string PassToEdit { get; set; }
    }
}
