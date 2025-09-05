using Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Mappings
{
    internal class ApiConsumeStatsMapConfig : IEntityTypeConfiguration<ApiConsumeStats>
    {
        public void Configure(EntityTypeBuilder<ApiConsumeStats> builder)
        {
            builder.ToTable("ApiConsumeStats");
        }
    }
}
