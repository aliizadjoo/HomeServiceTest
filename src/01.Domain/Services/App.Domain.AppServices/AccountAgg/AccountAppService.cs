using App.Domain.Core.Const.UserAgg;
using App.Domain.Core.Contracts.AccountAgg.AppService;
using App.Domain.Core.Contracts.CityAgg.Service;
using App.Domain.Core.Contracts.CustomerAgg.Service;
using App.Domain.Core.Contracts.ExpertAgg.Service;
using App.Domain.Core.Dtos.AccountAgg;
using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace App.Domain.AppServices.AccountAgg
{
    public class AccountAppService(
        UserManager<AppUser> _userManager
       , ILogger<AccountAppService> _logger 
        ,IExpertService _expertService
        ,ICustomerService _customerService
        ,SignInManager<AppUser> _signInManager,
        RoleManager<IdentityRole<int>> _roleManager
        ,ICityService _cityService): IAccountAppService
    {
        public async Task<IdentityResult> Register(UserRegisterDto userRegisterDto, CancellationToken cancellationToken)
        {

            if (userRegisterDto.Role == AppRoleConstants.Admin)
            {
                _logger.LogWarning("تلاش برای ثبت نام با نقش ادمین توسط کاربر: {Email}", userRegisterDto.Email);

                return IdentityResult.Failed(
                    new IdentityError { Description = "امکان ثبت نام با نقش ادمین وجود ندارد." }
                );
            }
            if (!await _roleManager.RoleExistsAsync(userRegisterDto.Role))
            {
                _logger.LogError("نقش نامعتبر است: {Role}", userRegisterDto.Role);

                return IdentityResult.Failed(
                    new IdentityError { Description = "نقش انتخاب شده معتبر نیست." }
                );
            }

            var isExist=await _cityService.IsExist(userRegisterDto.CityId, cancellationToken);

            if (!isExist) 
            {
                _logger.LogError("CityId نامعتبر است: {CityId}", userRegisterDto.CityId);

                return IdentityResult.Failed(
                    new IdentityError { Description = "شهر انتخاب شده معتبر نیست." }
                );

            }

            var user = new AppUser
            {
                UserName = userRegisterDto.Email,
                Email = userRegisterDto.Email,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
            };

            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
            if (!result.Succeeded)
            {
                _logger.LogWarning("ثبت نام کاربر شکست خورد: {Email}", userRegisterDto.Email);
                return result;
            }

            var roleResult = await _userManager.AddToRoleAsync(user, userRegisterDto.Role);
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);

                _logger.LogError("خطا در اختصاص نقش: {Errors}",
                    string.Join(", ", roleResult.Errors.Select(e => e.Description)));

                return IdentityResult.Failed(
                    new IdentityError { Description = "خطا در اختصاص نقش به کاربر." }
                );
            }

            if (userRegisterDto.Role == AppRoleConstants.Customer)
            {
                var customerResult = await _customerService.Create(user.Id, userRegisterDto.CityId, cancellationToken);
                if (!customerResult.IsSuccess)
                {
                    await _userManager.DeleteAsync(user);
                    _logger.LogError("خطا در ساخت پروفایل Customer برای کاربر {UserId}", user.Id);

                   
                    await _userManager.DeleteAsync(user);

                    return IdentityResult.Failed(new IdentityError { Description = "خطا در ساخت پروفایل کاربری." });
                }

            }
            else if (userRegisterDto.Role == AppRoleConstants.Expert)
            {
                var expertResult = await _expertService.Create(user.Id, userRegisterDto.CityId, cancellationToken);
                if (!expertResult.IsSuccess)
                {
                    _logger.LogError("خطا در ساخت پروفایل Expert برای کاربر {UserId}", user.Id);

                    await _userManager.DeleteAsync(user);

                    return IdentityResult.Failed(new IdentityError { Description = "خطا در ساخت پروفایل کاربری." });
                }
            }

            return IdentityResult.Success;

        }

        public async Task<bool> Login(UserLoginDto userLoginDto)
        {

            var result = await _signInManager.PasswordSignInAsync(userLoginDto.UserName, userLoginDto.Password, userLoginDto.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation("Login successful for user: {UserName}", userLoginDto.UserName);
                return true;
            }
            _logger.LogWarning("Login failed for user: {UserName}. Result: {Result}", userLoginDto.UserName, result.ToString());

            return false;
        }

        public async Task Logout()
        {

           _logger.LogInformation("User is logging out.");
            await _signInManager.SignOutAsync();
        }
    }
}
