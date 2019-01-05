using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TyreKlicker.API.Extensions;
using TyreKlicker.API.Models;
using TyreKlicker.API.Models.AccountViewModels;
using TyreKlicker.API.Services;
using TyreKlicker.API.Services.Token;
using TyreKlicker.Application.User.Command.CreateUser;
using TyreKlicker.Infrastructure.Identity.Models;

namespace TyreKlicker.API.Controllers
{
    [Authorize]
    [Route("api/account/[action]")]
    //[ApiController]
    public class ApiAccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IUserTokenService _userTokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public ApiAccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            IUserTokenService userTokenService,
            IRefreshTokenService refreshTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _userTokenService = userTokenService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return Ok();
                }
                //if (result.RequiresTwoFactor)
                //{
                //    return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                //}
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return Unauthorized("Your account has been locked.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return BadRequest(model);
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO Check user exists in Application Db - If it does send email to reset password
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");

                    var command = new CreateUserCommand()
                    {
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    };

                    return Ok(await Mediator.Send(command));
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return BadRequest(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> TokenLogin([FromBody]LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userToken = _userTokenService.CreateUserToken(user);
                return Ok(userToken);
            }

            return NotFound("Invalid email or password.");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshAccessToken(string token)
        {
            var refreshToken = _refreshTokenService.GetRefreshToken(token);

            if (refreshToken.Token == null)
            {
                return NotFound("Refresh token was not found.");
            }
            if (refreshToken.Revoked)
            {
                return BadRequest("Refresh token was revoked.");
            }
            var user = await _userManager.FindByEmailAsync(refreshToken.Email);

            var userToken = _userTokenService.CreateUserToken(user);

            return Ok(userToken);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RevokeRefreshToken(string token)
        {
            var refreshToken = _refreshTokenService.GetRefreshToken(token);

            if (refreshToken.Token == null)
            {
                return NotFound("Refresh token was not found.");
            }
            if (refreshToken.Revoked)
            {
                return BadRequest("Refresh token was revoked.");
            }
            _refreshTokenService.RevokeRefreshToken(token);
            return Ok();
        }

        //#region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        //#endregion Helpers
    }
}