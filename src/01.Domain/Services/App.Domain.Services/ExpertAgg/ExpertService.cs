using App.Domain.Core._common;
using App.Domain.Core.Contracts.ExpertAgg.Repository;
using App.Domain.Core.Contracts.ExpertAgg.Service;
using App.Domain.Core.Dtos;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.ExpertAgg
{
    public class ExpertService(IExpertRepository _expertRepository) : IExpertService
    {
        public async Task<Result<ExpertDto>> Create(int userId, int cityId, CancellationToken cancellationToken)
        {
            var data=await _expertRepository.Create(userId, cityId, cancellationToken);
            if (data==null)
            {
                return Result<ExpertDto>.Failure("عملیات ساخت متخصص با خطا مواجه شد ");
            }
            return Result<ExpertDto>.Success(data);
        }

        public async Task<Result<ExpertsPagedDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {

            var expertsPagedDto = await _expertRepository.GetAll(pageNumber, pageSize, cancellationToken);
            if (expertsPagedDto==null || !expertsPagedDto.ExpertsDto.Any())
            {
                return Result<ExpertsPagedDto>.Failure("متخصصی یافت نشد ");
            }

            return Result<ExpertsPagedDto>.Success(expertsPagedDto);
        }

        public async Task<Result<EditExpertDto?>> GetByAppUserIdForUpdate(int appUserId, CancellationToken cancellationToken)
        {
            var editExpertDto = await _expertRepository.GetByAppUserIdForUpdate(appUserId, cancellationToken);
            if (editExpertDto!=null)
            {
                return Result<EditExpertDto?>.Success(editExpertDto);
            }

            return Result<EditExpertDto?>.Failure("همچین کارشناسی یافت نشد.");
        }

        public async Task<int> GetIdByAppUserId(int userId, CancellationToken cancellationToken)
        {
          return await _expertRepository.GetIdByAppUserId(userId, cancellationToken);
        }

        public async Task<Result<bool>> Update(int appUserId, EditExpertDto dto, CancellationToken cancellationToken)
        {
           var result = await _expertRepository.Update(appUserId, dto, cancellationToken);

            if (result)
            {
                return Result<bool>.Success(result);
            }

            return Result<bool>.Failure("ویرایشی صورت نگرفت دوباره تلاش کنید.");
        }
    }
}
