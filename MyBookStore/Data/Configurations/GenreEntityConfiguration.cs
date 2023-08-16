using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Common;
using MyBookStore.Models;

namespace MyBookStore.Data.Configurations
{
	public class GenreEntityConfiguration : IEntityTypeConfiguration<Genre>
	{
		public void Configure(EntityTypeBuilder<Genre> builder)
		{
			builder.Property(x => x.Name)
				.HasMaxLength(GenreGlobalConstant.GenreNameMaxLength)
				.IsUnicode(false);
		}
	}
}
