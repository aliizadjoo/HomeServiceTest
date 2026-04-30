using App.Domain.AppServices.AccountAgg;
using App.Domain.Core.Contracts.AccountAgg.AppService;
using App.Domain.Core.Contracts.CityAgg.AppService;
using App.Domain.Core.Dtos.AccountAgg;
using App.EndPoints.MVC.HomeService.Area.Constants;
using App.EndPoints.MVC.HomeService.Areas.Identity.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.HomeService.Area.Identity.Controllers
{
    [Area(AreaConstants.Identity)]
    public class AccountController(ICityAppService _cityAppService
        , ILogger<AccountController> _logger,
        IAccountAppService _accountAppService
       ) : Controller
    {

        public async Task<IActionResult> Login(CancellationToken cancellationToken)
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userLoginDto = new UserLoginDto
            {
                UserName = model.UserName,
                Password = model.Password,
                RememberMe = model.RememberMe
            };

            var result = await _accountAppService.Login(userLoginDto);

            if (result)
            {
                _logger.LogInformation("User {UserName} logged in successfully.", model.UserName);


                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return LocalRedirect(model.ReturnUrl);
                }

                return RedirectToAction("Index", "Home", new { area = "" });
            }

            _logger.LogWarning("Failed login attempt for user {UserName}.", model.UserName);
            ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است.");
            return View(model);
        }

        public async Task<IActionResult> Register(CancellationToken cancellationToken)
        {
            ViewBag.Cities = await _cityAppService.GeAll(cancellationToken);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("تلاش برای ثبت‌نام با داده‌های نامعتبر توسط: {Email}", model.Email);


                ViewBag.Cities = await _cityAppService.GeAll(cancellationToken);
                return View(model);
            }


            var registerDto = new UserRegisterDto
            {
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Role = model.Role,
                CityId = model.CityId
            };


            var result = await _accountAppService.Register(registerDto, cancellationToken);

            if (result.Succeeded)
            {

                _logger.LogInformation("کاربر {UserName} با موفقیت ثبت‌نام کرد.", registerDto.Email);
                TempData["SuccessMessage"] = "ثبت‌نام شما با موفقیت انجام شد. اکنون می‌توانید وارد شوید.";
                return RedirectToAction("Login");
            }


            _logger.LogWarning("خطا در ثبت‌نام کاربر {Email}. جزییات: {Errors}",
            registerDto.Email, string.Join(", ", result.Errors.Select(e => e.Description)));

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }


            ViewBag.Cities = await _cityAppService.GeAll(cancellationToken);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _accountAppService.Logout();

            return RedirectToAction("Index", "Home", new { area = "" });
        }

    }
}
