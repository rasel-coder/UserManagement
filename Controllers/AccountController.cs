using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Data;
using UserManagement.ViewModels;

namespace UserManagement.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly INotyfService toastNotification;
    private readonly ILogger<AccountController> logger;
    private readonly IMapper mapper;

    public AccountController(UserManager<ApplicationUser> userManager
        , SignInManager<ApplicationUser> signInManager
        , INotyfService toastNotification
        , ILogger<AccountController> logger
        , IMapper mapper)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.toastNotification = toastNotification;
        this.logger = logger;
        this.mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> LogIn(string? returnUrl)
    {
        LoginViewModel model = new LoginViewModel()
        {
            ReturnUrl = returnUrl
        };
        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> LogIn(LoginViewModel model, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            var result = new Microsoft.AspNetCore.Identity.SignInResult();
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null && model.Password != null)
                result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

            if (user != null && result.Succeeded)
            {
                user.LastLoggedIn = DateTime.UtcNow;
                await userManager.UpdateAsync(user);

                toastNotification.Success("Logged in successfully");
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                toastNotification.Warning("User is blocked. Please try with another user");
                return View();
            }

            toastNotification.Custom("Invalid email or password", 10, "#B600FF", "bx bxs-error");
        }
        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp()
    {
        SignUpViewModel model = new SignUpViewModel();
        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = mapper.Map<ApplicationUser>(model);
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
                if (signInManager.IsSignedIn(User))
                {
                    user.LastLoggedIn = DateTime.UtcNow;
                    await userManager.UpdateAsync(user);

                    toastNotification.Success("Registration successful");
                    return RedirectToAction("Index", "Home");
                }
            }
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            toastNotification.Error("Registration failed");
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> LogOut()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home", new { area = "" });
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
