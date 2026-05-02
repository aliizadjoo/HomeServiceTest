using App.Domain.Core._common;
using App.Domain.Core.Dtos.AdminAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.AdminAgg.AppService
{
    public interface IAdminAppService
    {
        public Task<Result<AdminsPagedDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
