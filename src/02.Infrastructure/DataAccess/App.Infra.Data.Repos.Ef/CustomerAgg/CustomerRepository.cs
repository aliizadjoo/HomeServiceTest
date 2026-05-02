using App.Domain.Core.Contracts.CustomerAgg.Repository;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Entities;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;

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
                CustomerId = customer.Id,
                AppUserId = userId,
                CityId = cityId,
            };

            return customerDto;

        }

        public async Task<CustomersPagedDto> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Customers.AsNoTracking().AsQueryable();

            var totalCount = await query.CountAsync(cancellationToken);

            var data = await query.OrderBy(c => c.Id)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.Id,
                    FirstName = c.AppUser.FirstName,
                    LastName = c.AppUser.LastName,
                    PhoneNumber = c.AppUser.PhoneNumber,
                    Email = c.AppUser.Email,
                    CreatedAt = c.CreatedAt,
                    WalletBalance = c.WalletBalance,
                    Address = c.Address,
                    AppUserId = c.AppUserId,
                    CityId = c.CityId,
                })
             .Skip((pageNumber - 1) * pageSize)
             .Take(pageSize).ToListAsync(cancellationToken);

            CustomersPagedDto customersPagedDto = new CustomersPagedDto()
            {
                CustomersDto = data,
                TotalCount = totalCount,
            };

            return customersPagedDto;
        }

        public async Task<UpdateCustomerDto?> GetByAppUserIdForUpdate(int appUserId, CancellationToken cancellationToken)
        {
            return await _context.Customers.Where(c => c.AppUserId == appUserId)
                 .Select(c => new UpdateCustomerDto
                 {
                     FirstName = c.AppUser.FirstName,
                     LastName = c.AppUser.LastName,
                     PhoneNumber = c.AppUser.PhoneNumber,
                     ImagePath = c.AppUser.ImagePath,
                     Email = c.AppUser.Email,
                     WalletBalance = c.WalletBalance,
                     CityId = c.CityId,
                     Address = c.Address,

                 })
               .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> GetIdByAppUserId(int userId, CancellationToken cancellationToken)
        {
            return await _context.Customers.Where(c => c.AppUserId == userId)
                   .Select(c => c.Id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> Update(int appUserId, UpdateCustomerDto updateCustomerDto, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.Include(c => c.AppUser).FirstOrDefaultAsync(c => c.AppUserId == appUserId, cancellationToken);
            if (customer != null)
            {
                customer.AppUser.FirstName = updateCustomerDto.FirstName;
                customer.AppUser.LastName = updateCustomerDto.LastName;
                customer.AppUser.PhoneNumber = updateCustomerDto.PhoneNumber;
                customer.AppUser.ImagePath = updateCustomerDto.ImagePath;
                customer.WalletBalance = updateCustomerDto.WalletBalance;
                customer.CityId = updateCustomerDto.CityId;
                customer.Address = updateCustomerDto.Address;

                return await _context.SaveChangesAsync(cancellationToken) > 0;

            }

            return false;

        }


    }
}
