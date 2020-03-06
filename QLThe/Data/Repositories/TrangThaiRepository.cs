using QLThe.Data.Interfaces;
using QLThe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Data.Repositories
{
    public interface ITrangThaiRepository : IRepository<TrangThai>
    {

    }
    public class TrangThaiRepository : Repository<TrangThai>, ITrangThaiRepository
    {
        public TrangThaiRepository(QLTheDbContext context) : base(context)
        {
        }
    }
}
