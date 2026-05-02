using App.Domain.Core._common;
using App.Domain.Core.Contracts.ExpertAgg.AppService;
using App.Domain.Core.Contracts.ExpertAgg.Service;
using App.Domain.Core.Dtos;
using App.Domain.Core.Dtos.ExpertAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.ExpertAgg
{
    public class ExpertAppService(IExpertService _expertService) : IExpertAppService
    {
        public async Task<Result<ExpertsPagedDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
           return await _expertService.GetAll(pageNumber, pageSize, cancellationToken);
        }

        public async Task<Result<EditExpertDto?>> GetByAppUserIdForUpdate(int appUserId, CancellationToken cancellationToken)
        {
           return await  _expertService.GetByAppUserIdForUpdate(appUserId, cancellationToken);
        }

        public async Task<Result<bool>> Update(int appUserId, EditExpertDto dto, CancellationToken cancellationToken)
        {
            return await  _expertService.Update(appUserId, dto, cancellationToken);
        }
    }
}
