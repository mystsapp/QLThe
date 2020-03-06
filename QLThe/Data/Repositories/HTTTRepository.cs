using QLThe.Data.Interfaces;
using QLThe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Data.Repositories
{
    public interface IHTTTRepository : IRepository<HTTT>
    {

    }
    public class HTTTRepository : Repository<HTTT>, IHTTTRepository
    {
        public HTTTRepository(QLTheDbContext context) : base(context)
        {
        }
    }
}
