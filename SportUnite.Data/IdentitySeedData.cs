using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SportUnite.Data
{
    public static class IdentitySeedData
    {
	    private const string AdminUser = "Admin";
	    private const string AdminPassword = "Secret123$";

	    public static async void EnsurePopulated(IApplicationBuilder apps)
	    {
		    UserManager<IdentityUser> userManager = apps.ApplicationServices.GetRequiredService<UserManager<IdentityUser>>();

		    IdentityUser user = await userManager.FindByIdAsync(AdminUser);
		    if (user == null)
		    {
			    user = new IdentityUser("Admin");
			    await userManager.CreateAsync(user, AdminPassword);
		    }
	    }
    }
}
