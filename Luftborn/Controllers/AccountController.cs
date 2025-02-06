namespace Luftborn.Controllers;

public class AccountController : Controller
{
    public IActionResult Logout()
    {
        var callbackUrl = Url.Action(nameof(HomeController.Index), "Home", null, Request.Scheme);
        return SignOut(
            new AuthenticationProperties { RedirectUri = callbackUrl },
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme);
    }
}
