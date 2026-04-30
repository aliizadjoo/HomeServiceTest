using App.Domain.Core._common;
using App.Domain.Core.Contracts.ExpertAgg.Repository;
using App.Domain.Core.Contracts.ExpertAgg.Service;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.ExpertAgg
{
    public class ExpertService(IExpertRepository _expertRepository) : IExpertService
    {
        public async Task<Result<ExpertDto>> Create(int userId, int cityId, CancellationToken cancellationToken)
        {
            var data=await _expertRepository.Create(userId, cityId, cancellationToken);
            if (data==null)
            {
                return Result<ExpertDto>.Failure("عملیات ساخت متخصص با خطا مواجه شد ");
            }
            return Result<ExpertDto>.Success(data);
        }
    }
}
