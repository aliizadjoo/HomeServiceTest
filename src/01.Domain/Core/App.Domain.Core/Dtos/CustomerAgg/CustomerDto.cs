using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.CustomerAgg
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal? WalletBalance { get; set; }
        public string? Address { get; set; }
        public int AppUserId { get; set; }
        public int CityId { get; set; }
    }
}
