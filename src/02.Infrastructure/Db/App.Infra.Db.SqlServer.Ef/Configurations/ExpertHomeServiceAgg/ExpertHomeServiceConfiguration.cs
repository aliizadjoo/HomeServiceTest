using App.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configurations.ExpertHomeServiceAgg
{
    public class ExpertHomeServiceConfiguration : IEntityTypeConfiguration<ExpertHomeService>
    {
        public void Configure(EntityTypeBuilder<ExpertHomeService> builder)
        {

            builder.HasOne(eh => eh.HomeService)
                .WithMany(hs => hs.ExpertHomeServices)
                .HasForeignKey(eh => eh.HomeServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(eh => eh.Expert)
                .WithMany(e => e.ExpertHomeServices)
                .HasForeignKey(eh => eh.ExpertId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(ex => !ex.IsDeleted);
        }
    }
}
