using App.Domain.Core._common;
using App.Domain.Core.Dtos;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.ExpertAgg.Repository
{
    public  interface IExpertRepository
    {
        public Task<ExpertDto?> Create(int userId, int cityId, CancellationToken cancellationToken);
        public Task<int> GetIdByAppUserId(int userId, CancellationToken cancellationToken);
        public Task<EditExpertDto?> GetByAppUserIdForUpdate(int appUserId, CancellationToken cancellationToken);
        public Task<bool> Update(int appUserId, EditExpertDto dto, CancellationToken cancellationToken);

        public Task<ExpertsPagedDto> GetAll(int pageNumber , int pageSize , CancellationToken cancellationToken);
    }
}
