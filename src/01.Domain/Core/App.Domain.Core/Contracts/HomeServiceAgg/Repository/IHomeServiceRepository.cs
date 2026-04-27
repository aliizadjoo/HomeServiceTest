using App.Domain.Core.Dtos.HomeServiceAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.HomeServiceAgg.Repository
{
    public interface IHomeServiceRepository
    {
        public Task<int> Create(HomeServiceDto homeServiceDto, CancellationToken cancellationToken);

        public Task<bool> Update(HomeServiceDto homeServiceDto, CancellationToken cancellationToken);
        public Task<bool> Delete(int homeServiceId, CancellationToken cancellationToken);
    }
}
