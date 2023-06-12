using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACN_WebXemLichDaBong.Models
{
	[Table("GiaiDau")]
	public class GiaiDauModel
	{
		[Key]
		public int GiaiDauId { get; set; }
		public string TenGiaiDau { get; set; }
		public string AnhGiaiDau { get; set; }
	}
}
