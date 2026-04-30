using App.Domain.Core.Contracts.CityAgg.Repository;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.CityAgg
{
    public class CityRepository(AppDbContext _context) : ICityRepository
    {
        public async Task<bool> IsExist(int cityId , CancellationToken cancellationToken)
        {
           return await _context.Cities.AnyAsync(c=>c.Id == cityId , cancellationToken);
        }
    }
}
