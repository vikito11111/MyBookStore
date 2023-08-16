using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBookStore.Models;

namespace MyBookStore.Data.Configurations
{
    public class ApplicationUserLibraryEntityConfiguration : IEntityTypeConfiguration<ApplicationUserLibrary>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserLibrary> builder)
        {
            builder.HasKey(aul => new { aul.ApplicationUserId, aul.BookId });
        }
    }
}
