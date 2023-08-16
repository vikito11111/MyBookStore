using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Common;
using MyBookStore.Models;

namespace MyBookStore.Data.Configurations
{
	public class PublisherEntityConfiguration : IEntityTypeConfiguration<Publisher>
	{
		public void Configure(EntityTypeBuilder<Publisher> builder)
		{
			builder.Property(x => x.Name)
				.HasMaxLength(PublisherGlobalConstant.PublisherNameMaxLength)
				.IsUnicode(true);
		}
	}
}
