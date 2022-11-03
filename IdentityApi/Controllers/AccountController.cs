using IdentityApi.Extensions;
using IdentityApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace IdentityApi.Controllers;

public class AccountController : Controller
{
	private readonly UserManager<IdentityUser> _userManager;
	private readonly SignInManager<IdentityUser> _signInManager;

	public AccountController(
		UserManager<IdentityUser> userManager,
		SignInManager<IdentityUser> signInManager)
	{
		_userManager = userManager;
		_signInManager = signInManager;
	}

	[HttpGet]
	public IActionResult Register()
	{
		RegisterViewModel registerViewModel = new RegisterViewModel();

		return View(registerViewModel);
	}
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Register(RegisterViewModel model)
	{
		if (ModelState.IsValid)
		{
			var user = new ApplicationUser()
			{
				UserName = model.Email,
				Email = model.Email,
				Name = model.Name,
			};
			
			await _userManager.CreateAsync(user, model.Password);

			await _signInManager.SignInAsync(user, isPersistent: false);

			return RedirectToAction("Index", "Home");
		}

		return View(model);
	}

	private void AddErrors(IdentityResult result)
	{
		foreach (var error in result.Errors)
		{
			ModelState.AddModelError(string.Empty, error.Description);
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> LogOff()
	{
		await _signInManager.SignOutAsync();

		return RedirectToAction(nameof(HomeController.Index), "Home");
	}

	[HttpGet]
	public IActionResult Login()
	{
		return View();
	}
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginViewModel model)
	{
		if (ModelState.IsValid)
		{
			try
			{
				var identityUser = await _userManager.FindByEmailAsync(model.Email);

				var hashMd5 = model.Password.ToMd5HashString();

				if (identityUser != null)
				{
					if (identityUser.PasswordHash == hashMd5 || identityUser.PasswordHash == null)
					{
						var hasher = _userManager.PasswordHasher;

						var hashedPassword = hasher.HashPassword(identityUser, model.Password);

						identityUser.PasswordHash = hashedPassword;

						await _userManager.UpdateAsync(identityUser);

						var result = await _signInManager.PasswordSignInAsync(
							model.Email,
							model.Password,
							model.RememberMe,
							lockoutOnFailure: true);

						if (result.Succeeded)
						{
							return RedirectToAction("Index", "Home");
						}
					}
					else
					{
						var result = await _signInManager.PasswordSignInAsync(
							model.Email,
							model.Password,
							model.RememberMe,
							lockoutOnFailure: true);

						if (result.Succeeded)
						{
							return RedirectToAction("Index", "Home");
						}
					}
				}

				ModelState.AddModelError(string.Empty, "Invalid login attempt.");

				return View(model);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);

				throw;
			}
		}

		return View(model);
	}

}

