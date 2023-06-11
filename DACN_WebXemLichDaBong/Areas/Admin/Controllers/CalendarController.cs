using DACN_WebXemLichDaBong.Areas.Admin.Models;
using DACN_WebXemLichDaBong.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using OfficeOpenXml;
using System.Globalization;
using System;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

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
            foreach (var kt in ketThuc)
            {
                kt.IsKetThuc = true;
                _dataContext.LichThiDauModels.Update(kt);
                await _dataContext.SaveChangesAsync();
            }
            var chuaCapNhat = _dataContext.LichThiDauModels.Where(q => q.ThoiGianThiDau.Date == DateTime.Now.Date).ToList();
            var chuaCapNhatList = new List<LichThiDauViewModel>();
            foreach (var ltd in chuaCapNhat)
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

        public JsonResult SelectedDoiBong(int giaiDauId, int doiBongBenTraiId, int doiBongBenPhaiId)
        {
            var doiBong = _dataContext.DoiBongModels.Where(d => d.GiaiDauId == giaiDauId && d.DoiBongId != doiBongBenTraiId && d.DoiBongId != doiBongBenPhaiId);
            return Json(doiBong);
        }


        public JsonResult GetAvailableHinhThuc(int hinhThucId)
        {
            var hinhThuc = _dataContext.HinhThucThiDauModels.Where(d => !hinhThucId.Equals(d.HinhThucThiDauId));
            return Json(hinhThuc);
        }

        public JsonResult GetAvailableGiaiDau(int giaiDauId)
        {
            var giaiDau = _dataContext.GiaiDauModels.Where(d => !giaiDauId.Equals(d.GiaiDauId));
            return Json(giaiDau);
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

        public IActionResult ExcelCreate()
        {
            return View("ImportExcel");
        }

        [HttpPost]
        public IActionResult ImportExcel(IFormFile file)
        {
            var listLich = new List<LichThiDauModel>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(file.OpenReadStream()))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;
                var columnCount = worksheet.Dimension.Columns;
                // Lặp qua từng hàng trong tệp Excel và thực hiện xử lý dữ liệu
                for (int row = 1; row <= rowCount; row++) // Bắt đầu từ hàng thứ 2 để bỏ qua tiêu đề
                {
                    var hinhThucThiDauId = 0;
                    var doiBenTraiId = 0;
                    var doiBenPhaiId = 0;
                    var thoiGianThiDau = new DateTime();
                    var sanThiDau = "";
                    var giaiDauId = 0;
                    if (worksheet.Cells[row, 1].Value != null)
                    {
                        var hinhThucThiDau = _dataContext.HinhThucThiDauModels.Where(c => worksheet.Cells[row, 1].Value.ToString().Contains(c.TenHinhThucThiDau)).FirstOrDefault();
                        if (hinhThucThiDau != null)
                        {
                            hinhThucThiDauId = hinhThucThiDau.HinhThucThiDauId;
                        }
                    }
                    if (worksheet.Cells[row, 2].Value != null)
                    {
                        var doiBenTrai = _dataContext.DoiBongModels.Where(c => worksheet.Cells[row, 2].Value.ToString().Contains(c.TenDoiBong)).FirstOrDefault();
                        if (doiBenTrai != null)
                        {
                            doiBenTraiId = doiBenTrai.DoiBongId;
                        }
                    }
                    if (worksheet.Cells[row, 3].Value != null)
                    {
                        var doiBenPhai = _dataContext.DoiBongModels.Where(c => worksheet.Cells[row, 3].Value.ToString().Contains(c.TenDoiBong)).FirstOrDefault();
                        if (doiBenPhai != null)
                        {
                            doiBenPhaiId = doiBenPhai.DoiBongId;
                        }
                    }
                    if (worksheet.Cells[row, 4].Value != null)
                    {
                        var excelDateTime = (DateTime)worksheet.Cells[row, 4].Value;
                        var month = excelDateTime.Month < 10 ? "/0" + excelDateTime.Month : "/" + excelDateTime.Month;
                        var day = excelDateTime.Day < 10 ? "/0" + excelDateTime.Day : "/" + excelDateTime.Day;
                        var hour = excelDateTime.Hour < 10 ? "0" + excelDateTime.Hour : "" + excelDateTime.Hour;
                        var minute = excelDateTime.Minute < 10 ? ":0" + excelDateTime.Minute : ":" + excelDateTime.Minute;
                        var second = excelDateTime.Second < 10 ? ":0" + excelDateTime.Second : ":" + excelDateTime.Second;
                        var dateTimeString = excelDateTime.Year + month + day + " " + hour + minute + second;
                        thoiGianThiDau = DateTime.ParseExact(dateTimeString, "yyyy/MM/dd H:mm:ss", CultureInfo.InvariantCulture);
                    }
                    if (worksheet.Cells[row, 5].Value != null)
                    {
                        sanThiDau = worksheet.Cells[row, 5].Value.ToString();
                    }
                    if (worksheet.Cells[row, 6].Value != null)
                    {
                        var giaiDau = _dataContext.GiaiDauModels.Where(c => worksheet.Cells[row, 6].Value.ToString().Contains(c.TenGiaiDau)).FirstOrDefault();
                        if (giaiDau != null)
                        {
                            giaiDauId = giaiDau.GiaiDauId;
                        }
                    }
                    if (hinhThucThiDauId != 0 && doiBenTraiId != 0 && doiBenPhaiId != 0 && thoiGianThiDau != DateTime.MinValue && sanThiDau != "" && giaiDauId != 0)
                    {
                        listLich.Add(new LichThiDauModel
                        {
                            HinhThucThiDauId = hinhThucThiDauId,
                            DoiBenTraiId = doiBenTraiId,
                            DoiBenPhaiId = doiBenPhaiId,
                            ThoiGianThiDau = thoiGianThiDau,
                            IsKetThuc = thoiGianThiDau >= DateTime.Now.AddMinutes(120) ? true : false,
                            SanThiDau = sanThiDau,
                            GiaiDauId = giaiDauId
                        });
                    }
                }
            }
            return ViewComponent("LichThiDauExcel", listLich);
        }

        [HttpPost]
        public async Task<IActionResult> ExcelCreatePost(JsonArray jsonList)
        {
            foreach (var jsonElement in jsonList)
            {
                await _dataContext.LichThiDauModels.AddAsync(JsonSerializer.Deserialize<LichThiDauModel>(jsonElement));
                await _dataContext.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Calendar");
        }
    }
}
