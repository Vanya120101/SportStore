using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.WebService.Models.ViewModels;
using System.Threading.Tasks;

namespace SportsStore.WebService.Controllers;
public class AccountController : Controller
{
	private readonly UserManager<IdentityUser> _userManager;
	private readonly SignInManager<IdentityUser> _signInManager;

	public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
	{
		_userManager = userManager ?? throw new System.ArgumentNullException(nameof(userManager));
		_signInManager = signInManager ?? throw new System.ArgumentNullException(nameof(signInManager));
	}

	[AllowAnonymous]
	public ViewResult Login(string returnUrl)
	{
		var login = new LoginModel { ReturnUrl = returnUrl };

		return View(login); ;
	}

	[HttpPost]
	[AllowAnonymous]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginModel loginModel)
	{
		if(!ModelState.IsValid)
		{
			ModelState.AddModelError("", "Invalid name or password");
			return View(loginModel);
		}

		var user = await _userManager.FindByNameAsync(loginModel.Name);

		if(user != null)
		{
			await _signInManager.SignOutAsync();
			var succeeded = (await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded;
			if(succeeded)
			{
				return Redirect(loginModel?.ReturnUrl ?? "Admin/Index");
			}
		}

		ModelState.AddModelError("", "Invalid name or password");
		return View(loginModel);
	}

	public async Task<RedirectResult> Logout(string returnUrl = "/")
	{
		await _signInManager.SignOutAsync();
		return Redirect(returnUrl);
	}


}
