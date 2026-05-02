using App.Domain.Core.Const.UserAgg;
using App.EndPoints.MVC.HomeService.Area.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Controllers
{
    [Area(AreaConstants.Admin)]
    [Authorize(Roles = AppRoleConstants.Admin)]
    public class PanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
