using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.CareerAdvancement.Core.Entities.SessionsManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.CareerAdvancement.Core.Data.SessionsManagement
{
    internal sealed class AssessmentSessionConfiguration : IEntityTypeConfiguration<AssessmentSession>
    {
        public void Configure(EntityTypeBuilder<AssessmentSession> builder)
        {
            builder.ToTable(nameof(AssessmentSession));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SessionName).HasMaxLength(255).IsRequired();
            builder.Property(x => x.SessionType).HasMaxLength(10).IsRequired();
            builder.Property(x => x.AppStatus).HasMaxLength(10).IsRequired();
        }
    }
}
