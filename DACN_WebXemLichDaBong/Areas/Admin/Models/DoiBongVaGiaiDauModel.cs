using DACN_WebXemLichDaBong.Models;

namespace DACN_WebXemLichDaBong.Areas.Admin.Models
{
    public class DoiBongVaGiaiDauModel
    {
        public int GiaiDauId { get; set; }
        public string TenGiaiDau { get; set; }
        public string AnhGiaiDau { get; set; }
        public List<DoiBongModel> DoiBongs { get; set; }
    }
}
