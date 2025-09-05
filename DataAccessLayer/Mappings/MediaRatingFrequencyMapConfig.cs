using Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Mappings
{
    internal class MediaRatingFrequencyMapConfig : IEntityTypeConfiguration<MediaRatingFrequency>
    {
        public void Configure(EntityTypeBuilder<MediaRatingFrequency> builder)
        {
            builder.ToTable("MediaRatingFrequency");
        }
    }
}
