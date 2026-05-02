using App.Domain.Core._common;
using App.Domain.Core.Dtos;
using App.Domain.Core.Dtos.ExpertAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.ExpertAgg.AppService
{
    public interface IExpertAppService
    {
        public Task<Result<ExpertsPagedDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
        public Task<Result<EditExpertDto?>> GetByAppUserIdForUpdate(int appUserId, CancellationToken cancellationToken);
        public Task<Result<bool>> Update(int appUserId, EditExpertDto dto, CancellationToken cancellationToken);
    }
}
