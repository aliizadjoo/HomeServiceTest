using App.Domain.Core.Configurations;
using App.Domain.Core.Contracts.HomeServiceAgg.Repository;
using App.Domain.Core.Dtos.HomeServiceAgg;
using App.Infra.Cache.CacheKeys;
using App.Infra.Cache.Contract;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Dapper.HomeServiceAgg
{
    public class HomeServiceRepositoryDapper(ICacheService _cacheService  , SiteSetting siteSetting) : IHomeServiceRepositoryDapper
    {
        private readonly string _connectionString = siteSetting.ConnectionStrings.Sql;
        public async Task<List<HomeServiceDto>> GetAll(CancellationToken cancellationToken)
        {
            var homeServiceDtosCached= _cacheService.Get<List<HomeServiceDto>>(CacheKeys.HomeServices);

            if (homeServiceDtosCached!=null)
            {
                return homeServiceDtosCached;
            }

            using (var connection = new SqlConnection(_connectionString)) 
            {
                var query = new CommandDefinition(HomeServiceQueries.GetAll, cancellationToken);

                var result = await connection.QueryAsync<HomeServiceDto>(query);

                var HomeServiceDtos = result.ToList();

                _cacheService.SetSliding<List<HomeServiceDto>>(CacheKeys.HomeServices, HomeServiceDtos, 30);

                return HomeServiceDtos;

            }

        }
    }
}
