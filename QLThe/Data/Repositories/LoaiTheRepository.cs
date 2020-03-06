using QLThe.Data.Interfaces;
using QLThe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Data.Repositories
{
    public interface ILoaiTheRepository : IRepository<LoaiThe>
    {

    }
    public class LoaiTheRepository : Repository<LoaiThe>, ILoaiTheRepository
    {
        public LoaiTheRepository(QLTheDbContext context) : base(context)
        {
        }
    }
}
