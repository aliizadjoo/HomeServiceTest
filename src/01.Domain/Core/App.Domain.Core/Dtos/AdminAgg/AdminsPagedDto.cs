using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.AdminAgg
{
    public class AdminsPagedDto
    {
        public List<AdminDto> AdminsDto { get; set; } = [];
        public int TotalCount { get; set; }
    }
}
