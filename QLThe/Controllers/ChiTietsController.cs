using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QLThe.Data.Models;
using QLThe.Data.Repositories;
using QLThe.Models;
using QLThe.Utilities;

namespace QLThe.Controllers
{
    public class ChiTietsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public ChiTietViewModel ChiTietVM { get; set; }

        public ChiTietsController(IUnitOfWork unitOfWork,
                                  IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            ChiTietVM = new ChiTietViewModel()
            {
                StrUrl = ""
            };
        }
        public IActionResult Create(string maCT, string strUrl)
        {
            ViewBag.maCT = maCT;
            ChiTietVM.StrUrl = strUrl;
            ChiTietVM.HTTTs = _unitOfWork.hTTTRepository.GetAll();
            ChiTietVM.NoiNhanViewModels = NoiNhanViewModels().OrderBy(x => x.CodeTKH);
            ChiTietVM.LoaiThes = _unitOfWork.loaiTheRepository.GetAll();
            ChiTietVM.CurrentYear = DateTime.Now.Year.ToString().Substring(2, 2);
            return View(ChiTietVM);
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreatePost(string maCT, string strUrl)
        {
            var user = HttpContext.Session.Gets<User>("loginUser").FirstOrDefault();
            ChiTietVM.ChiTietCapThe.MaCapThe = maCT;
            //ChiTietVM.ThongTinThe.MaCapThe = maCT;

            ////////////////////// thong tin the ////////////////////////////
            var seriTu = long.Parse(ChiTietVM.ChiTietCapThe.SoSeriTu);
            var seriDen = long.Parse(ChiTietVM.ChiTietCapThe.SoSeriDen);
            var listChiTietCapThe = _unitOfWork.chiTietCapTheRepository.GetAllIncludeOne(x => x.CapThe);
            listChiTietCapThe = listChiTietCapThe.Where(x => x.CapThe.NguoiCap == user.HoTen);
            List<ThongTinThe> listTTThe = new List<ThongTinThe>();
            for (long i = seriTu; i <= seriDen; i++)
            {
                foreach (var ctct in listChiTietCapThe)
                {
                    if (long.Parse(ctct.SoSeriTu) <= i && i <= long.Parse(ctct.SoSeriDen))
                    {
                        SetAlert("Nhiều thẻ đã được cấp trong hệ thống vui lòng kiểm tra lại!", "error");
                        return Redirect(strUrl);

                    }
                }

                var noinhan = _unitOfWork.capTheRepository.FindIncludeOne(x => x.VanPhong, y => y.MaCapThe.Equals(maCT)).FirstOrDefault().VanPhong.Name;
                //var nn = noinhan.VanPhong.Name;
                var nguoinhan = _unitOfWork.capTheRepository.GetByStringId(maCT).NguoiNhan;
                listTTThe.Add(new ThongTinThe()
                {
                    SoSeri = i.ToString(),
                    MaCapThe = maCT,
                    Gia = ChiTietVM.ChiTietCapThe.MenhGia,
                    NguoiPhatHanh = user.HoTen,
                    NgayPhatHanh = DateTime.Now,
                    NoiNhan = noinhan, // _unitOfWork.vanPhongRepository.GetById(CapTheVM.CapThe.VanPhongId).Name,
                    NguoiNhan = nguoinhan,
                    Trangthai = "Nội bộ",
                    GhiChu = ChiTietVM.ThongTinThe.GhiChu

                });
            }

            _unitOfWork.chiTietCapTheRepository.Create(ChiTietVM.ChiTietCapThe);
            await _unitOfWork.thongTinTheRepository.CreateRangeAsync(listTTThe);
            await _unitOfWork.Complete();

            // hinh anh
            // Image being saved
            string webRootPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            //var listTTTheFromDb = _unitOfWork.thongTinTheRepository.Find(x => x.MaCapThe.Equals(maCT));
            if (files.Count != 0)
            {
                // Image has been uploaded
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(files[0].FileName);


                for (long i = seriTu; i <= seriDen; i++)
                {
                    // hinh 1
                    using (var fileStream = new FileStream(Path.
                                                                Combine(uploads,
                                                                i + "h_1" + extension),
                                                                FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    var the = _unitOfWork.thongTinTheRepository.GetByStringId(i.ToString());
                    the.Hinh1 = @"\" + SD.ImageFolder + @"\" + i + "h_1" + extension;

                    // hinh 2
                    using (var fileStream = new FileStream(Path.
                                                                           Combine(uploads,
                                                                           i + "h_2" + extension),
                                                                           FileMode.Create))
                    {
                        files[1].CopyTo(fileStream);
                    }

                    the.Hinh2 = @"\" + SD.ImageFolder + @"\" + i + "h_2" + extension;

                }

            }


            await _unitOfWork.Complete();
            SetAlert("Thêm mới thành công.", "success");
            return RedirectToAction(nameof(DetailsRedirect), new { strUrl = strUrl });
        }

        public async Task<IActionResult> Details(int ChiTietId, string strUrl)
        {
            ChiTietVM.ChiTietCapThe = _unitOfWork.chiTietCapTheRepository
                                                 .FindIncludeOne(x => x.CapThe, y => y.STT.Equals(ChiTietId))
                                                 .FirstOrDefault();
            //ChiTietVM.ChiTietCapThe = _unitOfWork.chiTietCapTheRepository.Find(x => x.STT.Equals(ChiTietId)).FirstOrDefault();
            ChiTietVM.ThongTinThe = _unitOfWork.thongTinTheRepository
                                               .FindIncludeOne(x => x.CapThe, y => y.SoSeri.Equals(ChiTietVM.ChiTietCapThe.SoSeriTu))
                                               .SingleOrDefault();
            ChiTietVM.StrUrl = strUrl;

            return View(ChiTietVM);
        }

        public IActionResult Delete(int ChiTietId, string strUrl)
        {
            ChiTietVM.ChiTietCapThe = _unitOfWork.chiTietCapTheRepository
                                                 .FindIncludeOne(x => x.CapThe, y => y.STT.Equals(ChiTietId))
                                                 .FirstOrDefault();
            //ChiTietVM.ChiTietCapThe = _unitOfWork.chiTietCapTheRepository.Find(x => x.STT.Equals(ChiTietId)).FirstOrDefault();
            ChiTietVM.ThongTinThe = _unitOfWork.thongTinTheRepository
                                               .FindIncludeOne(x => x.CapThe, y => y.SoSeri.Equals(ChiTietVM.ChiTietCapThe.SoSeriTu))
                                               .SingleOrDefault();
            ChiTietVM.StrUrl = strUrl;

            return View(ChiTietVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int ChiTietId, string strUrl)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            var chiTietById = _unitOfWork.chiTietCapTheRepository.Find(x => x.STT.Equals(ChiTietId)).SingleOrDefault();
            var seriTu = long.Parse(chiTietById.SoSeriTu);
            var seriDen = long.Parse(chiTietById.SoSeriDen);

            List<ThongTinThe> thongTinThes = new List<ThongTinThe>();
            for (long i = seriTu; i <= seriDen; i++)
            {
                thongTinThes.Add(_unitOfWork.thongTinTheRepository.GetByStringId(i.ToString()));
            }

            if (thongTinThes == null)
            {
                ViewBag.ErrorMessage = "Số seri từ  " + seriTu + " đến " + seriDen + " không tồn tại.";
                return NotFound();
            }

            else
            {
                string upload = Path.Combine(webRootPath, SD.ImageFolder);
                foreach (var thongtinthe in thongTinThes)
                {
                    if (!string.IsNullOrEmpty(thongtinthe.Hinh1))
                    {
                        var extension = Path.GetExtension(thongtinthe.Hinh1);
                        if (System.IO.File.Exists(Path.Combine(upload, thongtinthe.SoSeri + "h_1" + extension)))
                        {
                            System.IO.File.Delete(Path.Combine(upload, thongtinthe.SoSeri + "h_1" + extension));
                        }
                    }

                    if (!string.IsNullOrEmpty(thongtinthe.Hinh2))
                    {
                        var extension = Path.GetExtension(thongtinthe.Hinh2);
                        if (System.IO.File.Exists(Path.Combine(upload, thongtinthe.SoSeri + "h_2" + extension)))
                        {
                            System.IO.File.Delete(Path.Combine(upload, thongtinthe.SoSeri + "h_2" + extension));
                        }
                    }

                }
                // xoa thongtinthe
                _unitOfWork.thongTinTheRepository.DeleteRange(thongTinThes);

                // xoa chitiet
                _unitOfWork.chiTietCapTheRepository.Delete(chiTietById);

                await _unitOfWork.Complete();
                SetAlert("Xóa thành công!", "success");

                return Redirect(strUrl);

            }


        }

        public IActionResult DetailsRedirect(string strUrl)
        {
            return Redirect(strUrl);
        }

        public List<NoiNhanViewModel> NoiNhanViewModels()
        {
            var chiNhanhs = _unitOfWork.chiNhanhRepository.GetAll();
            var noiNhans = new List<NoiNhanViewModel>();
            foreach (var chinhanh in chiNhanhs)
            {
                noiNhans.Add(new NoiNhanViewModel()
                {
                    Name = chinhanh.CodeTKH + " - " + chinhanh.Name,
                    CodeTKH = chinhanh.CodeTKH
                });
            }

            return noiNhans;
        }
    }
}