using DACN_WebXemLichDaBong.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DACN_WebXemLichDaBong.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly DataContext _dataContext;
        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IActionResult Index()
        {
            var acc = _dataContext.Accounts.Where(a => a.LoaiTaiKhoan == 2).ToList();
            return View(acc);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var acc = _dataContext.Accounts.Where(a => a.AccountId == Id && a.LoaiTaiKhoan == 2).FirstOrDefault();
            _dataContext.Accounts.Remove(acc);
            await _dataContext.SaveChangesAsync();
            return Index();
        }
    }
}
