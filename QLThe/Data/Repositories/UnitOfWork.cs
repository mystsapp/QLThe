using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICapTheRepository capTheRepository { get; }
        IChiNhanhRepository chiNhanhRepository { get; }
        IChiTietCapTheRepository chiTietCapTheRepository { get; }
        IHTTTRepository hTTTRepository { get; }
        ILoaiTheRepository loaiTheRepository { get; }
        IRoleRepository roleRepository { get; }
        ITrangThaiRepository trangThaiRepository { get; }
        IUserRepository userRepository { get; }
        IVanPhongRepository vanPhongRepository { get; }
        IThongTinTheRepository thongTinTheRepository { get; }
        Task<int> Complete();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QLTheDbContext _context;

        public UnitOfWork(QLTheDbContext context)
        {
            _context = context;

            capTheRepository = new CapTheRepository(_context);
            chiNhanhRepository = new ChiNhanhRepository(_context);
            chiTietCapTheRepository = new ChiTietCapTheRepository(_context);
            hTTTRepository = new HTTTRepository(_context);
            loaiTheRepository = new LoaiTheRepository(_context);
            roleRepository = new RoleRepository(_context);
            trangThaiRepository = new TrangThaiRepository(_context);
            userRepository = new UserRepository(_context);
            vanPhongRepository = new VanPhongRepository(_context);
            thongTinTheRepository = new ThongTinTheRepository(_context);
        }
        public ICapTheRepository capTheRepository { get; }

        public IChiNhanhRepository chiNhanhRepository { get; }

        public IChiTietCapTheRepository chiTietCapTheRepository { get; }

        public IHTTTRepository hTTTRepository { get; }

        public ILoaiTheRepository loaiTheRepository { get; }

        public IUserRepository userRepository { get; }

        public IVanPhongRepository vanPhongRepository { get; }

        public IThongTinTheRepository thongTinTheRepository { get; }

        public IRoleRepository roleRepository { get; }

        public ITrangThaiRepository trangThaiRepository { get; }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.DisposeAsync();
        }
    }
}
