using App.Domain.Core.Dtos.AdminAgg;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using App.Domain.Core.Enums.UserAgg;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Models
{
    public class UserIndexViewModel
    {

        public CustomersPagedDto Customers { get; set; }
        public ExpertsPagedDto Experts { get; set; }
        public AdminsPagedDto Admins { get; set; }

        public UserRole ActiveRole { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        //public int TotalPages => (int)Math.Ceiling((double)Customers.TotalCount / PageSize);
    }
}
