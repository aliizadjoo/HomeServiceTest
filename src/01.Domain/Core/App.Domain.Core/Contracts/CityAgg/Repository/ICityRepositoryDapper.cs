using App.Domain.Core.Dtos.CityAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.CityAgg.Repository
{
    public interface ICityRepositoryDapper
    {
        public Task<List<CityDto>> GeAll(CancellationToken cancellationToken);
    }
}
