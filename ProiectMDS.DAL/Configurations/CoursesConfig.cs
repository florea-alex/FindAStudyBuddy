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
    public class CoursesConfig : IEntityTypeConfiguration<Courses>
    {
        public void Configure(EntityTypeBuilder<Courses> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.courseName)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);
        }
    }
}
