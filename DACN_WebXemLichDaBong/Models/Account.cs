using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DACN_WebXemLichDaBong.Models
{
	[Table("Account")]
	public class Account
	{
		[Key]
		public int AccountId { get; set; }
		public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
		public int LoaiTaiKhoan { get; set; }
		public int MucDoDangNhap { get; set; }
		public DateTime LanDangNhapGanNhat { get; set; }
	}
}
