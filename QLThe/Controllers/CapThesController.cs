using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
    public class CapThesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public CapTheViewModel CapTheVM { get; set; }
        public CapThesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public async Task<IActionResult> Create(string strUrl, string maCN = null)
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
                CapTheVM.VanPhongs = CapTheVM.VanPhongs.Where(x => x.ChiNhanhId == CapTheVM.ChiNhanhs.FirstOrDefault().Id);
            }

            CapTheVM.Users = await _unitOfWork.userRepository.GetAllIncludeAsync(x => x.ChiNhanh, y => y.Role);
            CapTheVM.Users = CapTheVM.Users.Where(x => x.VanPhong == CapTheVM.VanPhongs.FirstOrDefault().Name);
            CapTheVM.HTTTs = _unitOfWork.hTTTRepository.GetAll();
            return View(CapTheVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(string strUrl)
        {
            var user = HttpContext.Session.Gets<User>("loginUser").FirstOrDefault();
            CapTheVM.CapThe.NguoiCap = user.HoTen;
            CapTheVM.CapThe.NgayCap = DateTime.Now;
            // MaCT
            var lastMaCT = _unitOfWork.capTheRepository.FindIncludeOne(x => x.VanPhong, y => y.NguoiCap.Equals(user.HoTen))
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

            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            // Get the IP  
            var info = Dns.GetHostByName(hostName).AddressList;//.ToList();
            //string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            string myIP = Dns.GetHostByName(hostName).AddressList[3].ToString();

            CapTheVM.CapThe.MayTinh = computerName + "|" + userName + "|" + myIP;
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