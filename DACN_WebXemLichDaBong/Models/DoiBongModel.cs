using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACN_WebXemLichDaBong.Models
{
	[Table("DoiBong")]
	public class DoiBongModel
	{
		[Key]
		public int DoiBongId { get; set; }
		public int QuocGiaId { get; set; }
		public int GiaiDauId { get; set; }
		public string TenDoiBong { get; set; }
		public string AnhDoiBong { get; set; }
		public string TenHuanLuyenVien { get; set; }
		public string ThongTinThem { get; set; }
	}
}