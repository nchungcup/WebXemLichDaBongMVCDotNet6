using DACN_WebXemLichDaBong.Areas.Admin.Models;
using DACN_WebXemLichDaBong.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DACN_WebXemLichDaBong.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CalendarController : Controller
    {
        private readonly DataContext _dataContext;
        public CalendarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IActionResult> Index()
        {
            var ketThuc = _dataContext.LichThiDauModels.Where(l => l.ThoiGianThiDau.AddMinutes(130) <= DateTime.Now).ToList();
            foreach(var kt in ketThuc)
            {
                kt.IsKetThuc = true;
                _dataContext.LichThiDauModels.Update(kt);
                await _dataContext.SaveChangesAsync();
            }
            var chuaCapNhat = _dataContext.LichThiDauModels.Where(q => q.ThoiGianThiDau.Date == DateTime.Now.Date).ToList();
            var chuaCapNhatList = new List<LichThiDauViewModel>();
            foreach(var ltd in chuaCapNhat)
            {     
                var ltdView = new LichThiDauViewModel();
                ltdView.LichId = ltd.LichId;
                ltdView.HinhThucThiDauId = ltd.HinhThucThiDauId;
                ltdView.HinhThucThiDau = _dataContext.HinhThucThiDauModels.Where(h => h.HinhThucThiDauId == ltdView.HinhThucThiDauId).Select(h => h.TenHinhThucThiDau).FirstOrDefault();
                ltdView.DoiBenTraiId = ltd.DoiBenTraiId;
                ltdView.DoiBenTrai = _dataContext.DoiBongModels.Where(d => d.DoiBongId == ltdView.DoiBenTraiId).Select(d => d.TenDoiBong).FirstOrDefault();
                ltdView.DoiBenPhaiId = ltd.DoiBenPhaiId;
                ltdView.DoiBenPhai = _dataContext.DoiBongModels.Where(d => d.DoiBongId == ltdView.DoiBenPhaiId).Select(d => d.TenDoiBong).FirstOrDefault();
                ltdView.ThoiGianThiDau = ltd.ThoiGianThiDau;
                ltdView.IsKetThuc = ltd.IsKetThuc;
                ltdView.IsKetThucText = ltd.IsKetThuc == false ? "Chưa" : "Rồi";
                ltd.TySo = ltd.TySo;
                ltdView.SanThiDau = ltd.SanThiDau;
                ltdView.GiaiDauId = ltd.GiaiDauId;
                ltdView.GiaiDau = _dataContext.GiaiDauModels.Where(g => g.GiaiDauId == ltd.GiaiDauId).Select(g => g.TenGiaiDau).FirstOrDefault();
                chuaCapNhatList.Add(ltdView);
            }
            return View(chuaCapNhatList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new LichThiDauViewModel();
            viewModel.HinhThucThiDauAvailable = _dataContext.HinhThucThiDauModels.ToList();
            viewModel.GiaiDauAvailable = _dataContext.GiaiDauModels.ToList();
            return View("Create", viewModel);
        }

        public JsonResult GetDoiBong(int giaiDauId)
        {
            var doiBong = _dataContext.DoiBongModels.Where(d => d.GiaiDauId == giaiDauId);
            return Json(doiBong);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LichThiDauModel lichThiDau)
        {
            await _dataContext.LichThiDauModels.AddAsync(lichThiDau);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index", "Calendar");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var ltd = _dataContext.LichThiDauModels.Where(l => l.LichId == Id).FirstOrDefault();
            var ltdView = new LichThiDauViewModel();
            ltdView.LichId = ltd.LichId;
            ltdView.HinhThucThiDauId = ltd.HinhThucThiDauId;
            ltdView.HinhThucThiDau = _dataContext.HinhThucThiDauModels.Where(h => h.HinhThucThiDauId == ltdView.HinhThucThiDauId).Select(h => h.TenHinhThucThiDau).FirstOrDefault();
            ltdView.HinhThucThiDauAvailable = _dataContext.HinhThucThiDauModels.ToList();
            ltdView.DoiBenTraiId = ltd.DoiBenTraiId;
            ltdView.DoiBenTrai = _dataContext.DoiBongModels.Where(d => d.DoiBongId == ltdView.DoiBenTraiId).Select(d => d.TenDoiBong).FirstOrDefault();
            ltdView.DoiBenPhaiId = ltd.DoiBenPhaiId;
            ltdView.DoiBenPhai = _dataContext.DoiBongModels.Where(d => d.DoiBongId == ltdView.DoiBenPhaiId).Select(d => d.TenDoiBong).FirstOrDefault();
            ltdView.ThoiGianThiDau = ltd.ThoiGianThiDau;
            ltdView.IsKetThuc = ltd.IsKetThuc;
            ltd.TySo = ltd.TySo;
            ltdView.SanThiDau = ltd.SanThiDau;
            ltdView.GiaiDauId = ltd.GiaiDauId;
            ltdView.GiaiDauAvailable = _dataContext.GiaiDauModels.ToList();
            ltdView.GiaiDau = _dataContext.GiaiDauModels.Where(g => g.GiaiDauId == ltd.GiaiDauId).Select(g => g.TenGiaiDau).FirstOrDefault();
            return View("Edit", ltdView);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LichThiDauModel lichThiDau)
        {
            _dataContext.LichThiDauModels.Update(lichThiDau);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index", "Calendar");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var ltd = _dataContext.LichThiDauModels.Where(l => l.LichId == Id).FirstOrDefault();
            _dataContext.Remove(ltd);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index", "Calendar");
        }
    }
}
