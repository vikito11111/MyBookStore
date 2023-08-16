using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Common;
using MyBookStore.Models;

namespace MyBookStore.Data.Configurations
{
	public class SubGenreEntityConfiguration : IEntityTypeConfiguration<SubGenre>
	{
		public void Configure(EntityTypeBuilder<SubGenre> builder)
		{
			builder.Property(x => x.Name)
				.HasMaxLength(SubGenreGlobalConstant.SubGenreNameMaxLength)
				.IsUnicode(false);
		}
	}
}
