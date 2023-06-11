using Microsoft.AspNetCore.Mvc;
using DACN_WebXemLichDaBong.Models;
using DACN_WebXemLichDaBong.Areas.Admin.Models;

namespace DACN_WebXemLichDaBong.Areas.Admin.Components
{
    [ViewComponent(Name = "LichThiDauExcel")]
    public class LichThiDauExcelViewComponent : ViewComponent
    {
        private readonly DataContext _context;
        public LichThiDauExcelViewComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<LichThiDauModel> listLich)
        {
            var ltd = new List<LichThiDauViewModel>();
            if(listLich != null && listLich.Count() > 0)
            {     
                foreach (var l in listLich)
                {
                    var lich = new LichThiDauViewModel();
                    lich.HinhThucThiDauAvailable = _context.HinhThucThiDauModels.Where(c => !l.HinhThucThiDauId.Equals(c.HinhThucThiDauId)).ToList();
                    lich.GiaiDauAvailable = _context.GiaiDauModels.Where(c => !l.GiaiDauId.Equals(c.GiaiDauId)).ToList();
                    lich.SanThiDau = l.SanThiDau;
                    lich.ThoiGianThiDau = l.ThoiGianThiDau;
                    lich.HinhThucThiDau = _context.HinhThucThiDauModels.Where(h => h.HinhThucThiDauId == l.HinhThucThiDauId).Select(h => h.TenHinhThucThiDau).FirstOrDefault();
                    lich.DoiBenTrai = _context.DoiBongModels.Where(d => d.DoiBongId == l.DoiBenTraiId).Select(d => d.TenDoiBong).FirstOrDefault();
                    lich.DoiBenPhai = _context.DoiBongModels.Where(d => d.DoiBongId == l.DoiBenPhaiId).Select(d => d.TenDoiBong).FirstOrDefault();
                    lich.GiaiDau = _context.GiaiDauModels.Where(g => g.GiaiDauId == l.GiaiDauId).Select(g => g.TenGiaiDau).FirstOrDefault();
                    lich.DoiBenTraiId = l.DoiBenTraiId;
                    lich.DoiBenPhaiId = l.DoiBenPhaiId;
                    lich.GiaiDauId = l.GiaiDauId;
                    lich.HinhThucThiDauId = l.HinhThucThiDauId;
                    ltd.Add(lich);
                }
            }
            return await Task.FromResult((IViewComponentResult)View("Default", ltd));
        }
    }
}
