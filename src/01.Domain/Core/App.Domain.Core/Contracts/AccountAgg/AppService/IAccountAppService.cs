using App.Domain.Core.Dtos.AccountAgg;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.AccountAgg.AppService
{
    public interface IAccountAppService
    {
        public Task<IdentityResult> Register(UserRegisterDto userRegisterDto, CancellationToken cancellationToken);
        public Task<bool> Login(UserLoginDto userLoginDto);
        public Task Logout();
    }
}
