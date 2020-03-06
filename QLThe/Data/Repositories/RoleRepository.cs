using QLThe.Data.Interfaces;
using QLThe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Data.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {

    }
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(QLTheDbContext context) : base(context)
        {
        }
    }
}
