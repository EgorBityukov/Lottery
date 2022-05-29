// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Lottery.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lottery.Areas.Identity.Pages.Account.Manage
{
    public class AddressView : PageModel
    {
        private readonly Lottery.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AddressView> _logger;
        private readonly IUserInfoService _userInfoService;

        public AddressView(Lottery.Data.ApplicationDbContext context, IUserInfoService userInfoService,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AddressView> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _userInfoService = userInfoService;
        }

        [BindProperty]
        public Lottery.Models.Address Address { get; set; } = default!;


        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var userInfo = await _userInfoService.GetUserInfoByIdAsync(user.Id);

            var address = await _context.Addresses.Where(a => a.AddressId == userInfo.AddressId).FirstOrDefaultAsync();
            if (address == null)
            {
                return NotFound();
            }
            Address = address;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(object o)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(Address.AddressId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage();
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.AddressId == id);
        }
    }
}
