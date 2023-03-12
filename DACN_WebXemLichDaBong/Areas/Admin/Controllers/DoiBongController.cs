using DACN_WebXemLichDaBong.Areas.Admin.Models;
using DACN_WebXemLichDaBong.Models;
using Microsoft.AspNetCore.Mvc;

namespace DACN_WebXemLichDaBong.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoiBongController : Controller
    {
        private readonly DataContext _dataContext;
        public DoiBongController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IActionResult Index(int Id)
        {
            var doiBong = _dataContext.DoiBongModels.Where(d => d.GiaiDauId == Id).ToList();
            var tenGiaiDau = _dataContext.GiaiDauModels.Where(g => g.GiaiDauId == Id).Select(q => q.TenGiaiDau).FirstOrDefault();
            var quocGias = _dataContext.QuocGias.ToList();
            var doiBongView = new List<DoiBongViewModel>();
            foreach (var db in doiBong)
            {
                var dbv = new DoiBongViewModel();
                dbv.DoiBongId = db.DoiBongId;
                dbv.AnhDoiBong = db.AnhDoiBong;
                dbv.QuocGiaId = db.QuocGiaId;
                dbv.TenQuocGia = _dataContext.QuocGias.Where(q => q.QuocGiaId == dbv.QuocGiaId).Select(q => q.TenQuocGia).FirstOrDefault();
                dbv.GiaiDauId = db.GiaiDauId;
                dbv.TenGiaiDau = tenGiaiDau;
                dbv.TenDoiBong = db.TenDoiBong;
                dbv.TenHuanLuyenVien = db.TenHuanLuyenVien;
                dbv.ThongTinThem = db.ThongTinThem;
                dbv.QuocGias = quocGias;
                doiBongView.Add(dbv);
            }
            return View(doiBongView);
        }

        [HttpGet]
        public IActionResult Create(int Id)
        {
            var tenGiaiDau = _dataContext.GiaiDauModels.Where(g => g.GiaiDauId == Id).Select(q => q.TenGiaiDau).FirstOrDefault();
            var quocGias = _dataContext.QuocGias.ToList();
            var dbv = new DoiBongViewModel();
            dbv.GiaiDauId = Id;
            dbv.TenGiaiDau = tenGiaiDau;
            dbv.QuocGias = quocGias;
            return View(dbv);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoiBongModel doiBong)
        {
            await _dataContext.DoiBongModels.AddAsync(doiBong);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index", "DoiBong", new { Id = doiBong.GiaiDauId });
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var db = _dataContext.DoiBongModels.Where(g => g.DoiBongId == Id).FirstOrDefault();
            var tenGiaiDau = _dataContext.GiaiDauModels.Where(g => g.GiaiDauId == db.GiaiDauId).Select(q => q.TenGiaiDau).FirstOrDefault();
            var quocGias = _dataContext.QuocGias.ToList();
            var dbv = new DoiBongViewModel();
            dbv.DoiBongId = db.DoiBongId;
            dbv.AnhDoiBong = db.AnhDoiBong;
            dbv.QuocGiaId = db.QuocGiaId;
            dbv.TenQuocGia = _dataContext.QuocGias.Where(q => q.QuocGiaId == dbv.QuocGiaId).Select(q => q.TenQuocGia).FirstOrDefault();
            dbv.GiaiDauId = db.GiaiDauId;
            dbv.TenGiaiDau = tenGiaiDau;
            dbv.TenDoiBong = db.TenDoiBong;
            dbv.TenHuanLuyenVien = db.TenHuanLuyenVien;
            dbv.ThongTinThem = db.ThongTinThem;
            dbv.QuocGias = quocGias;
            return View(dbv);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DoiBongModel doiBong)
        {
            _dataContext.DoiBongModels.Update(doiBong);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index", "DoiBong", new { Id = doiBong.GiaiDauId });
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var db = _dataContext.DoiBongModels.Where(d => d.DoiBongId == Id).FirstOrDefault();
            _dataContext.DoiBongModels.Remove(db);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index", "DoiBong", new { Id = db.GiaiDauId });
        }
    }
}
