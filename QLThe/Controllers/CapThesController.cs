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
            CapTheVM.StrUrl = UriHelper.GetDisplayUrl(Request);
            ViewData["searchName"] = searchName;
            ViewData["searchDate"] = searchDate;
            CapTheVM.CapTheDtos = _unitOfWork.capTheRepository.ListCapThe(searchName, searchDate, user.HoTen, page);

            ViewBag.CaptheDtos = CapTheVM.CapTheDtos.Count();
            ViewBag.maCT = maCT;
            if (!string.IsNullOrEmpty(maCT))
            {
                CapTheVM.ChiTietCapThes = _unitOfWork.chiTietCapTheRepository.Find(x => x.MaCapThe == maCT);
                CapTheVM.ThongTinThes = _unitOfWork.thongTinTheRepository.Find(x => x.MaCapThe == maCT);

                var capthe = _unitOfWork.capTheRepository.GetByStringId(maCT);
                ViewBag.GhiChuCapTheByMaCT = capthe.GhiChu;
            }
            ViewBag.CapThe = CapTheVM.CapTheDtos;
            return View(CapTheVM);
        }

        public async Task<IActionResult> Create(string strUrl, string maCN = null, 
                                                               string seriDen = null)
        {
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
            CapTheVM.NoiNhanViewModels = noiNhans.OrderBy(x => x.CodeTKH);
            CapTheVM.LoaiThes = _unitOfWork.loaiTheRepository.GetAll();

            // so seri
            CapTheVM.CurrentYear = DateTime.Now.Year.ToString().Substring(2,2);
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
            
            if(lastMaCTPrefix == nowPrefixMaCT)
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
            ////////////////////// chi tiet ///////////////////////////////////
            // seriTu, seriDen --> ok (jquery)
            // gia --> tu go~
            // hinhthuc --> ko tinh
            // hinh1, hinh2 --> nợ
            // ghichu --> tu go~
            // menh gia -->  ok (jquery)
            // soluong --> view
            ////////////////////// thong tin the ////////////////////////////
            List<ThongTinThe> listTTThe = new List<ThongTinThe>();
            for(int i = int.Parse(CapTheVM.ChiTietCapThe.SoSeriTu); i <= int.Parse(CapTheVM.ChiTietCapThe.SoSeriDen); i++)
            {
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

                // hinh 1
                using (var fileStream = new FileStream(Path.Combine(uploads, CapTheVM.ThongTinThe.SoSeri + "h_1" + extension), 
                                                                    FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                
                foreach(var the in listTTTheFromDb)
                {
                    the.Hinh1 = @"\" + SD.ImageFolder + @"\" + CapTheVM.ThongTinThe.SoSeri + "h_1" + extension;
                }

                // hinh 2
                using (var fileStream = new FileStream(Path.Combine(uploads, CapTheVM.ThongTinThe.SoSeri + "h_2" + extension), 
                                                                    FileMode.Create))
                {
                    files[1].CopyTo(fileStream);
                }
                foreach (var the in listTTTheFromDb)
                {
                    the.Hinh2 = @"\" + SD.ImageFolder + @"\" + CapTheVM.ThongTinThe.SoSeri + "h_2" + extension;
                }

            }
            //else
            //{
            //    foreach (var the in listTTTheFromDb)
            //    {
            //        the.Hinh1 = "";
            //        the.Hinh2 = "";
            //    }
            //}

            await _unitOfWork.Complete();
            return View();
        }
        public IActionResult Edit()
        {
            return View(_unitOfWork.userRepository.GetAll());
        }
        
        [HttpPost]
        public IActionResult Edit(string list)
        {
            var idList = JsonConvert.DeserializeObject<List<User>>(list); 
            return View();
        }
    }
}