using App.Domain.Core.Const.UserAgg;
using App.Domain.Core.Contracts.CustomerAgg.Service;
using App.Domain.Core.Contracts.ExpertAgg.Service;
using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace App.Domain.AppServices.AccountAgg
{
    public class CustomUserClaimsPrincipalFactory(
     UserManager<AppUser> userManager,
     RoleManager<IdentityRole<int>> roleManager,
     IOptions<IdentityOptions> options,
     ICustomerService _customerService,
     IExpertService _expertService
 ) : UserClaimsPrincipalFactory<AppUser, IdentityRole<int>>(userManager, roleManager, options)
    {
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {

            var identity = await base.GenerateClaimsAsync(user);

            var roles = await userManager.GetRolesAsync(user);

            if (roles.Contains(AppRoleConstants.Customer))
            {

                var customerId = await _customerService.GetIdByAppUserId(user.Id, default);
               
                if (customerId > 0)
                {
                    identity.AddClaim(new Claim(CustomClaimTypes.CustomerId , customerId.ToString()));
                }
            }

            if (roles.Contains(AppRoleConstants.Expert))
            {
                var expertId = await _expertService.GetIdByAppUserId(user.Id, default);
                if (expertId > 0)
                {
                    identity.AddClaim(new Claim(CustomClaimTypes.ExpertId, expertId.ToString()));
                }
            }

            return identity;
        }
    }
}
