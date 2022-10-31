using IdentityApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace IdentityApi.Controllers
{
	public class AccountController : Controller
	{
		private readonly ILogger<AccountController> _logger;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountController(
			ILogger<AccountController> logger, 
			UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager,
			RoleManager<IdentityRole> roleManager)
		{
			_logger = logger;
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Register(string returnurl = null)
		{
			if (!await _roleManager.RoleExistsAsync("Admin"))
			{
				//create roles
				await _roleManager.CreateAsync(new IdentityRole("Admin"));
				await _roleManager.CreateAsync(new IdentityRole("User"));
			}

			List<SelectListItem> listItems = new List<SelectListItem>();
			listItems.Add(new SelectListItem()
			{
				Value = "Admin",
				Text = "Admin"
			});
			listItems.Add(new SelectListItem()
			{
				Value = "User",
				Text = "User"
			});

			ViewData["ReturnUrl"] = returnurl;
			RegisterViewModel registerViewModel = new RegisterViewModel()
			{
				RoleList = listItems
			};
			return View(registerViewModel);
		}

	}
}