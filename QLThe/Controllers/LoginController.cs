using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLThe.Data.Models;
using QLThe.Data.Repositories;
using QLThe.Models;
using QLThe.Utilities;

namespace QLThe.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        //[ActionName("login")]
        //[HttpGet]
        //public IActionResult Login()
        //{
        //    // ViewBag.request = UriHelper.GetDisplayUrl(Request);
        //    // listDaily("");
        //    return View();
        //}
        [HttpPost, ActionName("Index")]
        public async Task<IActionResult> IndexAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _unitOfWork.userRepository.Login(model.Username, "013");
                if (result == null)
                {
                    ModelState.AddModelError("", "Tài khoản này không tồn tại");
                }
                else
                {
                    if (!result.Trangthai)
                    {
                        ModelState.AddModelError("", "Tài khoản này đã bị khóa");
                        return View();
                    }
                    string modelPass = MaHoaSHA1.EncodeSHA1(model.Password);
                    if (result.Password != modelPass)
                    {
                        ModelState.AddModelError("", "Mật khẩu không đúng");
                    }
                    if (result.Password == modelPass)
                    {
                        var user = await _unitOfWork.userRepository.FindIncludeOneAsync(x => x.Role, x => x.Username == model.Username);
                        HttpContext.Session.Set("loginUser", user);


                        //HttpContext.Session.SetString("username", user.Username);
                        //HttpContext.Session.SetString("password", model.Password);
                        //HttpContext.Session.SetString("hoten", result.Hoten);
                        //HttpContext.Session.SetString("phong", result.Maphong);
                        //HttpContext.Session.SetString("chinhanh", result.Macn);
                        //HttpContext.Session.SetString("dienthoai", String.IsNullOrEmpty(result.Dienthoai) ? "" : result.Dienthoai);
                        //HttpContext.Session.SetString("macode", result.Macode);
                        //HttpContext.Session.SetString("roleId", result.Macode);

                        //DateTime ngaydoimk = Convert.ToDateTime(result.Ngaydoimk);
                        //int kq = (DateTime.Now.Month - ngaydoimk.Month) + 12 * (DateTime.Now.Year - ngaydoimk.Year);
                        //if (kq >= 3)
                        //{
                        //    return View("changepass");
                        //}
                        //else if (result.Doimk)
                        //{
                        //    return View("changepass");
                        //}

                        if (result.Doimk)
                        {
                            return View("changepass");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }
                }
            }
            return View();
        }
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "Home");
        }

        [HttpGet]
        public ActionResult changepass(string strUrl)
        {
            var entity = new ChangePassModel();
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();
            entity.Username = user.Username;
            entity.Password = user.Password;
            entity.strUrl = strUrl;
            return View("changepass", entity);
        }
        [HttpPost]
        public ActionResult changepass(ChangePassModel model)
        {
            if (ModelState.IsValid)
            {
                var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();
                string oldPass = user.Password;
                string modelPass = MaHoaSHA1.EncodeSHA1(model.Password);
                if (oldPass != modelPass)
                {
                    ModelState.AddModelError("", "Mật khẩu cũ không đúng");
                }
                //else if (model.Newpassword != model.Confirmpassword)
                //{
                //    ModelState.AddModelError("", "Mật khẩu nhập lại không đúng.");
                //}
                else
                {
                    int result = _unitOfWork.userRepository.Changepass(model.Username, MaHoaSHA1.EncodeSHA1(model.NewPassword));
                    if (result > 0)
                    {
                        SetAlert("Đổi mật khẩu thành công.", "success");
                        return LocalRedirect(model.strUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Không thể đổi mật khẩu.");
                    }
                }

            }
            return View();
        }

        private void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "waring")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}