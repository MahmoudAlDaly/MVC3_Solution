using Demo_DAL.Models;
using Demo_PL.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo_PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}



		#region SignUp
		[HttpGet]
		public IActionResult SighUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SighUp(SignUpViewModel signUpViewModel)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser user = await _userManager.FindByNameAsync(signUpViewModel.UserName);

				if (user is null)
				{
					user = new ApplicationUser();
					user.FName = signUpViewModel.FirstName;
					user.LName = signUpViewModel.LastName;
					user.UserName = signUpViewModel.UserName;
					user.Email = signUpViewModel.Email;
					user.IsAgree = signUpViewModel.IsAgree;

					var result = await _userManager.CreateAsync(user, signUpViewModel.Password);

					if (result.Succeeded)
					{
						return RedirectToAction(nameof(SignIn));
					}

					foreach (var item in result.Errors)
					{
						ModelState.AddModelError(string.Empty, item.Description);
					}

				}
				ModelState.AddModelError(string.Empty, "this user name is exit");
			}

			return View(signUpViewModel);
		}

		#endregion

		#region SignIn

		//public IActionResult SignIn()
		//{
		//	return View();
		//}

		#endregion

	}
}
