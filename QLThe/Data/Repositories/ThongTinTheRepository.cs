using QLThe.Data.Interfaces;
using QLThe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Data.Repositories
{
    public interface IThongTinTheRepository : IRepository<ThongTinThe>
    {

    }
    public class ThongTinTheRepository : Repository<ThongTinThe>, IThongTinTheRepository
    {
        public ThongTinTheRepository(QLTheDbContext context) : base(context)
        {
        }
    }
}
