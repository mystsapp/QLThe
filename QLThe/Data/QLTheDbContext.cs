using Microsoft.EntityFrameworkCore;
using QLThe.Data.Models;
using QLThe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Data
{
    public class QLTheDbContext : DbContext
    {
        public QLTheDbContext(DbContextOptions<QLTheDbContext> options) : base(options)
        {

        }

        public DbSet<CapThe> CapThes { get; set; }
        public DbSet<ChiNhanh> ChiNhanhs { get; set; }
        public DbSet<ChiTietCapThe> ChiTietCapThes { get; set; }
        public DbSet<HTTT> HTTTs { get; set; }
        public DbSet<LoaiThe> LoaiThes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ThongTinThe> ThongTinThes { get; set; }
        public DbSet<TrangThai> TrangThais { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VanPhong> VanPhongs { get; set; }
        public DbSet<LoginViewModel> LoginViewModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietCapThe>()
                .HasKey(c => new { c.STT, c.MaCapThe });

            //modelBuilder.Entity<ChiNhanh>()
            //    .HasKey(c => new { c.Id, c.MaCN });
        }
    }
}
