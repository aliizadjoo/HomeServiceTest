using App.Domain.Core.Dtos.CustomerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.CustomerAgg.Repository
{
    public interface ICustomerRepository
    {
        public Task<CustomerDto?> Create (int userId , int cityId , CancellationToken cancellationToken);

        public Task<int> GetIdByAppUserId(int userId , CancellationToken cancellationToken);
        public Task<UpdateCustomerDto?> GetByAppUserIdForUpdate(int appUserId , CancellationToken cancellationToken);
        public Task<bool> Update(int appUserId , UpdateCustomerDto updateCustomerDto, CancellationToken cancellationToken);

        public Task<CustomersPagedDto> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
