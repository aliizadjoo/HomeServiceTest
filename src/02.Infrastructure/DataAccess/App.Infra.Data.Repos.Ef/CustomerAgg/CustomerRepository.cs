using App.Domain.Core.Contracts.CustomerAgg.Repository;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Entities;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;

namespace App.Infra.Data.Repos.Ef.CustomerAgg
{
    public class CustomerRepository(AppDbContext _context) : ICustomerRepository
    {
        public async Task<CustomerDto?> Create(int userId, int cityId, CancellationToken cancellationToken)
        {
            Customer customer = new Customer()
            {
                AppUserId = userId,
                CityId = cityId,
            };

            await _context.AddAsync(customer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            CustomerDto customerDto = new CustomerDto()
            {
                Id = customer.Id,
                AppUserId = userId,
                CityId = cityId,
            };

            return customerDto;

        }
    }
}
