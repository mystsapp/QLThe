using QLThe.Data.Interfaces;
using QLThe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Data.Repositories
{
    public interface IVanPhongRepository : IRepository<VanPhong>
    {

    }
    public class VanPhongRepository : Repository<VanPhong>, IVanPhongRepository
    {
        public VanPhongRepository(QLTheDbContext context) : base(context)
        {
        }
    }
}
