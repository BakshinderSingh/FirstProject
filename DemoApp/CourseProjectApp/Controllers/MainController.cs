using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CourseProjectApp.Entities;
using CourseProjectApp.Services;
using CourseProjectApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseProjectApp.Controllers
{
    public class MainController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISmsSend _smsSend;

        public MainController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, ISmsSend smsSend)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _smsSend=smsSend;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResults = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (loginResults.Succeeded)
                {
                    if(_signInManager.IsSignedIn(User))
                    {

                    }
                    return RedirectToAction("Index", "LoggedIn");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Imformation");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Username
                };
                var identityResults = await _userManager.CreateAsync(identityUser, model.Password);
                if (identityResults.Succeeded)
                {
                    await _signInManager.SignInAsync(identityUser, isPersistent: false);
                    return RedirectToAction("Index", "LoggedIn");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error in creating my User");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("Index", "Main");
                }
                var code = "tolen";
                var result = await _userManager.ResetPasswordAsync(user, code, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "LoggedIn");
                }
            }
            return View();
        }

        public IActionResult FogotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FogotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user==null ||!(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return View("ForgotPasswordConfirmation");                    
                }
                // Send Email Confirmation

            }
            return View();
        }
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Main");
        }

        [HttpGet]
        public async Task<ActionResult> SmsTest()
        {
            await _smsSend.SendSmsAsync("9988900820", "Test Message");
            return RedirectToAction("Index", "Main");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider,string returnUrl=null)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Main", new {returnUrl=returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties,provider);
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl=null,string remoteError=null)
        {
            if(remoteError!=null)
            {
                return View("ExternalLoginFailure");
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(ExternalLoginfailure));
            }
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if(result.Succeeded)
            {
                if(returnUrl==null)
                {
                    return RedirectToAction("Index", "LoggedIn");
                }
                return Redirect(returnUrl);
            }
            else
            {
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email });                
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model,string returnUrl=null)
        {
            if(ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var alreadyRegistered = await _userManager.FindByEmailAsync(model.Email);
                var result = await _userManager.CreateAsync(user);
                if(result.Succeeded || alreadyRegistered.UserName != null)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        RedirectToAction("Index", "LoggedIn");
                    }
                }                
            }
            ViewData["ReturnUrl"] = returnUrl;
                return View(model);
        }

        public IActionResult ExternalLoginfailure()
        {
            return View();
        }
    }
}