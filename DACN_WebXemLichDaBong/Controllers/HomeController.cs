using DACN_WebXemLichDaBong.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DACN_WebXemLichDaBong.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly DataContext _context;
		public HomeController(ILogger<HomeController> logger, DataContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			var giaiDau = _context.GiaiDauModels.ToList();
			return View(giaiDau);
		}

        public IActionResult GiaiDau(int giaiDauId)
        {
            return ViewComponent("LichThiDau", giaiDauId);
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}