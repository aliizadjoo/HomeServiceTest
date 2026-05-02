using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.ExpertAgg
{
    public class ExpertsPagedDto
    {
        public List<ExpertDto> ExpertsDto { get; set; } = [];
        public int TotalCount { get; set; }
    }
}
