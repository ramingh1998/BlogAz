using Microsoft.EntityFrameworkCore;
using BlogAz.Domain.Entities.Blogs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogAz.Infrastructure.EntityConfigurations.Blogs
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blogs");

            builder.HasKey(q => q.Id);

            builder.Property(q => q.Title).IsRequired().HasMaxLength(60);
            builder.Property(q => q.Content).IsRequired().HasMaxLength(600);
            builder.Property(q => q.ImageName).IsRequired().HasMaxLength(300);
            builder.Property(q => q.CreatedAt).HasColumnType("datetime");
            builder.Property(q => q.UpdatedAt).HasColumnType("datetime");
        }
    }
}
