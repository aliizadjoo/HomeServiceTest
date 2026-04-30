using App.Domain.Core._common;
using App.Domain.Core.Contracts.CustomerAgg.Repository;
using App.Domain.Core.Contracts.CustomerAgg.Service;
using App.Domain.Core.Dtos.CustomerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.CustomerAgg
{
    public class CustomerService(ICustomerRepository _customerRepository) : ICustomerService
    {
        public async Task<Result<CustomerDto>> Create(int userId, int cityId, CancellationToken cancellationToken)
        {
            var data =  await  _customerRepository.Create(userId, cityId, cancellationToken);

            if (data ==null)
            {
                return Result<CustomerDto>.Failure("عملیات ساخت کاستومر با خطا مواجه شد ");
            }

            return Result<CustomerDto>.Success(data);
        }
    }
}
