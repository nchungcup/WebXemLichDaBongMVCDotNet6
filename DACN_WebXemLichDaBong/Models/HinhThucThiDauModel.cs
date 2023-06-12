using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACN_WebXemLichDaBong.Models
{
	[Table("HinhThucThiDau")]
	public class HinhThucThiDauModel
	{
		[Key]
		public int HinhThucThiDauId { get; set; }
		public string TenHinhThucThiDau { get; set; }
	}
}
