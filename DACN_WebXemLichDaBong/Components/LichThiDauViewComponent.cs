using Microsoft.AspNetCore.Mvc;
using DACN_WebXemLichDaBong.Models;
using DACN_WebXemLichDaBong.Areas.Admin.Models;

namespace DACN_WebXemLichDaBong.Components
{
	[ViewComponent(Name = "LichThiDau")]
	public class LichThiDauViewComponent : ViewComponent
	{
		private readonly DataContext _context;
		public LichThiDauViewComponent(DataContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync(int giaiDauId)
		{
			var lichThiDau = _context.LichThiDauModels.Where(ltd => ltd.ThoiGianThiDau.Date.Equals(DateTime.Now.Date)).ToList();
            if(giaiDauId != 0)
			{
				lichThiDau = lichThiDau.Where(ltd => ltd.GiaiDauId == giaiDauId).ToList();
			}
			var ltd = new List<LichThiDauViewModel>();
            foreach (var l in lichThiDau)
			{
				var lich = new LichThiDauViewModel();
				lich.SanThiDau = l.SanThiDau;
				lich.ThoiGianThiDau = l.ThoiGianThiDau;
				lich.HinhThucThiDau = _context.HinhThucThiDauModels.Where(h => h.HinhThucThiDauId == l.HinhThucThiDauId).Select(h => h.TenHinhThucThiDau).FirstOrDefault();
				lich.DoiBenTrai = _context.DoiBongModels.Where(d => d.DoiBongId == l.DoiBenTraiId).Select(d => d.AnhDoiBong).FirstOrDefault();
                lich.DoiBenPhai = _context.DoiBongModels.Where(d => d.DoiBongId == l.DoiBenPhaiId).Select(d => d.AnhDoiBong).FirstOrDefault();
				lich.GiaiDau = _context.GiaiDauModels.Where(g => g.GiaiDauId == l.GiaiDauId).Select(g => g.TenGiaiDau).FirstOrDefault();
				lich.TySo = l.TySo;
				if(lich.ThoiGianThiDau.AddMinutes(130) <= DateTime.Now)
				{
					lich.IsKetThuc = true; 
                }
				else
				{
					lich.IsKetThuc = false;
				}
                lich.IsKetThucText = lich.IsKetThuc == false ? "Chưa" : "Rồi";
                ltd.Add(lich);
			}
            return await Task.FromResult((IViewComponentResult)View("Default", ltd));
		}
	}
}
