using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class CustomerUseCaseConfiguration : IEntityTypeConfiguration<CustomerUseCase>
    {
        public void Configure(EntityTypeBuilder<CustomerUseCase> builder)
        {
            builder.HasOne(x => x.Customer).WithMany(x => x.CustomerUseCases).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
