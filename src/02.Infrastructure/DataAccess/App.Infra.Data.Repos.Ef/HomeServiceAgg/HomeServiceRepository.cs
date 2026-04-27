using App.Domain.Core.Contracts.HomeServiceAgg.Repository;
using App.Domain.Core.Dtos.HomeServiceAgg;
using App.Domain.Core.Entities;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Repos.Ef.HomeServiceAgg
{
    public class HomeServiceRepository(AppDbContext _context) : IHomeServiceRepository
    {
        public async Task<int> Create(HomeServiceDto homeServiceDto, CancellationToken cancellationToken)
        {
            HomeService homeService = new HomeService()
            {
                Name = homeServiceDto.Name,
                Description = homeServiceDto.Description,
                ImagePath = homeServiceDto.ImagePath,
                BasePrice = homeServiceDto.BasePrice,
                CategoryId = homeServiceDto.CategoryId,
            };

            await _context.HomeServices.AddAsync(homeService, cancellationToken);

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> Delete(int homeServiceId, CancellationToken cancellationToken)
        {
            var rowAffected = await _context.HomeServices.Where(hs => hs.Id == homeServiceId)
                .ExecuteUpdateAsync(setter =>
                setter.SetProperty(hs => hs.IsDeleted, true), cancellationToken);

            return rowAffected > 0;
        }

        public async Task<bool> Update(HomeServiceDto homeServiceDto, CancellationToken cancellationToken)
        {
            var rowsAffected = await _context.HomeServices.Where(hs => hs.Id == homeServiceDto.Id)
                 .ExecuteUpdateAsync(setter =>
                  setter
                  .SetProperty(hs => hs.Name, homeServiceDto.Name)
                  .SetProperty(hs => hs.Description, homeServiceDto.Description)
                  .SetProperty(hs => hs.ImagePath, homeServiceDto.ImagePath)
                  .SetProperty(hs => hs.BasePrice, homeServiceDto.BasePrice)
                  .SetProperty(hs => hs.CategoryId, homeServiceDto.CategoryId)
                  , cancellationToken
                  );

            return rowsAffected > 0;
        }


    }
}
