using App.Domain.Core.Contracts.AdminAgg.Repository;
using App.Domain.Core.Dtos.AdminAgg;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace App.Infra.Data.Repos.Ef.AdminAgg
{
    public class AdminRepository(AppDbContext _context , ILogger<AdminRepository> _logger) : IAdminRepository
    {
        public async Task<AdminsPagedDto> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Admins.AsNoTracking().AsQueryable();

            var totalCount = await query.CountAsync(cancellationToken);

            var adminsDto = await query
                 .OrderBy(a => a.Id)
                 .Select(a => new AdminDto
                 {
                     FirstName = a.AppUser.FirstName,
                     LastName = a.AppUser.LastName,
                     PhoneNumber = a.AppUser.PhoneNumber,
                     StaffCode = a.StaffCode
                 })
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize).ToListAsync(cancellationToken);

            AdminsPagedDto adminsPagedDto = new AdminsPagedDto()
            {
                AdminsDto = adminsDto,
                TotalCount = totalCount
            };

           _logger.LogInformation("لیست مدیران دریافت شد. تعداد کل: {Total}", totalCount);
            return adminsPagedDto;
        }
    }
}
