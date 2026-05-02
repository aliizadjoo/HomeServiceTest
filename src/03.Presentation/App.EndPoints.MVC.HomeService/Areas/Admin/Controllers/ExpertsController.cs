using App.Domain.Core.Const.UserAgg;
using App.Domain.Core.Contracts.CityAgg.AppService;
using App.Domain.Core.Contracts.ExpertAgg.AppService;
using App.Domain.Core.Contracts.HomeServiceAgg.AppService;
using App.Domain.Core.Dtos;
using App.EndPoints.MVC.HomeService.Area.Constants;
using App.EndPoints.MVC.HomeService.Areas.Admin.Models;
using App.EndPoints.MVC.HomeService.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Controllers
{
    [Area(AreaConstants.Admin)]
    [Authorize(Roles =AppRoleConstants.Admin)]
    public class ExpertsController
        (IExpertAppService _expertAppService,
        ILogger<ExpertsController> _logger,
        IHomeServiceAppService _homeServiceAppService,
        ICityAppService _cityAppService
        ) 
        : Controller
    {
       
        public async Task<IActionResult> Update(int appUserId, CancellationToken cancellationToken)
        {
            if (appUserId <= 0) return NotFound();

            var expertResult = await _expertAppService.GetByAppUserIdForUpdate(appUserId, cancellationToken);

            if (!expertResult.IsSuccess || expertResult.Data == null)
            {
                _logger.LogWarning("دریافت اطلاعات برای ویرایش کارشناس با شناسه {Id} ناموفق بود.", appUserId);
                TempData["ErrorMessage"] = "کارشناس مورد نظر یافت نشد.";
                return RedirectToAction("Customers","User"); 
            }

            var expert = expertResult.Data;
            var servicesResult = await _homeServiceAppService.GetAll(cancellationToken);

            var viewModel = new UpdateExpertViewModel 
            {
                AppUserId = expert.AppUserId,
                FirstName = expert.FirstName,
                LastName = expert.LastName,
                Email = expert.Email!,
                Bio = expert.Bio,
                WalletBalance = expert.WalletBalance ?? 0,
                CityId = expert.CityId,
                ExistingImagePath = expert.ImagePath,
                PhoneNumber = expert.PhoneNumber,
                SelectedHomeServicesId = expert.HomeServiceIds ?? new List<int>(), 
            };

            await PopulateViewModelLists(viewModel, cancellationToken);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateExpertViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
           
                await PopulateViewModelLists(viewModel, cancellationToken);
                return View(viewModel);
            }

        
            string? newImageName = viewModel.NewProfileImage?
                                       .UploadFile("experts");

            var dto = new EditExpertDto
            {
                AppUserId = viewModel.AppUserId,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                Bio = viewModel.Bio,
                CityId = viewModel.CityId,
                WalletBalance = viewModel.WalletBalance,
                HomeServiceIds = viewModel.SelectedHomeServicesId,
                ImagePath = newImageName ?? viewModel.ExistingImagePath
            };

            var updateResult = await _expertAppService.Update(viewModel.AppUserId, dto, cancellationToken);

            if (updateResult.IsSuccess)
            {
                TempData["SuccessMessage"] = "اطلاعات کارشناس با موفقیت ویرایش شد.";
                return RedirectToAction("Customers", "User");
            }

            _logger.LogError("ویرایش کارشناس با شناسه {Id} ناموفق بود. پیام خطا: {Message}", viewModel.AppUserId, updateResult.Message);
            ModelState.AddModelError(string.Empty, updateResult.Message);
            await PopulateViewModelLists(viewModel, cancellationToken);
            return View(viewModel);
        }

       
        private async Task PopulateViewModelLists(UpdateExpertViewModel viewModel, CancellationToken cancellationToken)
        {
          
            viewModel.AvailableCities = await _cityAppService.GeAll(cancellationToken);

            var servicesResult = await _homeServiceAppService.GetAll(cancellationToken);
            if (servicesResult.IsSuccess)
            {
                viewModel.AvailableServices = servicesResult.Data;
            }
        }
    }
}
