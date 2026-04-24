using App.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configurations.AdminAgg
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {

            builder.Property(a => a.TotalRevenue)
                 .HasPrecision(18, 2)
                 .IsRequired();

            builder.HasQueryFilter(a => !a.IsDeleted);


            builder.HasData(new Admin
            {
                Id = 1,
                AppUserId = 1,
                TotalRevenue = 0m,
                StaffCode = "ADM-1001",
                CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0),
                IsDeleted = false
            });
        }
    }
}
