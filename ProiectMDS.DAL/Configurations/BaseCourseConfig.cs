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
    public class BaseCourseConfig : IEntityTypeConfiguration<BaseCourses>
    {
        public void Configure(EntityTypeBuilder<BaseCourses> builder)
        {
            builder.HasKey(x => x.CourseId);

            builder.Property(x => x.courseName)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);
        }
    }
}
