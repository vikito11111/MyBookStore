using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Common;
using MyBookStore.Models;

namespace MyBookStore.Data.Configurations
{
	public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
	{
		public void Configure(EntityTypeBuilder<Author> builder)
		{
			builder.Property(x => x.Name)
				.HasMaxLength(AuthorGlobalConstant.AuthorNameMaxLength)
				.IsUnicode(true);

			builder.Property(x => x.Bio)
				.HasMaxLength(AuthorGlobalConstant.AuthorBioMaxLength)
				.IsUnicode(false);
		}
	}
}
