using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.WebApplication.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult AccessDenied(string ReturnUrl = "")
        {
            return View();
        }

        public async Task<IActionResult> Logout(string returnUrl = "")
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            if (returnUrl != "")
            {
                return Redirect(returnUrl);
            }
            else
            {
                return View("Logout");
            }
        }

        //private IUserReponsitory _userRepository;
        //public AccountController(IUserReponsitory userRepository)
        //{
        //    _userRepository = userRepository;
        //}

        //[AllowAnonymous]
        //public IActionResult Login(string returnUrl = "/")
        //{
        //    return View(new LoginModel { ReturnUrl = returnUrl });
        //}

        //[AllowAnonymous]
        //[HttpPost]
        //public async Task<IActionResult> Login (LoginModel model)
        //{
        //    var user = _userRepository.GetByUserNameAndPassWord(model.UserName, model.PassWord);
        //    if (user == null)
        //        return Unauthorized();

        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        //        new Claim(ClaimTypes.Name, user.FullName),
        //        new Claim(ClaimTypes.Role, user.Role),
        //        new Claim("FavoriteColor", user.FavoriteColor)

        //    };

        //    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //    var principal = new ClaimsPrincipal(identity);

        //    await HttpContext.SignInAsync(
        //        CookieAuthenticationDefaults.AuthenticationScheme, principal,
        //        new AuthenticationProperties { IsPersistent = model.RememberMe });

        //    return LocalRedirect(model.ReturnUrl);

        //}

        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return Redirect("/");
        //}

        //[AllowAnonymous]
        //public async Task<IActionResult> LoginWidthGoogle(string returlUrl = "/")
        //{
        //    var props = new AuthenticationProperties
        //    {
        //        RedirectUri = Url.Action("GoogleLoginCallBack"),
        //        Items =
        //        {
        //            {"returnURL", returlUrl }
        //        }
        //    };
        //    return Challenge(props, GoogleDefaults.AuthenticationScheme);
        //}

        //[AllowAnonymous]
        //public async Task<IActionResult> GoogleLoginCallBack()
        //{
        //    var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        //    var claims = result.Principal.Claims;
        //    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //    var claimPricipal = new ClaimsPrincipal(identity);

        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPricipal);
        //    return LocalRedirect(result.Properties.Items["returnURL"]);
        //}

    }
}
