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

                var userInfo = await _userInfoService.GetUserInfoByIdAsync(user.Id);
                var balance = userInfo.Balance;
                HttpContext.Response.Cookies.Append("Balance", balance.ToString());

                if (image != null)
                {
                    HttpContext.Session.SetString("UserImage", image);
                }

                return image;
            }
            return null;
        }

        public async Task<string> GetUserBalace()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var userInfo = await _userInfoService.GetUserInfoByIdAsync(user.Id);
                return userInfo.Balance.ToString();
            }
            return null;
        }

        public IActionResult BalanceView()
        {
            return PartialView("Pay");
        }

        public IActionResult BalanceViewWithdraw()
        {
            return PartialView("Take");
        }

        [HttpPost]
        public async Task AddBalance(decimal? balance)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                await _userInfoService.AddBalanceAsync(user.Id, balance.Value);
                var curBalance = await GetUserBalace();
                HttpContext.Response.Cookies.Append("Balance", curBalance);
            }
        }

        [HttpPost]
        public async Task TakeBalance(decimal? balance)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                await _userInfoService.TakeBalanceAsync(user.Id, balance.Value);
                var curBalance = await GetUserBalace();
                HttpContext.Response.Cookies.Append("Balance", curBalance);
            }
        }

        [HttpPost]
        public async Task SellLot(int drawId)
        {
            await _drawService.SellLot(drawId);
            var curBalance = await GetUserBalace();
            HttpContext.Response.Cookies.Append("Balance", curBalance);
        }

        [HttpPost]
        public async Task OrderLot(int drawId)
        {
            await _drawService.OrderLot(drawId);
        }

        [HttpPost]
        public async Task OrderChangeStatus(int drawId, string status)
        {
            await _drawService.ChangeStatusLot(drawId, status);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}