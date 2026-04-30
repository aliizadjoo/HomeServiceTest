using App.Domain.Core.Dtos.CityAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.CityAgg.Service
{
    public interface ICityService
    {
        public Task<bool> IsExist(int cityId, CancellationToken cancellationToken);
        public Task<List<CityDto>> GeAll(CancellationToken cancellationToken);
    }
}
