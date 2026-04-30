using App.Domain.Core._common;
using App.Domain.Core.Contracts.ExpertAgg.Repository;
using App.Domain.Core.Dtos.ExpertAgg;
using App.Domain.Core.Entities;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.ExpertAgg
{
    public class ExpertRepository(AppDbContext _context) : IExpertRepository
    {
        public async Task<ExpertDto?> Create(int userId, int cityId, CancellationToken cancellationToken)
        {
            Expert expert = new Expert() 
            {
              AppUserId = userId,
              CityId = cityId,
            };

            await  _context.Experts.AddAsync(expert , cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            ExpertDto expertDto = new ExpertDto() 
            {
                Id = expert.Id,
                AppUserId = userId,
                CityId= cityId,
                
            };

            return expertDto;
        }
    }
}
