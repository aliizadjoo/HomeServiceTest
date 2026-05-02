//using App.Domain.Core.Contracts.CityAgg.Service;
//using App.Domain.Core.Dtos.AccountAgg;
//using FluentValidation;
//using Microsoft.AspNetCore.Identity;
//using FluentValidation.AspNetCore;

//namespace App.Domain.AppServices.AccountAgg.Validations
//{
//    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
//    {
//        public UserRegisterDtoValidator(ICityService cityService, RoleManager<IdentityRole<int>> _roleManager)
//        {
//            RuleFor(x => x.FirstName)
                
//                .NotEmpty().WithMessage("{PropertyName} الزامی است");

//            RuleFor(x => x.LastName)
//                .NotEmpty().WithMessage("{PropertyName} الزامی است");

//            RuleFor(x => x.Email)
//                .NotEmpty().WithMessage("{PropertyName} الزامی است")
//                .EmailAddress().WithMessage("فرمت {PropertyName} صحیح نیست");

//            RuleFor(x => x.Password)
//                .NotEmpty().WithMessage("{PropertyName} الزامی است");

//            RuleFor(x => x.ConfirmPassword)
//                .Equal(x => x.Password).WithMessage("{PropertyName} با رمز عبور برابری نمی‌کند");

//            RuleFor(x => x.Role)
//               .NotEmpty().WithMessage("انتخاب {PropertyName} الزامی است")
//               .Must(role => !role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
//               .WithMessage("امکان انتخاب نقش ادمین وجود ندارد.")
//               .MustAsync(async (role, cancellationToken) =>
//               {
//                   return await _roleManager.RoleExistsAsync(role);
//               })
//               .WithMessage("{PropertyName} انتخاب شده معتبر نیست.");

//            RuleFor(c => c.CityId)
//                .NotEmpty().WithMessage("انتخاب {PropertyName} الزامی است")
//                .MustAsync(cityService.IsExist)
//                .WithMessage("{PropertyName} انتخاب شده معتبر نیست.");



//        }
//    }
//}
