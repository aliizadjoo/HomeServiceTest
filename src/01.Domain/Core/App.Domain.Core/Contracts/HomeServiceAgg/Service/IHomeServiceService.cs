using App.Domain.Core._common;
using App.Domain.Core.Dtos.HomeServiceAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.HomeServiceAgg.Service
{
    public interface IHomeServiceService
    {
        public Task<Result<List<HomeServiceDto>>> GetAll(CancellationToken cancellationToken);
    }
}
