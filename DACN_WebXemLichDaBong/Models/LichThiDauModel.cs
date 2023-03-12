using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACN_WebXemLichDaBong.Models
{
	[Table("LichThiDau")]
	public class LichThiDauModel
	{
		[Key]
		public int LichId { get; set; }
		public int HinhThucThiDauId { get; set; }
		public int DoiBenTraiId { get; set; }
		public int DoiBenPhaiId { get; set; }
		public DateTime ThoiGianThiDau { get; set; }
		public bool IsKetThuc { get; set; }
		public string TySo { get; set; }
		public string SanThiDau { get; set; }
		public int GiaiDauId { get; set; }
	}
}
