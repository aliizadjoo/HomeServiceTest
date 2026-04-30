using App.Domain.Core.Configurations;
using App.Domain.Core.Contracts.CityAgg.Repository;
using App.Domain.Core.Dtos.CityAgg;
using App.Infra.Cache.CacheKeys;
using App.Infra.Cache.Contract;
using Dapper;
using Microsoft.Data.SqlClient;

namespace App.Infra.Data.Repos.Dapper.CityAgg
{
    public class CityRepositoryDapper(SiteSetting siteSetting, ICacheService _cacheService) : ICityRepositoryDapper
    {
        private readonly string _connectionString = siteSetting.ConnectionStrings.Sql;
        public async Task<List<CityDto>> GeAll(CancellationToken cancellationToken)
        {
            var CityDtoCached = _cacheService.Get<List<CityDto>>(CacheKeys.Cities);
            if (CityDtoCached != null)
            {
                return CityDtoCached;
            }
            using (var _connection = new SqlConnection(_connectionString))
            {
                var query = new CommandDefinition(CityQueries.GetAll);

                var result = await _connection.QueryAsync<CityDto>(query);

                var cityDtos = result.ToList();

                _cacheService.SetSliding<List<CityDto>>(CacheKeys.Cities , cityDtos, 30);

                return cityDtos;
            }


        }

    }
}
