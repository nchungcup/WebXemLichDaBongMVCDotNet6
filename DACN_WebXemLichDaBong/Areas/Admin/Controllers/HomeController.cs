using DACN_WebXemLichDaBong.Models;
using Microsoft.AspNetCore.Mvc;

namespace DACN_WebXemLichDaBong.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly DataContext _dataContext;
        public HomeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [Area("Admin")]
        [Route(nameof(Admin) + "/Home")]
        public IActionResult Index()
        {
            var acc = _dataContext.Accounts.Where(a => a.LoaiTaiKhoan == 2).OrderByDescending(a => a.LanDangNhapGanNhat).Take(10).ToList();
            return View(acc);
        }
    }
}
