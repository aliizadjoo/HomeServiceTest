using App.Domain.Core.Dtos.CategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contracts.CategoryAgg.Repository
{
    public interface ICategoryRepository
    {
        public Task<int> Create(string title, string imagePath, CancellationToken cancellationToken);
        public Task<bool> Update(CategoryDto categoryDto, CancellationToken cancellationToken);
        public Task<bool> Delete(int categoryId, CancellationToken cancellationToken);


    }
}
