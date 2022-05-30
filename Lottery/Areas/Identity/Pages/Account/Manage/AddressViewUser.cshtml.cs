// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Lottery.Models;
using Lottery.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lottery.Areas.Identity.Pages.Account.Manage
{
    public class AddressViewUser : PageModel
    {
        private readonly Lottery.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserInfoService _userInfoService;

        public AddressViewUser(Lottery.Data.ApplicationDbContext context, IUserInfoService userInfoService,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _userInfoService = userInfoService;
        }

        [BindProperty]
        public Lottery.Models.Address UserAddress { get; set; } = default!;


        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userInfo = await _userInfoService.GetUserInfoByIdAsync(user.Id);
            Address userAddress;

            if (id.HasValue)
            {
                userAddress = await _context.Addresses.Where(a => a.AddressId == id ).FirstOrDefaultAsync();
            }
            else
            {
                userAddress = await _context.Addresses.Where(a => a.AddressId == userInfo.AddressId).FirstOrDefaultAsync();
            }
            
            if (userAddress == null)
            {
                return NotFound();
            }
            UserAddress = userAddress;
            return Page();
        }

        

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.AddressId == id);
        }
    }
}
