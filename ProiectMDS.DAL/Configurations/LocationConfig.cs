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
    public class LocationConfig : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.County)
                .HasColumnType("nvarchar(30)")
                .HasMaxLength(30)
                .IsRequired(false);

            builder.Property(x => x.City)
                .HasColumnType("nvarchar(30)")
                .HasMaxLength(30)
                .IsRequired(false);

            builder.Property(x => x.Street)
                .HasColumnType("nvarchar(30)")
                .HasMaxLength(30)
                .IsRequired(false);
        }
    }
}
