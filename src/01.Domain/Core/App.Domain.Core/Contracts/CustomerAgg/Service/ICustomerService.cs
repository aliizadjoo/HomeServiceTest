using App.Domain.Core._common;
using App.Domain.Core.Dtos.CustomerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.CustomerAgg.Service
{
    public interface ICustomerService
    {
        public Task<Result<CustomerDto>> Create(int userId, int cityId, CancellationToken cancellationToken);
        public Task<int> GetIdByAppUserId(int userId, CancellationToken cancellationToken);
        public Task<Result<bool>> Update(int appUserId, UpdateCustomerDto updateCustomerDto,bool isAdmin ,CancellationToken cancellationToken);
        public Task<Result<CustomersPagedDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
        public Task<Result<UpdateCustomerDto?>> GetByAppUserIdForUpdate(int appUserId, CancellationToken cancellationToken);
    }
}
