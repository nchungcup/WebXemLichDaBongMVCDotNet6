using Microsoft.AspNetCore.Mvc;
using DACN_WebXemLichDaBong.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DACN_WebXemLichDaBong.Controllers
{
    public class SigninSignupController : Controller
    {
        private readonly ILogger<SigninSignupController> _logger;
        private readonly DataContext _context;
        public SigninSignupController(ILogger<SigninSignupController> logger, DataContext datacontext)
        {
            _logger = logger;
            _context = datacontext;
        }
        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signin(string TaiKhoan, string MatKhau)
        {
            if(TaiKhoan == null && MatKhau == null)
            {
                TempData["alertMessage"] = "Không được để trống!";
                return RedirectToAction("Signin", "SigninSignup");
            }    
            var data = await _context.Accounts.Where(a => a.TaiKhoan != null && a.TaiKhoan.Equals(TaiKhoan) && a.MatKhau != null && a.MatKhau.Equals(MatKhau)).ToListAsync();
            if (data.Count() > 0)
            {
                var usession = (from a in _context.Accounts
                                where a.TaiKhoan == TaiKhoan && a.MatKhau == MatKhau
                                select a.LoaiTaiKhoan).FirstOrDefault();
                if (usession == 1)
                {
                    HttpContext.Session.SetString("Admin", TaiKhoan);
                    HttpContext.Session.Remove("Client");
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    HttpContext.Session.SetString("Client", TaiKhoan);
                    HttpContext.Session.Remove("Admin");
                    data.FirstOrDefault().MucDoDangNhap += 1;
                    data.FirstOrDefault().LanDangNhapGanNhat = DateTime.Now;
                    
                    _context.Accounts.Update(data.FirstOrDefault());
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                var u = _context.Accounts.Where(a => a.TaiKhoan != null && a.TaiKhoan.Equals(TaiKhoan) && a.MatKhau != null && a.MatKhau != MatKhau).Count();
                if (u == 1)
                {
                    TempData["alertMessage"] = "Mật Khẩu Sai";
                    return RedirectToAction("Signin", "SigninSignup");
                }
                TempData["alertMessage"] = "Tài Khoản Không Tồn Tại!";
                return RedirectToAction("Signin", "SigninSignup");
            }
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup(Account _account)
        {
            var accDuplicate = _context.Accounts.Where(a => a.TaiKhoan.Equals(_account.TaiKhoan)).ToList();
            var emailDuplicate = _context.Accounts.Where(a => a.Email.Equals(_account.Email)).ToList();
            if (accDuplicate.Count() > 0 || emailDuplicate.Count() > 0)
            {
                TempData["alertMessage"] = "Tên Tài Khoản Hoặc Email Đã Có Trong Hệ Thống";
                return RedirectToAction("Signup", "SigninSignup");
            }
            else
            {
                var acc = new Account
                {
                    TaiKhoan = _account.TaiKhoan,
                    MatKhau = _account.MatKhau,
                    LoaiTaiKhoan = 2,
                    LanDangNhapGanNhat = DateTime.Now,
                    Email = _account.Email,
                    MucDoDangNhap = 0
                };

                _context.Accounts.Add(acc);
                await _context.SaveChangesAsync();
                return RedirectToAction("Signin", "SigninSignup");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Admin");
            HttpContext.Session.Remove("Client");
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}