using NumToWords;
using QLThe.Data.Interfaces;
using QLThe.Data.Models;
using QLThe.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace QLThe.Data.Repositories
{
    public interface ICapTheRepository : IRepository<CapThe>
    {
        IPagedList<CapTheDto> ListCapThe(string searchName, string searchDate, string hoTen, int? page);
    }
    public class CapTheRepository : Repository<CapThe>, ICapTheRepository
    {
        public CapTheRepository(QLTheDbContext context) : base(context)
        {
        }

        public IPagedList<CapTheDto> ListCapThe(string searchName, string searchDate, string hoTen, int? page)
        {

            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand

            var list = GetAllIncludeOne(x => x.VanPhong).AsQueryable();
            list = list.Where(x => x.NguoiCap == hoTen);
            if (!string.IsNullOrEmpty(searchName))
            {
                list = list.Where(x => x.MaCapThe.ToLower().Contains(searchName.ToLower()) || 
                                       x.NguoiCap.ToLower().Contains(searchName.ToLower()) ||
                                       x.NguoiNhan.ToLower().Contains(searchName.ToLower()));
            }

            var count = list.Count();

            var listCapThe = new List<CapTheDto>();
            foreach (var capthe in list)
            {
                var captheDto = new CapTheDto();

                captheDto.MaCapThe = capthe.MaCapThe;
                captheDto.NguoiCap = capthe.NguoiCap;
                captheDto.NgayCap = capthe.NgayCap;
                captheDto.ChiNhanh = capthe.MaCN;
                captheDto.VanPhong = capthe.VanPhong.Name;
                captheDto.NguoiNhan = capthe.NguoiNhan;
                captheDto.TongSoLuong = capthe.TongSoLuong;
                captheDto.Httt = capthe.Httt;
                captheDto.GhiChu = capthe.GhiChu;
                captheDto.HoaDon = capthe.HoaDon;
                captheDto.MayTinh = capthe.MayTinh;

                var chitietcapthe = _context.ChiTietCapThes.Where(x => x.MaCapThe == capthe.MaCapThe);
                if (capthe.TongSoLuong != 0)
                {
                    var mengia = capthe.TongSoLuong * chitietcapthe.SingleOrDefault().MenhGia;
                    captheDto.TienBangChu = SoSangChu.DoiSoSangChu(mengia.Value.ToString("N0")) + " đồng";

                }
                else
                    captheDto.TienBangChu = 0.ToString();

                listCapThe.Add(captheDto);
            }

            if (!string.IsNullOrEmpty(searchDate))
            {
                DateTime bgDate = Convert.ToDateTime(searchDate);
                listCapThe = listCapThe.Where(x => x.NgayCap.Value.ToShortDateString().Equals(bgDate.ToShortDateString())).ToList();
            }

            // page the list
            const int pageSize = 2;
            var listPaged = listCapThe.ToPagedList(page ?? 1, pageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;

            
            return listPaged;
        }
    }
}
