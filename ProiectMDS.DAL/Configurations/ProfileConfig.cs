using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProiectMDS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.DAL.Configurations
{
    public class ProfileConfig : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.University)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .HasColumnType("nvarchar(500)")
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.phoneNumber)
                .HasColumnType("nvarchar(15)")
                .HasMaxLength(15)
                .IsRequired(false);

            builder.HasOne(x => x.Address)
                .WithOne(x => x.Profile)
                .HasForeignKey<UserProfile>(x => x.LocationId)
                .IsRequired(false);

            builder.HasMany(x => x.Courses)
                .WithOne(x => x.Profile);
        }
    }
}
