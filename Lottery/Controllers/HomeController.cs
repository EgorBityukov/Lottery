using Lottery.Models;
using Lottery.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lottery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserInfoService _userInfoService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IDrawService _drawService;

        public HomeController(ILogger<HomeController> logger, 
            IUserInfoService userInfoService, 
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager,
            IDrawService drawService)
        {
            _logger = logger;
            _userInfoService = userInfoService;
            _userManager = userManager;
            _signInManager = signInManager;
            _drawService = drawService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Delivery()
        {
            ViewBag.CurrentTab = "Delivery";
            return View();
        }

        public IActionResult Help()
        {
            ViewBag.CurrentTab = "Help";
            return View();
        }

        public async Task<IActionResult> History()
        {
            var list = await _drawService.GetLastDraws();
            ViewBag.CurrentTab = "History";
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<string> GetUserPhoto()
        {    
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var image = await _userInfoService.GetImageStringByIdAsync(user.Id);
                return image;
            }
            return null;
        }
    
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}