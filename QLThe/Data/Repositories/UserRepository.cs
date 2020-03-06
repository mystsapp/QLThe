using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QLThe.Data.Interfaces;
using QLThe.Data.Models;
using QLThe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLThe.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindIdIncludeCNAndRole(int? id);
        LoginViewModel Login(string username, string mact);
        int Changepass(string username, string newpass);
    }
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(QLTheDbContext context) : base(context)
        {
        }

        public async Task<User> FindIdIncludeCNAndRole(int? id)
        {
            return await _context.Users.Include(x => x.ChiNhanh).Include(x => x.Role).SingleAsync(x => x.Id == id);
        }

        
        public LoginViewModel Login(string username, string mact)
        {
            var parammeter = new SqlParameter[]
           {
                new SqlParameter("@username",username),
                new SqlParameter("@mact",mact)
           };

            var result = _context.LoginViewModels.FromSqlRaw("dbo.spLogin @username, @mact", parammeter).ToList();

            if (result == null)
            {
                return null;
            }
            else
            {
                return result.SingleOrDefault();
            }
        }

        public int Changepass(string username, string newpass)
        {
            try
            {
                var result = Find(x => x.Username == username).FirstOrDefault();

                result.Password = newpass;
                result.DoiMK = false;
                result.NgayDoiMK = DateTime.Now;
                _context.SaveChanges();
                return 1;
            }
            catch { throw; }
        }
    }
}
