using DACN_WebXemLichDaBong.Areas.Admin.Models;
using DACN_WebXemLichDaBong.Models;
using Microsoft.AspNetCore.Mvc;

namespace DACN_WebXemLichDaBong.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GiaiDauController : Controller
    {
        private readonly DataContext _dataContext;
        public GiaiDauController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            var giaiDau = _dataContext.GiaiDauModels.ToList();
            var gds = new List<DoiBongVaGiaiDauModel>();
            foreach(var g in giaiDau)
            {
                var gd = new DoiBongVaGiaiDauModel();
                gd.GiaiDauId = g.GiaiDauId;
                gd.TenGiaiDau = g.TenGiaiDau;
                gd.AnhGiaiDau = g.AnhGiaiDau;
                gd.DoiBongs = _dataContext.DoiBongModels.Where(d => d.GiaiDauId == gd.GiaiDauId).ToList();
                gds.Add(gd);
            }    
            return View(gds);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GiaiDauModel giaiDau)
        {
            await _dataContext.GiaiDauModels.AddAsync(giaiDau);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index", "GiaiDau");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var gd = _dataContext.GiaiDauModels.Where(g => g.GiaiDauId == Id).FirstOrDefault();
            return View(gd);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GiaiDauModel giaiDau)
        {
            _dataContext.GiaiDauModels.Update(giaiDau);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index", "GiaiDau");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var db = _dataContext.DoiBongModels.Where(d => d.GiaiDauId == Id).ToList();
            _dataContext.DoiBongModels.RemoveRange(db);
            var gd = _dataContext.GiaiDauModels.Where(g => g.GiaiDauId == Id).FirstOrDefault();
            _dataContext.GiaiDauModels.Remove(gd);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index", "GiaiDau");
        }

    }
}
