using App.Domain.Core.Contracts.CategoryAgg.Repository;
using App.Domain.Core.Dtos.CategoryAgg;
using App.Domain.Core.Entities;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Repos.Ef.CategoryAgg
{
    public class CategoryRepository(AppDbContext _context) : ICategoryRepository
    {
        public async Task<int> Create(string title, string imagePath, CancellationToken cancellationToken)
        {
            Category category = new Category()
            {
                Title = title,
                ImagePath = imagePath,
            };
            await _context.Categories.AddAsync(category, cancellationToken);

            return await _context.SaveChangesAsync(cancellationToken);

        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var affectedRows = await _context.Categories
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(c => c.IsDeleted, true),
                    cancellationToken);

            return affectedRows > 0;
        }
        public async Task<bool> Update(CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            var categoryRows = _context.Categories
                .Where(c => c.Id == categoryDto.Id)
                .ExecuteUpdateAsync
                   (setter =>
                   setter.SetProperty(c => c.Title, categoryDto.Title)
                   .SetProperty(c => c.ImagePath, categoryDto.ImagePath)
                   , cancellationToken
                   );

            return await categoryRows > 0;

        }




    }
}
