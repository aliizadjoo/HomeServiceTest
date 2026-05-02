using App.Domain.Core.Contracts.ExpertAgg.Repository;
using App.Domain.Core.Dtos;
using App.Domain.Core.Dtos.ExpertAgg;
using App.Domain.Core.Entities;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;

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

            await _context.Experts.AddAsync(expert, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            ExpertDto expertDto = new ExpertDto()
            {
                Id = expert.Id,
                AppUserId = userId,
                CityId = cityId,

            };

            return expertDto;
        }

        public async Task<ExpertsPagedDto> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Experts.AsNoTracking().AsQueryable();

            var totalCount = await query.CountAsync(cancellationToken);

            var expertsDto = await query.OrderBy(e => e.Id)
                  .Select(e => new ExpertDto
                  {
                      Id = e.Id,
                      AppUserId = e.AppUserId,
                      FirstName = e.AppUser.FirstName,
                      LastName = e.AppUser.LastName,
                      PhoneNumber = e.AppUser.PhoneNumber,
                      CreatedAt = e.CreatedAt,
                      AverageScore = e.AverageScore,
                      HomeServicesName = e.ExpertHomeServices.Select(ehs => ehs.HomeService.Name).ToList(),

                  }).Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize).ToListAsync(cancellationToken);

            ExpertsPagedDto expertsPagedDto = new ExpertsPagedDto()
            {
                ExpertsDto = expertsDto,
                TotalCount = totalCount
            };

            return expertsPagedDto;
        }

        public async Task<EditExpertDto?> GetByAppUserIdForUpdate(int appUserId, CancellationToken cancellationToken)
        {
            return await _context.Experts.Where(e => e.AppUserId == appUserId)
                   .Select(e => new EditExpertDto
                   {
                       AppUserId = e.AppUserId,
                       FirstName = e.AppUser.FirstName,
                       LastName = e.AppUser.LastName,
                       PhoneNumber = e.AppUser.PhoneNumber,
                       ImagePath = e.AppUser.ImagePath,
                       Email = e.AppUser.Email,
                       CityId = e.CityId,
                       WalletBalance = e.WalletBalance,
                       Bio = e.Bio,
                       HomeServices = e.ExpertHomeServices.Select(ehs => ehs.HomeService.Name).ToList(),
                       HomeServiceIds = e.ExpertHomeServices.Select(ehs=>ehs.HomeServiceId).ToList(),
                   }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> GetIdByAppUserId(int userId, CancellationToken cancellationToken)
        {
            return await _context.Experts.Where(e => e.AppUserId == userId)
                   .Select(e => e.Id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> Update(int appUserId, EditExpertDto dto, CancellationToken cancellationToken)
        {
            var expert = await _context.Experts
                .Include(e => e.AppUser)
                .Include(e => e.ExpertHomeServices)
                .FirstOrDefaultAsync(e => e.AppUserId == appUserId, cancellationToken);

            if (expert == null)
                return false;


            expert.AppUser.FirstName = dto.FirstName;
            expert.AppUser.LastName = dto.LastName;
            expert.AppUser.PhoneNumber = dto.PhoneNumber;
            expert.AppUser.ImagePath = dto.ImagePath;
            expert.AppUser.Email = dto.Email;
            expert.Bio = dto.Bio;
            expert.WalletBalance = dto.WalletBalance;
            expert.CityId = dto.CityId;

            _context.ExpertHomeServices.RemoveRange(expert.ExpertHomeServices);

            var newExpertHomeServices = dto.HomeServiceIds
                .Select(homeServiceId => new ExpertHomeService
                {
                    ExpertId = expert.Id,
                    HomeServiceId = homeServiceId
                })
                .ToList();

            await _context.ExpertHomeServices.AddRangeAsync(newExpertHomeServices, cancellationToken);

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }


    }
}
