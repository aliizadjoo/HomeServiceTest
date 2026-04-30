using App.Domain.Core.Dtos.CityAgg;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.CityAgg.Repository
{
    public interface ICityRepository
    {
       
        public Task<bool> IsExist(int cityId , CancellationToken cancellationToken);
    }
}
