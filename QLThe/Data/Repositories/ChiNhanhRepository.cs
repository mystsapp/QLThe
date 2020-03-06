using QLThe.Data.Interfaces;
using QLThe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Data.Repositories
{
    public interface IChiNhanhRepository: IRepository<ChiNhanh>
    {

    }
    public class ChiNhanhRepository : Repository<ChiNhanh>, IChiNhanhRepository
    {
        public ChiNhanhRepository(QLTheDbContext context) : base(context)
        {
        }
    }
}
