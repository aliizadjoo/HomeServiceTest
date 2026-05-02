using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.ExpertAgg
{
    public class ExpertDto
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> HomeServicesName { get; set; } = [];
        public string? Bio { get; set; }
        public decimal? WalletBalance { get; set; }
        public double? AverageScore { get; set; }
        public int AppUserId { get; set; }
        public int CityId { get; set; }
    }
}
