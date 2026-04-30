using App.Domain.Core._common;
using App.Domain.Core.Dtos.ExpertAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.ExpertAgg.Service
{
    public interface IExpertService
    {

        public Task<Result<ExpertDto>> Create(int userId, int cityId, CancellationToken cancellationToken);
    }
}
