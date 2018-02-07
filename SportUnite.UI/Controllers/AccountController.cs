using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SportUnite.UI.ViewModels;

namespace SportUnite.UI.Controllers
{
	[Authorize]
    public class AccountController : Controller
    {
	    private UserManager<IdentityUser> _userManager;
	    private SignInManager<IdentityUser> _signInManager;

	    public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
	    {
		    _userManager = userMgr;
		    _signInManager = signInMgr;
	    }
		
		[AllowAnonymous]
	    public ViewResult Login(string returnUrl)
	    {
		    return View(new LoginModel
		    {
			    ReturnUrl = returnUrl
		    });
	    }

	    [HttpPost]
	    [AllowAnonymous]
	    [ValidateAntiForgeryToken]
	    public async Task<IActionResult> Login(LoginModel model)
	    {
		    if (ModelState.IsValid)
		    {
			    IdentityUser user = await _userManager.FindByNameAsync(model.Name);

			    if (user != null)
			    {
				    await _signInManager.SignOutAsync();
				    if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
				    {
					    return Redirect(model?.ReturnUrl ?? "/Home/Index");
				    }
			    }
		    }

			ModelState.AddModelError("", "Invalid name or password");
		    return View(model);
	    }

	    public async Task<IActionResult> Logout(string returnUrl = "/")
	    {
		    await _signInManager.SignOutAsync();
		    return Redirect(returnUrl);
	    }
    }
}
