using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Infrastructure.Identity;
using PDHourTracker.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Controllers
{
    /// <summary>
    /// Controller is used to sign in/out users and create accounts using external authentication providers.
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        public IActionResult AccessDenied(string returnUrl)
        {
            ViewData["Host"] = HttpContext.Request.Host.Host;
            //ViewData["Protocol"] = HttpContext.Request.Protocol;
            ViewData["IsHttps"] = HttpContext.Request.IsHttps;
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            // If user is already login, bypass login
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToLocal(returnUrl);
            }

            // Clear any existing cookie
            HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external provider if the user already has a login
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey,
                isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                // log -- needs to be setup
                //Log.Information($"User sign in {info.LoginProvider} "
                //    + $"({info.Principal.FindFirstValue(ClaimTypes.Email)})");

                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                // Attempt to get email and name from account info
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
                var lastName = info.Principal.FindFirstValue(ClaimTypes.Surname);

                // See if user exists but has not confirmed email
                //var user = _userManager.FindByEmailAsync(email).Result;
                //if (user != null)
                //{
                    //if (!user.EmailConfirmed)
                    //{
                    //    // Direct to page asking to resend email confirmation
                    //    return RedirectToAction(nameof(ResendEmailVerification),
                    //        new { userId = user.Id });
                    //}
                //}

                return View("ExternalLogin", new ExternalLoginViewModel
                {
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if ((info == null))
                {
                    throw new ApplicationException("Error loading external login information during confirmation");
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    // Log new user crreated
                    //Log.Information($"New user created {model.Email}");

                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        // Sign in the user with this external provider
                        var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey,
                            isPersistent: false, bypassTwoFactor: true);

                        if (signInResult.Succeeded)
                        {
                            // Log login added
                            //Log.Information($"New login added {model.Email} {info.LoginProvider}");

                            //await _signInManager.SignInAsync(user, isPersistent: false); // Confirm email first
                            //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                            //var callbackUrl = GenerateEmailConfirmationLink(user.Id, code);
                            //await SendEmailConfirmationEmail(user.Email, callbackUrl);
                            // log
                            //return RedirectToAction("EmailConfirmationSent", new { email = user.Email });
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            // log
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #region Helpers
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        #endregion
    }
}
