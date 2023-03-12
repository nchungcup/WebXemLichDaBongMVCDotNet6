using DACN_WebXemLichDaBong.Models;

namespace DACN_WebXemLichDaBong.Areas.Admin.Models
{
    public class DoiBongViewModel
    {
        public int DoiBongId { get; set; }
        public int QuocGiaId { get; set; }
        public string TenQuocGia { get; set; }
        public string TenDoiBong { get; set; }
        public string AnhDoiBong { get; set; }
        public string TenHuanLuyenVien { get; set; }
        public string ThongTinThem { get; set; }
        public int GiaiDauId { get; set; }
        public string TenGiaiDau { get; set; }
        public List<QuocGiaModel> QuocGias { get; set; }
    }
}
