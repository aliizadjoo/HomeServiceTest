using App.Domain.Core._common;
using App.Domain.Core.Contracts.CityAgg.Repository;
using App.Domain.Core.Contracts.CustomerAgg.Repository;
using App.Domain.Core.Contracts.CustomerAgg.Service;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Services.CustomerAgg
{
    public class CustomerService(
        ICustomerRepository _customerRepository,
        ICityRepository _cityRepository,
        UserManager<AppUser> _userManager
        ) : ICustomerService
    {
        public async Task<Result<CustomerDto>> Create(int userId, int cityId, CancellationToken cancellationToken)
        {
            var data = await _customerRepository.Create(userId, cityId, cancellationToken);

            if (data == null)
            {
                return Result<CustomerDto>.Failure("عملیات ساخت کاستومر با خطا مواجه شد ");
            }

            return Result<CustomerDto>.Success(data);
        }

        public async Task<Result<CustomersPagedDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var customersPagedDto = await _customerRepository.GetAll(pageNumber, pageSize, cancellationToken);

            if (customersPagedDto != null && customersPagedDto.CustomersDto.Any())
            {
                return Result<CustomersPagedDto>.Success(customersPagedDto);
            }

            return Result<CustomersPagedDto>.Failure("هیچ مشتری  یافت نشد.");
        }

        public async Task<Result<UpdateCustomerDto?>> GetByAppUserIdForUpdate(int appUserId, CancellationToken cancellationToken)
        {
            var updateCustomerDto = await _customerRepository.GetByAppUserIdForUpdate(appUserId, cancellationToken);

            if (updateCustomerDto != null) 
            {
               return Result<UpdateCustomerDto>.Success(updateCustomerDto)!;
            }

            return Result<UpdateCustomerDto?>.Failure("همچین کاربری موجود نمیباشد");
        }

        public async Task<int> GetIdByAppUserId(int userId, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetIdByAppUserId(userId, cancellationToken);
        }

        public async Task<Result<bool>> Update(int appUserId, UpdateCustomerDto updateCustomerDto, bool isAdmin, CancellationToken cancellationToken)
        {

            var isExist = await _cityRepository.IsExist(updateCustomerDto.CityId, cancellationToken);
            if (!isExist)
            {
                return Result<bool>.Failure("شهر انتخاب شده نامعتبر است.");
            }

            if (isAdmin && !string.IsNullOrEmpty(updateCustomerDto.Email))
            {

                var user = await _userManager.FindByIdAsync(appUserId.ToString());
                if (user != null && user.Email != updateCustomerDto.Email)
                {
                    user.Email = updateCustomerDto.Email;
                    user.NormalizedEmail = updateCustomerDto.Email.ToUpper();
                    user.UserName = updateCustomerDto.Email;
                    user.NormalizedUserName = updateCustomerDto.Email.ToUpper();
                    user.EmailConfirmed = true;

                    var identityResult = await _userManager.UpdateAsync(user);
                    if (!identityResult.Succeeded)
                    {
                        return Result<bool>.Failure("خطا در بروزرسانی ایمیل توسط سیستم احراز هویت.");
                    }
                }
            }


            var result = await _customerRepository.Update(appUserId, updateCustomerDto, cancellationToken);

            if (!result)
            {
                return Result<bool>.Failure("ویرایش انجام نشد.");
            }

            return Result<bool>.Success(result,"ویرایش با موفقیت انجام شد.");

        }
    }
}
