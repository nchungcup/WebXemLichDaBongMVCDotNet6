using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DACN_WebXemLichDaBong.Models;

namespace DACN_WebXemLichDaBong.Areas.Admin.Models
{
    public class LichThiDauViewModel
    {
        public int LichId { get; set; }
        public int HinhThucThiDauId { get; set; }
        public string HinhThucThiDau { get; set; } 
        public int DoiBenTraiId { get; set; }
        public string DoiBenTrai { get; set; }
        public int DoiBenPhaiId { get; set; }
        public string DoiBenPhai { get; set; }
        public DateTime ThoiGianThiDau { get; set; }
        public bool IsKetThuc { get; set; }
        public string IsKetThucText { get; set; }
        public string TySo { get; set; }
        public string SanThiDau { get; set; }
        public int GiaiDauId { get; set; }
        public string GiaiDau { get; set; }
        public IList<HinhThucThiDauModel> HinhThucThiDauAvailable { get; set; }
        public IList<GiaiDauModel> GiaiDauAvailable { get; set; }
    }
}