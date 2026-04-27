using App.Domain.Core.Configurations;
using App.Domain.Core.Contracts.CategoryAgg.Repository;
using App.Domain.Core.Dtos.CategoryAgg;
using App.Infra.Cache.CacheKeys;
using App.Infra.Cache.Contract;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Dapper.CategoryAgg
{
    public class CategoryRepositoryDapper(SiteSetting siteSetting , ICacheService cache) : ICategoryRepositoryDapper
    {
        private readonly string _connectionString = siteSetting.ConnectionStrings.Sql;
        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            var categoryDtosCached = cache.Get<List<CategoryDto>>(CacheKeys.Categories);
            if (categoryDtosCached != null)
            {
                return categoryDtosCached;
            }
            
            using (var connection = new SqlConnection (_connectionString)) 
            {
                var query = new CommandDefinition(CategoryQueries.GetAll, cancellationToken);
                var result =  await connection.QueryAsync<CategoryDto>(query);
                var categoryDtos = result.ToList();

                cache.SetSliding<List<CategoryDto>>(CacheKeys.Categories, categoryDtos, 30);
                return categoryDtos;
            }

        }
    }
}
