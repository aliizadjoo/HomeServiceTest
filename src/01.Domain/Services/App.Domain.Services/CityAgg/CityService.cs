using App.Domain.Core.Contracts.CityAgg.Repository;
using App.Domain.Core.Contracts.CityAgg.Service;
using App.Domain.Core.Dtos.CityAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.CityAgg
{
    public class CityService(ICityRepository _cityRepository , ICityRepositoryDapper _cityRepositoryDapper) : ICityService
    {
        public Task<List<CityDto>> GeAll(CancellationToken cancellationToken)
        {
           return _cityRepositoryDapper.GeAll(cancellationToken);
        }

        public async Task<bool> IsExist(int cityId, CancellationToken cancellationToken)
        {
            return await _cityRepository.IsExist(cityId, cancellationToken);
        }
    }
}
