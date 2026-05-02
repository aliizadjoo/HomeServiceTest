using App.Domain.Core._common;
using App.Domain.Core.Contracts.CustomerAgg.AppService;
using App.Domain.Core.Contracts.CustomerAgg.Service;
using App.Domain.Core.Dtos.CustomerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.CustomerAgg
{
    public class CustomerAppService(ICustomerService _customerService) : ICustomerAppService
    {
        public async Task<Result<CustomersPagedDto>> GetAll(int pageNumber, int pageSize,  CancellationToken cancellationToken)
        {
           return await _customerService.GetAll(pageNumber ,pageSize, cancellationToken);
        }

        public async Task<Result<UpdateCustomerDto?>> GetByAppUserIdForUpdate(int appUserId, CancellationToken cancellationToken)
        {
           return await _customerService.GetByAppUserIdForUpdate(appUserId, cancellationToken);
        }

        public async Task<Result<bool>> Update(int appUserId, UpdateCustomerDto updateCustomerDto, bool isAdmin, CancellationToken cancellationToken)
        {
          return await _customerService.Update(appUserId  , updateCustomerDto, isAdmin, cancellationToken);
        }
    }
}
