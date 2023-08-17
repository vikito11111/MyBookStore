using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBookStore.Models;

namespace MyBookStore.Data.Configurations
{
    public class TopicEntityConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasOne(t => t.Creator)
                .WithMany(u => u.Topics)
                .HasForeignKey(t => t.CreatorId);
        }
    }
}
