// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System;
using System.Threading.Tasks;
using Lottery.Models;
using Lottery.Services;
using Lottery.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Lottery.Areas.Identity.Pages.Account.Manage
{
    public class UserHistory : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserInfoService _userInfoService;
        private readonly IDrawService _drawService;
        private readonly ILogger<UserHistory> _logger;

        public UserHistory(
            UserManager<IdentityUser> userManager,
            ILogger<UserHistory> logger,
            IUserInfoService userInfoService,
            IDrawService drawService)
        {
            _userManager = userManager;
            _logger = logger;
            _userInfoService = userInfoService;
            _drawService = drawService;
        }

        [BindProperty]
        public List<HistoryViewModel> Draws { get; set; } = new List<HistoryViewModel>();

        public enum DrawStatuses
        {
            Waiting,
            Sold,
            Ordered,
            Delivering,
            Delivered
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (User.IsInRole("Admin"))
            {
                var list = await _drawService.GetAllDraws();
                Draws = list;
            }
            else
            {
                var list = await _drawService.GetUserDraws(user.Id);
                Draws = list;
            }

            return Page();
        }
    }
}
