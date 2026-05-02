using App.Domain.AppServices.CityAgg;
using App.Domain.AppServices.CustomerAgg;
using App.Domain.Core.Const.UserAgg;
using App.Domain.Core.Contracts.CityAgg.AppService;
using App.Domain.Core.Contracts.CustomerAgg.AppService;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Entities;
using App.EndPoints.MVC.HomeService.Area.Constants;
using App.EndPoints.MVC.HomeService.Areas.Admin.Models;
using App.EndPoints.MVC.HomeService.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Controllers
{

    [Area(AreaConstants.Admin)]
    [Authorize(Roles = AppRoleConstants.Admin)]
    public class CustomersController
        (ICustomerAppService _customerAppService,
        ICityAppService _cityAppService
        ) 
        : Controller
    {
        public async Task<IActionResult> Update(int appUserId, CancellationToken cancellationToken)
        {
            var customerResult = await _customerAppService.GetByAppUserIdForUpdate(appUserId, cancellationToken);

            if (!customerResult.IsSuccess)
            {
               
                TempData["ErrorMessage"] = customerResult.Message;
                return RedirectToAction("Customers", "User"); 
            }

      
            var cities = await _cityAppService.GeAll(cancellationToken);

            var viewModel = new UpdateCustomerViewModel
            {
                AppUserId = appUserId,
                FirstName = customerResult.Data.FirstName,
                LastName = customerResult.Data.LastName,
                Email = customerResult.Data.Email!,
                PhoneNumber = customerResult.Data.PhoneNumber,
                Address = customerResult.Data.Address,
                CityId = customerResult.Data.CityId,
                WalletBalance = customerResult.Data.WalletBalance,
                ExistingImagePath = customerResult.Data.ImagePath,
                Cities = cities 
            };

            return View(viewModel);
        }

        

        [HttpPost] 
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Update(UpdateCustomerViewModel viewModel, CancellationToken cancellationToken)
        {
          
            if (!ModelState.IsValid)
            {
                viewModel.Cities = await _cityAppService.GeAll(cancellationToken);
                return View(viewModel); 
            }

            
            string? imagePath = viewModel.ExistingImagePath; 

           
            if (viewModel.NewProfileImage != null && viewModel.NewProfileImage.Length > 0)
            {
                
                var uploadedFileName = viewModel.NewProfileImage.UploadFile("customers");

                if (!string.IsNullOrEmpty(uploadedFileName))
                {
                    imagePath = uploadedFileName;
                
                }
            }

           
            var updateDto = new UpdateCustomerDto
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                Address = viewModel.Address,
                CityId = viewModel.CityId,
                WalletBalance = viewModel.WalletBalance,
                ImagePath = imagePath 
            };

            var result = await _customerAppService.Update(viewModel.AppUserId, updateDto, true, cancellationToken);

          
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "اطلاعات مشتری با موفقیت ویرایش شد.";
                return RedirectToAction("Customers", "User");  
            }
            else
            {
               
                ModelState.AddModelError(string.Empty, result.Message ?? "خطایی در هنگام ویرایش رخ داد.");
                
                viewModel.Cities = await _cityAppService.GeAll(cancellationToken);
                return View(viewModel);
            }
        }

    }
}
