using App.Domain.Core.Const.UserAgg;
using App.Domain.Core.Contracts.AdminAgg.AppService;
using App.Domain.Core.Contracts.CustomerAgg.AppService;
using App.Domain.Core.Contracts.ExpertAgg.AppService;
using App.Domain.Core.Dtos.AdminAgg;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using App.Domain.Core.Enums.UserAgg;
using App.EndPoints.MVC.HomeService.Area.Constants;
using App.EndPoints.MVC.HomeService.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Controllers
{
    [Area(AreaConstants.Admin)]
    [Authorize(Roles = AppRoleConstants.Admin)]
    public class UserController(
        ICustomerAppService _customerAppService
        ,IExpertAppService _expertAppService
        ,IAdminAppService _adminAppService
        ) 
        : Controller
    {
        public async Task<IActionResult> Customers(int pageNumber = 1, int pageSize = 4,  CancellationToken cancellationToken = default)
        {
            var result = await _customerAppService.GetAll(pageNumber,pageSize, cancellationToken);

            var viewModel = new UserIndexViewModel
            {
                ActiveRole = UserRole.Customer,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

            if (result.IsSuccess)
            {
                viewModel.Customers = result.Data;
              
                viewModel.TotalPages = (int)Math.Ceiling((double)result.Data.TotalCount / pageSize);
            }
            else
            {
              
                ViewBag.ErrorMessage = result.Message ?? "خطایی در دریافت اطلاعات رخ داد.";
                viewModel.Customers = new CustomersPagedDto(); 
            }

            return View("index", viewModel);
        }

        public async Task<IActionResult> Experts(int pageNumber = 1, int pageSize = 4, CancellationToken cancellationToken = default)
        {
            var result = await _expertAppService.GetAll(pageNumber, pageSize, cancellationToken);

            var viewModel = new UserIndexViewModel
            {
                ActiveRole = UserRole.Expert,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

            if (result.IsSuccess)
            {
                viewModel.Experts = result.Data;

                viewModel.TotalPages = (int)Math.Ceiling((double)result.Data.TotalCount / pageSize);
            }
            else
            {

                ViewBag.ErrorMessage = result.Message ?? "خطایی در دریافت اطلاعات رخ داد.";
                viewModel.Experts = new ExpertsPagedDto();
            }

            return View("index", viewModel);
        }

        public async Task<IActionResult> Admins(int pageNumber = 1, int pageSize = 4, CancellationToken cancellationToken = default)
        {

            var result = await _adminAppService.GetAll(pageNumber, pageSize, cancellationToken);

            var viewModel = new UserIndexViewModel
            {
                ActiveRole = UserRole.Admin,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

            if (result.IsSuccess)
            {
                viewModel.Admins = result.Data;
                viewModel.TotalPages = (int)Math.Ceiling((double)result.Data.TotalCount / pageSize);
            }
            else
            {
                ViewBag.ErrorMessage = result.Message ?? "خطایی در دریافت اطلاعات رخ داد.";
                viewModel.Admins = new AdminsPagedDto();
            }

            return View("index", viewModel);
        }


    }

}
