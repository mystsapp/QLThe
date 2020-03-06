using QLThe.Data.Interfaces;
using QLThe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Data.Repositories
{
    public interface IChiTietCapTheRepository : IRepository<ChiTietCapThe>
    {

    }
    public class ChiTietCapTheRepository : Repository<ChiTietCapThe>, IChiTietCapTheRepository
    {
        public ChiTietCapTheRepository(QLTheDbContext context) : base(context)
        {
        }
    }
}
