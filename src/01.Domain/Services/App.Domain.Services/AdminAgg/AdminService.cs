using App.Domain.Core._common;
using App.Domain.Core.Contracts.AdminAgg.Repository;
using App.Domain.Core.Contracts.AdminAgg.Service;
using App.Domain.Core.Dtos.AdminAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.AdminAgg
{
    public class AdminService(IAdminRepository _adminRepository) : IAdminService
    {
        public async Task<Result<AdminsPagedDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var adminsPagedDto = await _adminRepository.GetAll(pageNumber, pageSize, cancellationToken);

            if (adminsPagedDto==null||!adminsPagedDto.AdminsDto.Any())
            {
                return Result<AdminsPagedDto>.Failure("ادمینی یافت نشد.");
            }

            return Result<AdminsPagedDto>.Success(adminsPagedDto);

        }
    }
}
