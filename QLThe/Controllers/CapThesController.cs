using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using QLThe.Data.Models;
using QLThe.Data.Repositories;
using QLThe.Dtos;
using QLThe.Models;
using QLThe.Utilities;
using Rotativa.AspNetCore;
using X.PagedList;

namespace QLThe.Controllers
{
    public class CapThesController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public CapTheViewModel CapTheVM { get; set; }
        public CapThesController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            CapTheVM = new CapTheViewModel()
            {
                //CapThePartialViewModel = new CapThePartialViewModel(),
                StrUrl = ""
            };
        }
        public IActionResult Index(string maCT = null, string searchName = null, string searchDate = null, int page = 1)
        {
            var user = HttpContext.Session.Gets<User>("loginUser").FirstOrDefault();
            //////////////////// for delete
            if (maCT != null)
            {
                var ct = _unitOfWork.capTheRepository.GetByStringId(maCT);
                if (ct == null)
                {
                    var lastMaCT = _unitOfWork.capTheRepository
                                          .FindIncludeOne(x => x.VanPhong, y => y.NguoiCap.Equals(user.HoTen))
                                          .OrderByDescending(x => x.MaCapThe).FirstOrDefault().MaCapThe;
                    maCT = lastMaCT;
                }
            }

            ////////////////////

            CapTheVM.StrUrl = UriHelper.GetDisplayUrl(Request);
            ViewData["searchName"] = searchName;
            ViewData["searchDate"] = searchDate;
            CapTheVM.CapTheDtos = _unitOfWork.capTheRepository.ListCapThe(searchName, searchDate, user.HoTen, page);

            ViewBag.CaptheDtos = CapTheVM.CapTheDtos == null ? 0 : CapTheVM.CapTheDtos.Count();
            ViewBag.maCT = maCT;
            if (!string.IsNullOrEmpty(maCT))
            {
                CapTheVM.ChiTietCapThes = _unitOfWork.chiTietCapTheRepository.Find(x => x.MaCapThe == maCT);
                CapTheVM.ThongTinThes = _unitOfWork.thongTinTheRepository.Find(x => x.MaCapThe == maCT);

                var capthe = _unitOfWork.capTheRepository.GetByStringId(maCT);
                if (!string.IsNullOrEmpty(capthe.GhiChu))
                {
                    ViewBag.GhiChuCapTheByMaCT = capthe.GhiChu;
                }
                else
                {
                    ViewBag.GhiChuCapTheByMaCT = "";
                }
            }
            ViewBag.CapThe = CapTheVM.CapTheDtos;
            return View(CapTheVM);
        }

        public async Task<IActionResult> Create(string strUrl, string maCN = null,
                                                               string seriDen = null)
        {
            CapTheVM.StrUrl = strUrl;
            CapTheVM.ChiNhanhs = _unitOfWork.chiNhanhRepository.GetAll();

            CapTheVM.VanPhongs = await _unitOfWork.vanPhongRepository.GetAllIncludeOneAsync(x => x.ChiNhanh);
            if (maCN != null)
            {
                ViewBag.MaCN = maCN;
                CapTheVM.VanPhongs = CapTheVM.VanPhongs.Where(x => x.ChiNhanh.MaCN == maCN);
            }
            else
            {
                CapTheVM.VanPhongs = CapTheVM.VanPhongs.Where(x => x.ChiNhanhId == CapTheVM
                                                       .ChiNhanhs
                                                       .FirstOrDefault().Id);
            }

            CapTheVM.Users = await _unitOfWork.userRepository.GetAllIncludeAsync(x => x.ChiNhanh, y => y.Role);
            CapTheVM.Users = CapTheVM.Users.Where(x => x.VanPhong == CapTheVM.VanPhongs.FirstOrDefault().Name);
            CapTheVM.HTTTs = _unitOfWork.hTTTRepository.GetAll();

            // chi tiet cap the
            CapTheVM.NoiNhanViewModels = NoiNhanViewModels().OrderBy(x => x.CodeTKH);
            CapTheVM.LoaiThes = _unitOfWork.loaiTheRepository.GetAll();

            // so seri
            CapTheVM.CurrentYear = DateTime.Now.Year.ToString().Substring(2, 2);
            return View(CapTheVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(string strUrl)
        {
            var user = HttpContext.Session.Gets<User>("loginUser").FirstOrDefault();
            CapTheVM.CapThe.NguoiCap = user.HoTen;
            CapTheVM.CapThe.NgayCap = DateTime.Now;

            // MaCT
            var lastMaCT = _unitOfWork.capTheRepository
                                      .FindIncludeOne(x => x.VanPhong, y => y.NguoiCap.Equals(user.HoTen))
                                      .OrderByDescending(x => x.MaCapThe).FirstOrDefault().MaCapThe;
            var lastMaCTPrefix = lastMaCT.Split("/")[0];

            var maVP = _unitOfWork.vanPhongRepository.Find(x => x.Name == user.VanPhong).SingleOrDefault().MaVP;
            var namTao = DateTime.Now.Year.ToString().Substring(2);
            var nowPrefixMaCT = maVP + namTao;

            if (lastMaCTPrefix == nowPrefixMaCT)
            {
                var prefixMaCT = lastMaCTPrefix + "/";
                CapTheVM.CapThe.MaCapThe = GetNextId.NextID(lastMaCT, prefixMaCT);
            }
            else
            {
                var prefixMaCT = nowPrefixMaCT + "/";
                CapTheVM.CapThe.MaCapThe = GetNextId.NextID("", prefixMaCT);
            }
            // May tinh
            var computerName = Environment.MachineName;
            var userName = Environment.UserName;
            var osVersion = Environment.OSVersion;
            var domainName = Environment.UserDomainName;

            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            // Get the IP  
            //var info = Dns.GetHostByName(hostName).AddressList;//.ToList();
            var info = Dns.GetHostEntry(hostName).AddressList;
            //string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            string myIP = Dns.GetHostEntry(hostName).AddressList[3].ToString();

            CapTheVM.CapThe.MayTinh = computerName + "|" + userName + "|" + myIP + "|" + domainName;

            // xuat hoadon
            CapTheVM.CapThe.HoaDon = false;

            // tongsoluong
            CapTheVM.CapThe.TongSoLuong = CapTheVM.ChiTietCapThe.SoLuong;
            ////////////////////// chi tiet ///////////////////////////////////
            // seriTu, seriDen --> ok (jquery)
            // gia --> tu go~
            // hinhthuc --> noibo
            // hinh1, hinh2 --> below
            // ghichu --> tu go~
            // menh gia -->  ok (jquery)
            // soluong --> view

            CapTheVM.ChiTietCapThe.MaCapThe = CapTheVM.CapThe.MaCapThe;
            ////////////////////// thong tin the ////////////////////////////
            var seriTu = long.Parse(CapTheVM.ChiTietCapThe.SoSeriTu);
            var seriDen = long.Parse(CapTheVM.ChiTietCapThe.SoSeriDen);
            var listChiTietCapThe = _unitOfWork.chiTietCapTheRepository.GetAll().Where(x => x.CapThe.NguoiCap == user.HoTen);
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

                listTTThe.Add(new ThongTinThe()
                {
                    SoSeri = i.ToString(),
                    MaCapThe = CapTheVM.CapThe.MaCapThe,
                    Gia = CapTheVM.ChiTietCapThe.MenhGia,
                    NguoiPhatHanh = user.HoTen,
                    NgayPhatHanh = DateTime.Now,
                    NoiNhan = _unitOfWork.vanPhongRepository.GetById(CapTheVM.CapThe.VanPhongId).Name,
                    NguoiNhan = CapTheVM.CapThe.NguoiNhan,
                    Trangthai = "Nội bộ",
                    GhiChu = CapTheVM.ThongTinThe.GhiChu

                });
            }
            _unitOfWork.capTheRepository.Create(CapTheVM.CapThe);
            _unitOfWork.chiTietCapTheRepository.Create(CapTheVM.ChiTietCapThe);
            await _unitOfWork.thongTinTheRepository.CreateRangeAsync(listTTThe);
            await _unitOfWork.Complete();

            // hinh anh
            // Image being saved
            string webRootPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var listTTTheFromDb = _unitOfWork.thongTinTheRepository.Find(x => x.MaCapThe == CapTheVM.CapThe.MaCapThe);
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

                //// hinh 1
                //using (var fileStream = new FileStream(Path.
                //                                       Combine(uploads, 
                //                                       listTTTheFromDb.FirstOrDefault().SoSeri + "h_1" + extension),
                //                                       FileMode.Create))
                //{
                //    files[0].CopyTo(fileStream);
                //}

                //foreach (var the in listTTTheFromDb)
                //{
                //    the.Hinh1 = @"\" + SD.ImageFolder + @"\" + listTTTheFromDb.FirstOrDefault().SoSeri + "h_1" + extension;
                //}

                //// hinh 2
                //using (var fileStream = new FileStream(Path.
                //                                       Combine(uploads, 
                //                                       listTTTheFromDb.FirstOrDefault().SoSeri + "h_2" + extension),
                //                                       FileMode.Create))
                //{
                //    files[1].CopyTo(fileStream);
                //}
                //foreach (var the in listTTTheFromDb)
                //{
                //    the.Hinh2 = @"\" + SD.ImageFolder + @"\" + listTTTheFromDb.FirstOrDefault().SoSeri + "h_2" + extension;
                //}

            }


            await _unitOfWork.Complete();
            SetAlert("Thêm mới thành công.", "success");
            return Redirect(strUrl);
        }
        //public IActionResult Edit()
        //{
        //    return View(_unitOfWork.userRepository.GetAll());
        //}

        //[HttpPost]
        //public IActionResult Edit(string list)
        //{
        //    var idList = JsonConvert.DeserializeObject<List<User>>(list);
        //    return View();
        //}

        public async Task<IActionResult> Details(string maCT, string strUrl)
        {
            CapTheVM.CapThe = _unitOfWork.capTheRepository.FindIncludeOne(x => x.VanPhong, y => y.MaCapThe.Equals(maCT))
                                                          .SingleOrDefault();

            CapTheVM.StrUrl = strUrl;

            return View(CapTheVM);
        }

        public async Task<IActionResult> Delete(string maCT, string strUrl)
        {
            CapTheVM.CapThe = _unitOfWork.capTheRepository.FindIncludeOne(x => x.VanPhong, y => y.MaCapThe.Equals(maCT))
                                                          .SingleOrDefault();

            CapTheVM.StrUrl = strUrl;

            return View(CapTheVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(string maCT, string strUrl)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            IEnumerable<ThongTinThe> thongTinThes = await _unitOfWork.thongTinTheRepository.FindAsync(x => x.MaCapThe.Equals(maCT));

            if (thongTinThes == null)
            {
                ViewBag.ErrorMessage = "Mã cấp thẻ này " + maCT + " Không tồn tại";
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

                // xoa capthe
                CapTheVM.CapThe = _unitOfWork.capTheRepository.GetByStringId(maCT);
                _unitOfWork.capTheRepository.Delete(CapTheVM.CapThe);
                

                await _unitOfWork.Complete();
                SetAlert("Xóa thành công!", "success");

                return Redirect(strUrl);

            }

            
        }

        public async Task<IActionResult> PdfReport(string maCT)
        {
            CapTheVM.CapThe = _unitOfWork.capTheRepository.FindIncludeOne(x => x.VanPhong, y => y.MaCapThe.Equals(maCT)).SingleOrDefault();
            CapTheVM.ChiTietCapThes = _unitOfWork.chiTietCapTheRepository.Find(y => y.MaCapThe.Equals(maCT));
            CapTheVM.TongTien = (CapTheVM.ChiTietCapThes.FirstOrDefault().SoLuong * CapTheVM.ChiTietCapThes.FirstOrDefault().MenhGia).Value.ToString("N0");
            ///// Currency to money
            string s = NumToWords.SoSangChu.DoiSoSangChu(CapTheVM.TongTien);
            //string c = AmountToWords.changeCurrencyToWords(hoaDon.ThanhTienVAT.ToString().ToLower());
            //string t = String.IsNullOrEmpty(loaitien) ? "" : " Exchange rate USD/VND";
            CapTheVM.TienBangChu = char.ToUpper(s[0]) + s.Substring(1) + " đồng " + "vnd";
            return new ViewAsPdf(CapTheVM);
            //return View(CapTheVM);
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