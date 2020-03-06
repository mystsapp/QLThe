using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QLThe.Data.Models;
using QLThe.Data.Repositories;
using QLThe.Models;
using QLThe.Utilities;

namespace QLThe.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public UserViewModel UserVM { get; set; }
        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            UserVM = new UserViewModel()
            {
                Users = _unitOfWork.userRepository.GetAll(),
                ChiNhanhs = _unitOfWork.chiNhanhRepository.GetAll(),
                VanPhongs = _unitOfWork.vanPhongRepository.GetAll(),
                KhoiViewModels = khoiViewModels(),
                Roles = _unitOfWork.roleRepository.GetAll(),
                User = new User(),
                OldPass = ""
            };
        }
        public async Task<IActionResult> Index()
        {
            var user = HttpContext.Session.Gets<User>("loginUser").FirstOrDefault();
            if (user.Role.Name != "Admins" )
            {
                return View("AccessDenied");
            }
            var a = UserVM.Users.Count();
            return View(UserVM);
        }

        // Get Create method
        public IActionResult Create()
        {

            var user = HttpContext.Session.Gets<User>("loginUser").FirstOrDefault();
            if (user.Role.Name != "Admins")
            {
                return View("AccessDenied");
            }
            return View(UserVM);
        }

        // Post: Create Method
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            var user = HttpContext.Session.Gets<User>("loginUser").FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return View(UserVM);

            }

            UserVM.User.Ngaytao = DateTime.Now;
            UserVM.User.NguoiTao = user.NguoiTao;
            UserVM.User.MaCN = _unitOfWork.chiNhanhRepository.GetById(UserVM.User.ChiNhanhId).MaCN;
            UserVM.User.Password = MaHoaSHA1.EncodeSHA1(UserVM.User.Password);

            _unitOfWork.userRepository.Create(UserVM.User);
            await _unitOfWork.Complete();
            SetAlert("Thêm User thành cong.", "success");
            return RedirectToAction(nameof(Index));
        }

        // Get: Edit method
        public async Task<IActionResult> Edit(int? id)
        {
            var user = HttpContext.Session.Gets<User>("loginUser").FirstOrDefault();
            if (user.Role.Name != "Admins" )
            {
                return View("AccessDenied");
            }

            UserVM.User = await _unitOfWork.userRepository.GetByIdAsync(id);
            if (UserVM.User != null)
            {
                return View(UserVM);
            }
            else
            {
                ViewBag.ErrorMessage = "User is not found.";
                return View(nameof(NotFound));
            }
        }

        // Post: Eidt method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            if (UserVM.User.Id != id)
            {
                ViewBag.ErrorMessage = "User is not found.";
                return View(nameof(NotFound));
            }
            if (!ModelState.IsValid)
            {
               
                return View(UserVM);
            }

            if (UserVM.PassToEdit != null) //password field is required
            {
                UserVM.User.Password = MaHoaSHA1.EncodeSHA1(UserVM.PassToEdit);
                UserVM.User.NgayDoiMK = DateTime.Now;
            }
            else
            {
                UserVM.User.Password = UserVM.OldPass;
            }
            UserVM.User.Ngaycapnhat = DateTime.Now;
            UserVM.User.NguoiCapNhat = user.Username;
            _unitOfWork.userRepository.Update(UserVM.User);
            await _unitOfWork.Complete();
            SetAlert("Cập nhật User thành cong.", "success");
            return RedirectToAction(nameof(Index));
        }

        // Get: Details method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            UserVM.User = await _unitOfWork.userRepository.FindIdIncludeCNAndRole(id);

            if (UserVM.User == null)
                return NotFound();

            return View(UserVM);
        }

        // Get: Delete method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            UserVM.User = await _unitOfWork.userRepository.FindIdIncludeCNAndRole(id);

            if (UserVM.User == null)
                return NotFound();

            return View(UserVM);
        }

        // Post: Delete method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            User user = await _unitOfWork.userRepository.GetByIdAsync(id);

            if (user == null)
                return NotFound();
            else
            {
                _unitOfWork.userRepository.Delete(user);
                await _unitOfWork.Complete();
                SetAlert("Xóa User thành công.", "success");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public JsonResult GetNhanVienByVanPhong(int vanPhongId)
        {
            if(vanPhongId == 0)
            {
                return Json(new
                {
                    data = false
                });
            }
            var vp = _unitOfWork.vanPhongRepository.GetById(vanPhongId).Name;
            var dMDaiLies = _unitOfWork.userRepository.Find(x => x.VanPhong.Equals(vp));
            return Json(new
            {
                data = JsonConvert.SerializeObject(dMDaiLies)
            });
        }
        
        [HttpGet]
        public JsonResult GetVanPhongByChiNhanh(int chinhanh)
        {
            var dMDaiLies = _unitOfWork.vanPhongRepository.Find(x => x.ChiNhanhId == chinhanh);
            return Json(new
            {
                data = JsonConvert.SerializeObject(dMDaiLies)
            });
        }
        
        [HttpGet]
        public JsonResult GetVanPhongByMaCN(string chinhanh)
        {
            var dMDaiLies = _unitOfWork.vanPhongRepository.GetAllIncludeOne(x => x.ChiNhanh).Where(x => x.ChiNhanh.MaCN == chinhanh);
            return Json(new
            {
                data = JsonConvert.SerializeObject(dMDaiLies)
            });
        }

        [AcceptVerbs("get", "post")]
        public IActionResult UsersExists(string Username)
        {
            bool result = false;
            if (string.IsNullOrEmpty(Username))
            {
                return Json(true);
            }
            var user = _unitOfWork.userRepository.Find(x => x.Username.ToLower() == Username.ToLower());
            if (user.Count() == 0)
                result = true;
            return Json(result);
        }

        [AcceptVerbs("get", "post")]
        public IActionResult UsersEditExists(string UsernameEdit, string Username)
        {
            bool result = false;
            var user = _unitOfWork.userRepository.Find(x => x.Username.ToLower() == UsernameEdit.ToLower());
            var count = user.Count();
            if (count == 0 || (UsernameEdit == Username))
                result = true;
            return Json(result);
        }

        public List<KhoiViewModel> khoiViewModels()
        {
            return new List<KhoiViewModel>()
            {
                new KhoiViewModel() { Id = 1, Name = "OB" },
                new KhoiViewModel() { Id = 2, Name = "ND" }
            };
        }
    }
}