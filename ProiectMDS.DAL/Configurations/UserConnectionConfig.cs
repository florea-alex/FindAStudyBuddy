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
    public class UserConnectionConfig : IEntityTypeConfiguration<UserConnections>
    {
        public void Configure(EntityTypeBuilder<UserConnections> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
