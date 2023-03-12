using Microsoft.EntityFrameworkCore;

namespace DACN_WebXemLichDaBong.Models
{
	public class DataContext :DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) 
		{ 
		}

		public DbSet<Account> Accounts { get; set; }
		public DbSet<LichThiDauModel> LichThiDauModels { get; set; }
		public DbSet<DoiBongModel> DoiBongModels { get; set; }
		public DbSet<GiaiDauModel> GiaiDauModels { get; set; }
		public DbSet<HinhThucThiDauModel> HinhThucThiDauModels { get; set; }
        public DbSet<QuocGiaModel> QuocGias { get; set; }
    }
}
